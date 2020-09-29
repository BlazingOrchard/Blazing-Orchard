using System.IO;
using System.Text;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazingOrchard.Liquid.TextWriters
{
    internal class ShapeWriter : TextWriter
    {
        private readonly RenderTreeBuilder _builder;
        private readonly IShapeRenderer _shapeRenderer;
        private int _sequence;

        public ShapeWriter(RenderTreeBuilder builder, IShapeRenderer shapeRenderer)
        {
            _builder = builder;
            _shapeRenderer = shapeRenderer;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(object? value)
        {
            if (value is IShape shape)
            {
                _sequence = _shapeRenderer.RenderShapeAsync(shape, _sequence, _builder).GetAwaiter().GetResult();
                return;
            }

            base.Write(value);
        }
    }
}