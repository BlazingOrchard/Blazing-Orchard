using System;

namespace BlazingOrchard.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependsOnAttribute : Attribute
    {
        public DependsOnAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }
        
        public Type ModuleType { get; }
    }
}