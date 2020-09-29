using System.IO;
using System.Text;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazingOrchard.Liquid.TextWriters
{
    internal class RenderTreeWriter : TextWriter
    {
        private readonly RenderTreeBuilder _builder;
        private readonly IShapeRenderer _shapeRenderer;
        private int _sequence;

        public RenderTreeWriter(RenderTreeBuilder builder, IShapeRenderer shapeRenderer)
        {
            _builder = builder;
            _shapeRenderer = shapeRenderer;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(string? value) => _builder.AddMarkupContent(_sequence++, value);

        public override void Write(char[]? buffer)
        {
            var text = new string(buffer);
            _builder.AddMarkupContent(_sequence++, text);
        }

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