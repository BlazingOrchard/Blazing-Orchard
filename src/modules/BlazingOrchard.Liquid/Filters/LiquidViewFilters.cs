using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Extensions;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using BlazingOrchard.Liquid.FluidTypes;
using Fluid;
using Fluid.Values;
using Humanizer;

namespace BlazingOrchard.Liquid.Filters
{
    public static class LiquidViewFilters
    {
        private static readonly AsyncFilterDelegate HtmlClassDelegate = HtmlClass;
        private static readonly AsyncFilterDelegate NewShapeDelegate = NewShape;
        private static readonly AsyncFilterDelegate ShapeRenderDelegate = ShapeRender;
        private static readonly AsyncFilterDelegate ShapeStringifyDelegate = ShapeStringify;
        private static readonly FilterDelegate ShapePropertiesDelegate = ShapeProperties;

        public static FilterCollection WithLiquidViewFilters(this FilterCollection filters)
        {
            filters.AddAsyncFilter("html_class", HtmlClassDelegate);
            filters.AddAsyncFilter("shape_new", NewShapeDelegate);
            filters.AddAsyncFilter("shape_render", ShapeRenderDelegate);
            filters.AddAsyncFilter("shape_stringify", ShapeStringifyDelegate);
            filters.AddFilter("shape_properties", ShapePropertiesDelegate);

            return filters;
        }

        public static ValueTask<FluidValue> HtmlClass(FluidValue input,
            FilterArguments arguments,
            TemplateContext context)
        {
            return new ValueTask<FluidValue>(new StringValue(input.ToStringValue().Camelize()));
        }

        public static ValueTask<FluidValue> NewShape(FluidValue input,
            FilterArguments arguments,
            TemplateContext context)
        {
            static async ValueTask<FluidValue> Awaited(ValueTask<IShape> task) => FluidValue.Create(await task);

            if (!context.AmbientValues.TryGetValue("ShapeFactory", out var item) ||
                !(item is IShapeFactory shapeFactory))
                return ThrowArgumentException<ValueTask<FluidValue>>("ShapeFactory missing while invoking 'shape_new'");

            var type = input.ToStringValue();
            var properties = new Dictionary<string, object>(arguments.Count);

            foreach (var name in arguments.Names)
                properties.Add(name.Underscore().Pascalize(), arguments[name].ToObjectValue());

            var task = shapeFactory.CreateAsync(type, Arguments.From(properties));
            
            if (!task.IsCompletedSuccessfully)
                return Awaited(task);

            return new ValueTask<FluidValue>(FluidValue.Create(task.Result));
        }

        public static ValueTask<FluidValue> ShapeStringify(FluidValue input,
            FilterArguments arguments,
            TemplateContext context) => input.ToObjectValue() is IShape shape
            ? new ValueTask<FluidValue>(new ShapeValue(shape))
            : new ValueTask<FluidValue>(NilValue.Instance);

        public static ValueTask<FluidValue> ShapeRender(FluidValue input,
            FilterArguments arguments,
            TemplateContext context) =>
            input.ToObjectValue() is IShape shape
                ? new ValueTask<FluidValue>(new ShapeValue(shape))
                : new ValueTask<FluidValue>(NilValue.Instance);

        public static FluidValue ShapeProperties(FluidValue input, FilterArguments arguments, TemplateContext context)
        {
            if (input.ToObjectValue() is IShape shape)
            {
                foreach (var name in arguments.Names)
                    shape.Properties[name.Underscore().Pascalize()] = arguments[name].ToObjectValue();

                return FluidValue.Create(shape);
            }

            return NilValue.Instance;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static T ThrowArgumentException<T>(string message) => throw new ArgumentException(message);
    }
}