using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Extensions
{
    public static class ServiceHarvesterServiceCollectionExtensions
    {
        public static IServiceCollection AddServices<T>(
            this IServiceCollection services,
            IEnumerable<Assembly> moduleAssemblies) =>
            services
                .Scan(
                    scan => scan
                        .FromAssemblies(moduleAssemblies)
                        .AddClasses(type => type.AssignableTo<T>())
                        .As<T>()
                        .WithSingletonLifetime());
    }
}