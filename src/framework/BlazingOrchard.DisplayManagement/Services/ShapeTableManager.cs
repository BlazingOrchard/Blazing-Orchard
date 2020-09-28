using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlazingOrchard.DisplayManagement.Components;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeTableManager : IShapeTableManager
    {
        private readonly IComponentTypeProvider _componentTypeProvider;
        private readonly IEnumerable<IShapeTableProvider> _shapeTableProviders;
        private ShapeTable? _shapeTable;

        public ShapeTableManager(
            IComponentTypeProvider componentTypeProvider,
            IEnumerable<IShapeTableProvider> shapeTableProviders)
        {
            _componentTypeProvider = componentTypeProvider;
            _shapeTableProviders = shapeTableProviders;
        }

        public ShapeTable GetShapeTable()
        {
            if (_shapeTable == null)
            {
                var shapeDescriptors = new Dictionary<string, ShapeDescriptor>();

                foreach (var shapeTableProvider in _shapeTableProviders)
                {
                    var builder = new ShapeTableBuilder();
                    shapeTableProvider.Discover(builder);
                    var alterationSets = builder.BuildAlterations().GroupBy(x => x.ShapeType);

                    foreach (var alterations in alterationSets)
                    {
                        var shapeType = alterations.Key;
                        var key = $"{shapeTableProvider.GetType().Name}{shapeType.ToLower()}";

                        if (!shapeDescriptors.ContainsKey(key))
                        {
                            var descriptor = new ShapeDescriptor(shapeType);

                            foreach (var alteration in alterations)
                                alteration.Alter(descriptor);

                            shapeDescriptors[key] = descriptor;
                        }
                    }
                }

                var mergedDescriptors = MergeDescriptors(shapeDescriptors).ToDictionary(x => x.ShapeType);
                var componentTypes = _componentTypeProvider.GetComponentTypes();
                var bindings = componentTypes.SelectMany(GetShapeBindings);

                _shapeTable = new ShapeTable(bindings.ToList(), mergedDescriptors);
            }

            return _shapeTable;
        }

        private IEnumerable<ShapeBinding> GetShapeBindings(Type componentType)
        {
            yield return new ShapeBinding(componentType.Name, componentType);

            var shapeAttribute = componentType.GetCustomAttribute<ShapeAttribute>();

            if (shapeAttribute != null)
                yield return new ShapeBinding(shapeAttribute.ShapeType, componentType);
        }

        private IEnumerable<ShapeDescriptor> MergeDescriptors(IDictionary<string, ShapeDescriptor> shapeDescriptors)
        {
            return shapeDescriptors
                .GroupBy(x => x.Value.ShapeType)
                .Select(
                    group => new ShapeDescriptor(group.Key)
                    {
                        Bindings = group
                            .SelectMany(x => x.Value.Bindings)
                            .GroupBy(x => x.Key, StringComparer.OrdinalIgnoreCase)
                            .Select(x => x.Last())
                            .ToDictionary(x => x.Key, x => x.Value),

                        Wrappers = group
                            .SelectMany(sd => sd.Value.Wrappers)
                            .ToList(),

                        CreatingAsync = group
                            .SelectMany(sd => sd.Value.CreatingAsync)
                            .ToList(),

                        CreatedAsync = group
                            .SelectMany(sd => sd.Value.CreatedAsync)
                            .ToList(),

                        DisplayingAsync = group
                            .SelectMany(sd => sd.Value.DisplayingAsync)
                            .ToList(),

                        ProcessingAsync = group
                            .SelectMany(sd => sd.Value.ProcessingAsync)
                            .ToList(),

                        DisplayedAsync = group
                            .SelectMany(sd => sd.Value.DisplayedAsync)
                            .ToList()
                    });
        }
    }
}