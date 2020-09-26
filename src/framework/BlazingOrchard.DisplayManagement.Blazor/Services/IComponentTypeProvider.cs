using System;
using System.Collections.Generic;

namespace BlazingOrchard.DisplayManagement.Blazor.Services
{
    public interface IComponentTypeProvider
    {
        IEnumerable<Type> GetComponentTypes();
    }
}