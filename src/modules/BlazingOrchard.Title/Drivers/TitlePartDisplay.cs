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
                context =>
                {
                    var shape = context.New.TitlePart();
                    shape.ContentItem = contentPart.ContentItem;
                    shape.Title = contentPart.ContentItem.DisplayText;
                    shape.TitlePart = contentPart;
                    return shape;
                });
        }
    }
}