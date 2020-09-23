using BlazingOrchard.DisplayManagement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.DisplayManagement.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddShapeMap<T>(this IServiceCollection services)
            where T : class, IShapeMapProvider =>
            services.AddSingleton<IShapeMapProvider, T>();
    }
}