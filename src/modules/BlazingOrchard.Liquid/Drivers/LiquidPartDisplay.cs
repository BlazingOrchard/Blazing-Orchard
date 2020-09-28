using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using BlazingOrchard.Liquid.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazingOrchard.Liquid.Drivers
{
    public class LiquidPartDisplay : ContentPartDisplayDriver<LiquidPart>
    {
        private readonly ILiquidTemplateManager _liquidTemplateManager;
        private readonly HtmlEncoder _htmlEncoder;

        public LiquidPartDisplay(ILiquidTemplateManager liquidTemplateManager, HtmlEncoder htmlEncoder)
        {
            _liquidTemplateManager = liquidTemplateManager;
            _htmlEncoder = htmlEncoder;
        }

        protected override IDisplayResult? BuildDisplay(LiquidPart contentPart)
        {
            return Shape(
                    nameof(LiquidPart),
                    async context =>
                    {
                        var shape = await context.New.LiquidPart();
                        shape.ContentItem = contentPart.ContentItem;
                        shape.Liquid = contentPart.Liquid!;
                        shape.LiquidBodyPart = contentPart;
                        //shape.Html = (await ToHtmlAsync(contentPart, shape)) ?? "";
                        shape.RenderLiquid = (RenderFragment)(b => RenderLiquid(b, contentPart, shape));
                        
                        return shape;
                    })
                .DefaultLocation("5");
        }

        private void RenderLiquid(RenderTreeBuilder builder, LiquidPart liquidPart, IShape shape)
        {
            var writer = new RenderTreeWriter(builder);
            
            _liquidTemplateManager.RenderAsync(
                liquidPart.Liquid,
                writer,
                _htmlEncoder,
                shape,
                scope => { scope.SetValue("ContentItem", liquidPart.ContentItem); }).GetAwaiter().GetResult();
            
            //builder.AddMarkupContent(0, "<strong>Hey, it's me!</strong>");
        }

        private async Task<string?> ToHtmlAsync(LiquidPart liquidPart, IShape shape)
        {
            return await _liquidTemplateManager.RenderAsync(
                liquidPart.Liquid,
                _htmlEncoder,
                shape,
                scope => { scope.SetValue("ContentItem", liquidPart.ContentItem); });
        }
    }

    internal class RenderTreeWriter : TextWriter
    {
        private readonly RenderTreeBuilder _builder;
        private int _sequence;

        public RenderTreeWriter(RenderTreeBuilder builder)
        {
            _builder = builder;
        }
        
        public override Encoding Encoding { get; }

        public override void Write(string? value)
        {
            _builder.AddMarkupContent(_sequence++, value);
            //base.Write(value);
        }

        public override Task WriteAsync(string? value)
        {
            _builder.AddMarkupContent(_sequence++, value);
            return Task.CompletedTask;
            //return base.WriteAsync(value);
        }
    }
}