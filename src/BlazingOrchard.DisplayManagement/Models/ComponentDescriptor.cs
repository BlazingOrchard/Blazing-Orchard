using System;
using System.Collections.Generic;

namespace BlazingOrchard.DisplayManagement.Models
{
    public class ComponentDescriptor
    {
        public Type ComponentType { get; set; }
        public IDictionary<string, object> Attributes { get; set; }
    }
}