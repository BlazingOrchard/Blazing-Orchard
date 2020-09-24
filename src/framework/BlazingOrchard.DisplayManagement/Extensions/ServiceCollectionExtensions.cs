using BlazingOrchard.DisplayManagement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.DisplayManagement.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddShapeMap<T>(this IServiceCollection services)
            where T : class, IShapeMapRule =>
            services.AddSingleton<IShapeMapRule, T>();
    }
}