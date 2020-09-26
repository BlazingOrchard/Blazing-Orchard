using BlazingOrchard.DisplayManagement.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.DisplayManagement.Blazor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorDisplayManagement(this IServiceCollection services)
        {
            return services
                .AddSingleton<IComponentTypeProvider, ComponentTypeProvider>()
                .AddSingleton<IShapeRenderer, ShapeRenderer>();
        }
    }
}