using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazingOrchard.DisplayManagement.Blazor.Services
{
    public class ComponentTypeProvider : IComponentTypeProvider
    {
        private readonly IModuleRegistry _moduleRegistry;
        private IList<Type>? _componentTypes;

        public ComponentTypeProvider(IModuleRegistry moduleRegistry)
        {
            _moduleRegistry = moduleRegistry;
        }
        
        public IEnumerable<Type> GetComponentTypes()
        {
            if (_componentTypes == null)
            {
                var query =
                    from assembly in _moduleRegistry.ModuleAssemblies
                    from type in assembly.GetExportedTypes()
                    where type.IsAssignableTo(typeof(IComponent))
                    select type;

                _componentTypes = query.ToList();
            }

            return _componentTypes;
        }
    }
}