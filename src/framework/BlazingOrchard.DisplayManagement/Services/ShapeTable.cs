using System.Collections.Generic;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeTable
    {
        public ShapeTable(ICollection<ShapeBinding> shapeBindings)
        {
            ShapeBindings = shapeBindings;
        }

        public ICollection<ShapeBinding> ShapeBindings { get; }
    }
}