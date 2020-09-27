using BlazingOrchard.Markdown.Options;
using Markdig;
using Microsoft.Extensions.Options;

namespace BlazingOrchard.Markdown.Services
{
    public class MarkdownService : IMarkdownService
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public MarkdownService(IOptions<MarkdownPipelineOptions> options)
        {
            var builder = new MarkdownPipelineBuilder();

            foreach (var action in options.Value.Configure)
                action?.Invoke(builder);

            _markdownPipeline = builder.Build();
        }

        public string? ToHtml(string? markdown) =>
            markdown == null ? null : Markdig.Markdown.ToHtml(markdown, _markdownPipeline);
    }
}