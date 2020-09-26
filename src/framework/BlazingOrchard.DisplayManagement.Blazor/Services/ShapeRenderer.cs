using System;
using System.Linq;
using BlazingOrchard.DisplayManagement.Shapes;
using Microsoft.AspNetCore.Components;

namespace BlazingOrchard.DisplayManagement.Blazor.Services
{
    public class ShapeRenderer : IShapeRenderer
    {
        private readonly IComponentTypeProvider _componentTypeProvider;

        public ShapeRenderer(IComponentTypeProvider componentTypeProvider)
        {
            _componentTypeProvider = componentTypeProvider;
        }
        
        public RenderFragment RenderShape(IShape shape)
        {
            var componentType = GetComponentType(shape);
            return RenderComponent(componentType, shape);
        }

        private Type GetComponentType(IShape shape)
        {
            // TODO: Implement selection based on specificity of available shape alternates. 
            var componentType = _componentTypeProvider.GetComponentTypes()
                .FirstOrDefault(x => x.Name == shape.Metadata.Type);

            if (componentType == null)
                throw new Exception($"No component found to render shape {shape.Metadata.Type}.");

            return componentType;
        }
        
        private RenderFragment RenderComponent(Type componentType, IShape shape) => builder =>
        {
            builder.OpenComponent(0, componentType);
            builder.AddAttribute(1, "Model", shape);
            builder.CloseComponent();
        };
    }
}