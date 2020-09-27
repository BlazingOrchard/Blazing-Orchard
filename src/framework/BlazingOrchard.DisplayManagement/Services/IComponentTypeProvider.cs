using System;
using System.Collections.Generic;

namespace BlazingOrchard.DisplayManagement.Services
{
    public interface IComponentTypeProvider
    {
        IEnumerable<Type> GetComponentTypes();
    }
}