using System;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeResult : IDisplayResult
    {
        private readonly Func<IBuildShapeContext, ValueTask<IShape>> _shapeBuilder;

        public ShapeResult(string shapeType, Func<IBuildShapeContext, ValueTask<IShape>> shapeBuilder)
        {
            ShapeType = shapeType;
            _shapeBuilder = shapeBuilder;
        }

        public string ShapeType { get; }
        public IShape Shape { get; private set; }

        public async Task ApplyAsync(BuildDisplayContext context)
        {
            var newShape = Shape = await _shapeBuilder(context);

            // Ignore if the driver returned a null shape.
            if (newShape == null)
            {
                return;
            }

            dynamic parentShape = context.Shape;

            if (parentShape is Shape shape)
                shape.Add(newShape);
        }
    }
}