using System;
using System.Linq;
using BlazingOrchard.DisplayManagement.Shapes;
using Microsoft.AspNetCore.Components;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeRenderer : IShapeRenderer
    {
        private readonly IShapeTableManager _shapeTableManager;

        public ShapeRenderer(IShapeTableManager shapeTableManager)
        {
            _shapeTableManager = shapeTableManager;
        }

        public RenderFragment RenderShape(IShape shape)
        {
            var componentType = GetComponentType(shape);
            return RenderComponent(componentType, shape);
        }

        private Type GetComponentType(IShape shape)
        {
            var shapeTable = _shapeTableManager.GetShapeTable();
            var shapeTypes = shape.Metadata.Alternates.Reverse().Concat(new[] { shape.Metadata.Type }).ToList();

            var query =
                from shapeType in shapeTypes
                from b in shapeTable.ShapeBindings
                where b.ShapeType == shapeType
                select b;

            var binding = query.FirstOrDefault();

            if (binding == null)
                throw new Exception($"No component binding found for shape {shape.Metadata.Type}.");

            return binding.ComponentType;
        }

        private RenderFragment RenderComponent(Type componentType, IShape shape) => builder =>
        {
            builder.OpenComponent(0, componentType);
            builder.AddAttribute(1, "Model", shape);
            builder.CloseComponent();
        };
    }
}