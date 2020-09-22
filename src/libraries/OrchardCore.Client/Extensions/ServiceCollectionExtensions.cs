using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System;
using OrchardCore.Client.Options;
using OrchardCore.Client.Services;

namespace OrchardCore.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrchardApiClient(this IServiceCollection services, Action<OptionsBuilder<OrchardApiClientOptions>>? configureOptions = default)
        {
            var optionsBuilder = services.AddOptions<OrchardApiClientOptions>();
            configureOptions?.Invoke(optionsBuilder);
            
            services.AddRefitClient<IContentClient>().ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<OrchardApiClientOptions>>().Value;
                client.BaseAddress = options.ServerUrl;
            });

            return services;
        }
    }
}
