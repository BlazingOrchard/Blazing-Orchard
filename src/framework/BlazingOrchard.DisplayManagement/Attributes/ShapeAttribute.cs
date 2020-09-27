using System;

namespace BlazingOrchard.DisplayManagement
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ShapeAttribute : Attribute
    {
        public ShapeAttribute(string shapeType)
        {
            ShapeType = shapeType;
        }
        
        public string ShapeType { get; }
    }
}