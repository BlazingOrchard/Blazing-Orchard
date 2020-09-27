using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlazingOrchard.Attributes;
using BlazingOrchard.Helpers;
using BlazingOrchard.Modules;
using BlazingOrchard.Services;
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
            var assemblies = moduleAssemblies.ToList();
            RegisterModules(services, assemblies);

            var serviceCollection = services.Copy();
            using var sp = serviceCollection.AddServices<IStartup>(assemblies).BuildServiceProvider();

            var modules = sp.GetServices<IStartup>()
                .OrderByDependenciesAndPriorities(HasDependency, x => 0);

            foreach (var module in modules)
                module.AddServices(services);

            return services;
        }

        private static void RegisterModules(IServiceCollection services, IEnumerable<Assembly> moduleAssemblies)
        {
            var descriptor = services.FirstOrDefault(x => x.ServiceType == typeof(IModuleRegistry));

            if (descriptor == null)
            {
                descriptor = new ServiceDescriptor(typeof(IModuleRegistry), new ModuleRegistry(moduleAssemblies));
                services.Add(descriptor);
            }
            else
            {
                var registry = (IModuleRegistry)descriptor.ImplementationInstance;
                registry!.AddModuleAssemblies(moduleAssemblies);
            }
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