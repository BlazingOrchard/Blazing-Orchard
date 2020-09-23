using BlazingOrchard.Contents.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Contents
{
    // TODO: Replace this with option-based registration like Orchard Core.
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddContentPart<T>(this IServiceCollection services)
            where T : ContentPart =>
            services.AddTransient<ContentPart, T>();

        public static IServiceCollection AddContentField<T>(this IServiceCollection services)
            where T : ContentField =>
            services.AddTransient<ContentField, T>();
    }
}