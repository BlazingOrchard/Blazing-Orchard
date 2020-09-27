using BlazingOrchard.Contents;
using BlazingOrchard.Contents.Display.Extensions;
using BlazingOrchard.Html;
using BlazingOrchard.Markdown.Drivers;
using BlazingOrchard.Markdown.Extensions;
using BlazingOrchard.Markdown.Models;
using BlazingOrchard.Markdown.Services;
using BlazingOrchard.Modules;
using Markdig;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Markdown
{
    public class Startup : IStartup
    {
        public void AddServices(IServiceCollection services)
        {
            services
                .AddContentPart<MarkdownBodyPart>()
                .AddContentPartDisplayDriver<MarkdownBodyPartDisplay>()
                .AddContentField<MarkdownField>()
                .AddContentFieldDisplayDriver<MarkdownFieldDisplay>()
                .AddSingleton<IMarkdownService, MarkdownService>()
                .AddHtmlSanitizer();

            services.ConfigureMarkdownPipeline(pipeline => pipeline.DisableHtml());
        }
    }
}