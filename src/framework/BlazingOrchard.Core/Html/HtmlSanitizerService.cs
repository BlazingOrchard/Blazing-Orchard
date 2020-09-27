using BlazingOrchard.Services;
using Ganss.XSS;
using Microsoft.Extensions.Options;
using OrchardCore.Infrastructure.Html;

namespace BlazingOrchard.Html
{
    public class HtmlSanitizerService : IHtmlSanitizerService
    {
        private readonly IHtmlSanitizer _sanitizer = new HtmlSanitizer();

        public HtmlSanitizerService(IOptions<HtmlSanitizerOptions> options)
        {
            foreach(var action in options.Value.Configure)
            {
                action?.Invoke(_sanitizer);
            }
        }

        public string Sanitize(string html)
        {
            return _sanitizer.Sanitize(html);
        }
    }
}
