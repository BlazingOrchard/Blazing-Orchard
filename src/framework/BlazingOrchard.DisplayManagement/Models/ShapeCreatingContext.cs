using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Models
{
    public class ShapeCreatingContext
    {
        public IServiceProvider ServiceProvider { get; set; }= default!;
        public IShapeFactory ShapeFactory { get; set; }= default!;
        public dynamic New { get; set; }= default!;
        public string ShapeType { get; set; }= default!;
        public Func<ValueTask<IShape>> CreateAsync { get; set; }= default!;
        public IList<Func<ShapeCreatedContext, Task>> OnCreated { get; set; } = new List<Func<ShapeCreatedContext, Task>>();

        public Func<IShape> Create
        {
            set => CreateAsync = () => new ValueTask<IShape>(value());
        }
    }
}