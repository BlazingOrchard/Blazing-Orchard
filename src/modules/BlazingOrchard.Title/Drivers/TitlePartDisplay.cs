using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.Title.Models;

namespace BlazingOrchard.Title.Drivers
{
    public class TitlePartDisplay : ContentPartDisplayDriver<TitlePart>
    {
        protected override IDisplayResult? BuildDisplay(TitlePart contentPart)
        {
            return Shape(
                    "TitlePart",
                    async context =>
                    {
                        dynamic shape = await context.New.TitlePart();
                        shape.ContentItem = contentPart.ContentItem;
                        shape.Title = contentPart.ContentItem.DisplayText;
                        shape.TitlePart = contentPart;

                        shape.Metadata.Alternates.Add($"{contentPart.ContentItem.ContentType}__{shape.Metadata.Type}");
                        return shape;
                    })
                .DefaultLocation("1");
        }
    }
}