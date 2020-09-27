using BlazingOrchard.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Infrastructure.Html;

namespace BlazingOrchard.Html
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHtmlSanitizer(this IServiceCollection services)
        {
            services.AddOptions<HtmlSanitizerOptions>();
            services.ConfigureHtmlSanitizer((sanitizer) => { sanitizer.AllowedAttributes.Add("class"); });
            services.AddSingleton<IHtmlSanitizerService, HtmlSanitizerService>();

            return services;
        }
    }
}