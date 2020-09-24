using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.ContentFields.Components;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using Microsoft.AspNetCore.Components;

namespace BlazingOrchard.ContentFields.Services
{
    public class ContentFieldShapes : IShapeMapRule
    {
        public bool Matches(IShape shape) => shape.Metadata.Type == "TextField";

        public ValueTask<ComponentDescriptor> DescribeComponentAsync(
            IShape shape,
            CancellationToken cancellationToken = default)
        {
            var descriptor = shape.Metadata.Type switch
            {
                "TextField" => CreateComponentDescriptor<TextField>(shape),
                _ => throw new ArgumentException($"Shape type {shape.Metadata.Type} is not supported")
            };

            return new ValueTask<ComponentDescriptor>(descriptor);
        }

        private ComponentDescriptor CreateComponentDescriptor<T>(IShape shape) where T : IComponent =>
            new ComponentDescriptor
            {
                ComponentType = typeof(T),
                Attributes = new Dictionary<string, object>
                {
                    ["Model"] = shape
                }
            };
    }
}