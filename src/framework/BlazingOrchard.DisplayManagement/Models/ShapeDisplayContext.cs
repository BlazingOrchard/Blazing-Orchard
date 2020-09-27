using System;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Models
{
    public class ShapeDisplayContext
    {
        public IShape Shape { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
