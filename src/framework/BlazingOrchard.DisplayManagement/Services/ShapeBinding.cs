using System;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeBinding
    {
        public ShapeBinding(string shapeType, Type componentType)
        {
            ShapeType = shapeType;
            ComponentType = componentType;
        }

        public string ShapeType { get; }
        public Type ComponentType { get; }
    }
}