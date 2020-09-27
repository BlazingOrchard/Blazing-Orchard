using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Extensions;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeFactory : DynamicObject, IShapeFactory
    {
        private readonly IShapeTableManager _shapeTableManager;
        private readonly IServiceProvider _serviceProvider;

        public ShapeFactory(IShapeTableManager shapeTableManager, IServiceProvider serviceProvider)
        {
            _shapeTableManager = shapeTableManager;
            _serviceProvider = serviceProvider;
        }

        public async ValueTask<IShape> CreateAsync(string shapeType, Func<ValueTask<IShape>> shapeFactory)
        {
            var shapeTable = _shapeTableManager.GetShapeTable();
            var descriptors = shapeTable.Descriptors;

            var creatingContext = new ShapeCreatingContext
            {
                ServiceProvider = _serviceProvider,
                New = this,
                ShapeFactory = this,
                ShapeType = shapeType,
                OnCreated = new List<Func<ShapeCreatedContext, Task>>(),
                CreateAsync = shapeFactory
            };

            if (descriptors.TryGetValue(shapeType, out var shapeDescriptor))
            {
                // "Creating" events may add behaviors and alter base type.
                foreach (var ev in shapeDescriptor.CreatingAsync)
                    await ev(creatingContext);
            }

            // Create the new instance.
            var createdContext = new ShapeCreatedContext(
                _serviceProvider,
                creatingContext.New,
                creatingContext.ShapeFactory,
                creatingContext.ShapeType,
                await creatingContext.CreateAsync());

            var shape = createdContext.Shape;

            if (shape == null)
                throw new InvalidOperationException(
                    $"Invalid base type for shape: {createdContext.Shape.GetType()}");

            var shapeMetadata = shape.Metadata;
            shapeMetadata.Type = shapeType;

            if (shapeDescriptor != null)
                foreach (var ev in shapeDescriptor.CreatedAsync)
                    await ev(createdContext);

            foreach (var ev in creatingContext.OnCreated)
                await ev(createdContext);

            return shape;
        }

        public dynamic New => this;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object result)
        {
            // await New.FooAsync()
            // await New.Foo()

            var binderName = binder.Name;

            if (binderName.EndsWith("Async", StringComparison.Ordinal))
                binderName = binder.Name.Substring(binder.Name.Length - "Async".Length);

            result = this.CreateAsync(binderName, Arguments.From(args, binder.CallInfo.ArgumentNames));

            return true;
        }
    }
}