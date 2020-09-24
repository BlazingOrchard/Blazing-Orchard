using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using BlazingOrchard.Menu.Components;
using Microsoft.AspNetCore.Components;

namespace BlazingOrchard.Menu.Services
{
    public class MenuShapes : IShapeMapRule
    {
        public bool Matches(IShape shape) => shape.Metadata.Type == "MenuItemsListPart";

        public ValueTask<ComponentDescriptor> DescribeComponentAsync(
            IShape shape,
            CancellationToken cancellationToken = default)
        {
             var descriptor = shape.Metadata.Type switch
             {
                 "MenuItemsListPart" => CreateComponentDescriptor<MenuItemsListPart>(shape),
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