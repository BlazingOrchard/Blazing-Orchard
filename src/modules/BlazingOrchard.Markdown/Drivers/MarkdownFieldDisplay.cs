using BlazingOrchard.Contents.Display.Models;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.Markdown.Models;
using BlazingOrchard.Markdown.Services;
using BlazingOrchard.Services;

namespace BlazingOrchard.Markdown.Drivers
{
    public class MarkdownFieldDisplay : ContentFieldDisplayDriver<MarkdownField>
    {
        private readonly IMarkdownService _markdownService;
        private readonly IHtmlSanitizerService _htmlSanitizerService;

        public MarkdownFieldDisplay(IMarkdownService markdownService, IHtmlSanitizerService htmlSanitizerService)
        {
            _markdownService = markdownService;
            _htmlSanitizerService = htmlSanitizerService;
        }

        protected override IDisplayResult? BuildDisplay(MarkdownField contentField, BuildFieldDisplayContext context)
        {
            return Shape(
                    nameof(MarkdownField),
                    async model =>
                    {
                        var shape = await model.New.MarkdownField();
                        var markdown = contentField.Markdown ?? "";
                        var html = _markdownService.ToHtml(markdown);
                        var settings = context.PartFieldDefinition.GetSettings<MarkdownBodyPartSettings>();

                        if (settings.SanitizeHtml) 
                            html = _htmlSanitizerService.Sanitize(html);

                        shape.Markdown = markdown;
                        shape.Html = html;
                        shape.ContentItem = contentField.ContentItem;
                        shape.MarkdownBodyPart = contentField;
                        return shape;
                    })
                .DefaultLocation("5");
        }
    }
}