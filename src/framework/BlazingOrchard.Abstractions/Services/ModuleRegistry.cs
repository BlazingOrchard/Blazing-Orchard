using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazingOrchard.Services
{
    public class ModuleRegistry : IModuleRegistry
    {
        private readonly IList<Assembly> _assemblies;
        public ModuleRegistry(IEnumerable<Assembly> assemblies) => _assemblies = assemblies.ToList();
        public IEnumerable<Assembly> ModuleAssemblies => _assemblies.ToList();

        public void AddModuleAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies) 
                _assemblies.Add(assembly);
        }
    }
}