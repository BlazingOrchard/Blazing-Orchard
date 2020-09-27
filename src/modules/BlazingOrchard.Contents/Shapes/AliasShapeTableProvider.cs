using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.Contents.Shapes
{
    public class AliasShapeTableProvider : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Content")
                .OnDisplaying(
                    context =>
                    {
                        dynamic shape = context.Shape;
                        var contentItem = (ContentItem)shape.ContentItem;

                        if (contentItem.ContentType == "Menu")
                            context.Shape.Metadata.Alternates.Add("Menu__MainMenu");
                    });
        }
    }
}