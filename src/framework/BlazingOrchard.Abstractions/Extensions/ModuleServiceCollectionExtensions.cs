using System.Collections.Generic;
using System.Reflection;
using BlazingOrchard.Attributes;
using BlazingOrchard.Helpers;
using BlazingOrchard.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Extensions
{
    public static class ModuleServiceCollectionExtensions
    {
        public static IServiceCollection AddModules(
            this IServiceCollection services,
            params Assembly[] moduleAssemblies) =>
            services.AddModules(moduleAssemblies);

        public static IServiceCollection AddModules(
            this IServiceCollection services,
            IEnumerable<Assembly> moduleAssemblies)
        {
            var serviceCollection = services.Copy();
            using var sp = serviceCollection.AddServices<IStartup>(moduleAssemblies).BuildServiceProvider();

            var modules = sp.GetServices<IStartup>()
                .OrderByDependenciesAndPriorities(HasDependency, x => 0);

            foreach (var module in modules)
                module.AddServices(services);

            return services;
        }

        private static bool HasDependency(IStartup observer, IStartup subject)
        {
            var dependsOn = observer.GetType().GetCustomAttribute<DependsOnAttribute>();

            if (dependsOn == null)
                return false;

            return dependsOn.ModuleType == subject.GetType();
        }
    }
}