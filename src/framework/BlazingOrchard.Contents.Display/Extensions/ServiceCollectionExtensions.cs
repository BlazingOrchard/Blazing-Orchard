using BlazingOrchard.Contents.Display.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Contents.Display.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddContentPartDisplayDriver<T>(this IServiceCollection services)
            where T : class, IContentPartDisplayDriver =>
            services.AddTransient<IContentPartDisplayDriver, T>();

        public static IServiceCollection AddContentFieldDisplayDriver<T>(this IServiceCollection services)
            where T : class, IContentFieldDisplayDriver =>
            services.AddTransient<IContentFieldDisplayDriver, T>();
    }
}