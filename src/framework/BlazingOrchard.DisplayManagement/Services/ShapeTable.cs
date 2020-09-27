using System.Collections.Generic;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeTable
    {
        public ShapeTable(ICollection<ShapeBinding> shapeBindings, IDictionary<string, ShapeDescriptor> descriptors)
        {
            ShapeBindings = shapeBindings;
            Descriptors = descriptors;
        }

        public ICollection<ShapeBinding> ShapeBindings { get; }
        public IDictionary<string, ShapeDescriptor> Descriptors { get; }
    }
}