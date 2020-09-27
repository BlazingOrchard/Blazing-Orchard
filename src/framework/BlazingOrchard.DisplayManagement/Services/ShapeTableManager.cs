using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlazingOrchard.DisplayManagement.Components;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeTableManager : IShapeTableManager
    {
        private readonly IComponentTypeProvider _componentTypeProvider;
        private ShapeTable? _shapeTable;

        public ShapeTableManager(IComponentTypeProvider componentTypeProvider)
        {
            _componentTypeProvider = componentTypeProvider;
        }

        public ShapeTable GetShapeTable()
        {
            if (_shapeTable == null)
            {
                var componentTypes = _componentTypeProvider.GetComponentTypes()
                    .Where(x => x.IsAssignableTo(typeof(ShapeTemplate)));
                
                var bindings = componentTypes.SelectMany(GetShapeBindings);
                _shapeTable = new ShapeTable(bindings.ToList());
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
    }
}