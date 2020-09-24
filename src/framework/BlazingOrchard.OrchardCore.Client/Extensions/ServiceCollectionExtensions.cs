using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System;
using BlazingOrchard.OrchardCore.Client.Options;
using BlazingOrchard.OrchardCore.Client.Services;

namespace BlazingOrchard.OrchardCore.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrchardApiClient(
            this IServiceCollection services,
            Action<OptionsBuilder<OrchardApiClientOptions>>? configureOptions = default)
        {
            var optionsBuilder = services.AddOptions<OrchardApiClientOptions>();
            configureOptions?.Invoke(optionsBuilder);

            services.AddClient<IContentClient>();
            services.AddClient<IContentTypeClient>();

            return services;
        }

        private static void AddClient<T>(this IServiceCollection services) where T : class =>
            services.AddRefitClient<T>().ConfigureHttpClient(
                (sp, client) =>
                {
                    var options = sp.GetRequiredService<IOptions<OrchardApiClientOptions>>().Value;
                    client.BaseAddress = options.ServerUrl;
                });
    }
}