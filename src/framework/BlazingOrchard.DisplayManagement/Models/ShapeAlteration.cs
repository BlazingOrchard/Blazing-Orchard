using System;
using System.Collections.Generic;

namespace BlazingOrchard.DisplayManagement.Models
{
    public class ShapeAlteration
    {
        private readonly IList<Action<ShapeDescriptor>> _configurations;

        public ShapeAlteration(string shapeType, IList<Action<ShapeDescriptor>> configurations)
        {
            _configurations = configurations;
            ShapeType = shapeType;
        }

        public string ShapeType { get; }

        public void Alter(ShapeDescriptor descriptor)
        {
            foreach (var configuration in _configurations) 
                configuration(descriptor);
        }
    }
}