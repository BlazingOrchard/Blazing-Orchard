using BlazingOrchard.DisplayManagement.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazingOrchard(this IServiceCollection services)
        {
            return services.AddDisplayManagement();
        }
    }
}