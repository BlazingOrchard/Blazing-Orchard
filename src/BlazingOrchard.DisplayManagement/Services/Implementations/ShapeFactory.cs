using System;
using System.Dynamic;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Extensions;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeFactory : DynamicObject, IShapeFactory
    {
        public ValueTask<IShape> CreateAsync(string shapeType)
        {
            var shape = new Shape { Metadata = { Type = shapeType } };
            return new ValueTask<IShape>(shape);
        }
        
        public dynamic New => this;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
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