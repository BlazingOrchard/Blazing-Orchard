using BlazingOrchard.DisplayManagement.Blazor.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazingOrchard(this IServiceCollection services)
        {
            return services.AddBlazorDisplayManagement();
        }
    }
}