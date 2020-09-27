using BlazingOrchard.Alias.Models;
using BlazingOrchard.Contents;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Services;
using Humanizer;

namespace BlazingOrchard.Alias.Services
{
    public class AliasShapes : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Content")
                .OnDisplaying(
                    context =>
                    {
                        dynamic shape = context.Shape;
                        var contentItem = (ContentItem)shape.ContentItem;
                        var aliasPart = contentItem.Get<AliasPart>(nameof(AliasPart));

                        if (!string.IsNullOrWhiteSpace(aliasPart?.Alias))
                        {
                            context.Shape.Metadata.Alternates.Add(
                                $"Content__{contentItem.ContentType}__{aliasPart.Alias}");
                            
                            context.Shape.Metadata.Alternates.Add($"Content__{aliasPart.Alias.Titleize().Pascalize()}");
                        }
                    });
        }
    }
}