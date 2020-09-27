using System;
using Ganss.XSS;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Infrastructure.Html;

namespace BlazingOrchard.Html
{
    public static class HtmlSanitizerOptionsExtensions
    {
        /// <summary>
        /// Adds a configuration action to the html sanitizer.
        /// </summary>
        public static void ConfigureHtmlSanitizer(this IServiceCollection services, Action<IHtmlSanitizer> action)
        {
            services.Configure<HtmlSanitizerOptions>(o =>
            {
                o.Configure.Add(action);
            });
        }
    }
}
