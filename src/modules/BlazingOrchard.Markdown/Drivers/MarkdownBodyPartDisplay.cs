using BlazingOrchard.Contents.Display.Models;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.Markdown.Models;
using BlazingOrchard.Markdown.Services;
using BlazingOrchard.Services;

namespace BlazingOrchard.Markdown.Drivers
{
    public class MarkdownBodyPartDisplay : ContentPartDisplayDriver<MarkdownBodyPart>
    {
        private readonly IMarkdownService _markdownService;
        private readonly IHtmlSanitizerService _htmlSanitizerService;

        public MarkdownBodyPartDisplay(IMarkdownService markdownService, IHtmlSanitizerService htmlSanitizerService)
        {
            _markdownService = markdownService;
            _htmlSanitizerService = htmlSanitizerService;
        }

        protected override IDisplayResult? BuildDisplay(MarkdownBodyPart contentPart, BuildPartDisplayContext context)
        {
            return Shape(
                    nameof(MarkdownBodyPart),
                    async model =>
                    {
                        var shape = await model.New.MarkdownBodyPart();
                        var markdown = contentPart.Markdown ?? "";
                        var html = _markdownService.ToHtml(markdown);
                        var settings = context.TypePartDefinition.GetSettings<MarkdownBodyPartSettings>();

                        if (settings.SanitizeHtml)
                            html = _htmlSanitizerService.Sanitize(html);

                        shape.Markdown = markdown;
                        shape.Html = html;
                        shape.ContentItem = contentPart.ContentItem;
                        shape.MarkdownBodyPart = contentPart;
                        return shape;
                    })
                .DefaultLocation("5");
        }
    }
}