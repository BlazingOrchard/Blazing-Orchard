using System.Collections.Generic;
using System.Reflection;

namespace BlazingOrchard.Services
{
    public interface IModuleRegistry
    {
        /// <summary>
        /// Returns all assemblies that represent an application module.
        /// </summary>
        IEnumerable<Assembly> ModuleAssemblies { get; }

        /// <summary>
        /// Adds module assemblies to the registry.
        /// </summary>
        void AddModuleAssemblies(IEnumerable<Assembly> assemblies);
    }
}