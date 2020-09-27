using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.Html.Models;

namespace BlazingOrchard.Html.Drivers
{
    public class HtmlBodyPartDisplay : ContentPartDisplayDriver<HtmlBodyPart>
    {
        protected override IDisplayResult? BuildDisplay(HtmlBodyPart contentPart)
        {
            return Shape(
                    nameof(HtmlBodyPart),
                    async context =>
                    {
                        var shape = await context.New.HtmlBodyPart();
                        shape.ContentItem = contentPart.ContentItem;
                        shape.Html = contentPart.Html!;
                        shape.HtmlBodyPart = contentPart;
                        return shape;
                    })
                .DefaultLocation("5");
        }
    }
}