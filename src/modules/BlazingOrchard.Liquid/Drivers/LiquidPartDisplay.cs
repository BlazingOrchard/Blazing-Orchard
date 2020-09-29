using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using BlazingOrchard.Liquid.Models;
using BlazingOrchard.Liquid.TextWriters;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazingOrchard.Liquid.Drivers
{
    public class LiquidPartDisplay : ContentPartDisplayDriver<LiquidPart>
    {
        private readonly ILiquidTemplateManager _liquidTemplateManager;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly IShapeRenderer _shapeRenderer;

        public LiquidPartDisplay(
            ILiquidTemplateManager liquidTemplateManager,
            HtmlEncoder htmlEncoder,
            IShapeRenderer shapeRenderer)
        {
            _liquidTemplateManager = liquidTemplateManager;
            _htmlEncoder = htmlEncoder;
            _shapeRenderer = shapeRenderer;
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
                        shape.RenderLiquid = (RenderFragment)(b => RenderLiquid(b, contentPart, shape));

                        return shape;
                    })
                .DefaultLocation("5");
        }

        private void RenderLiquid(RenderTreeBuilder builder, LiquidPart liquidPart, IShape shape)
        {
            var writer = new RenderTreeWriter(builder, _shapeRenderer);

            _liquidTemplateManager.RenderAsync(
                liquidPart.Liquid,
                writer,
                _htmlEncoder,
                shape,
                scope => { scope.SetValue("ContentItem", liquidPart.ContentItem); }).GetAwaiter().GetResult();
        }
    }
}