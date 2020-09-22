using System;
using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Abstractions.Models;
using BlazingOrchard.Abstractions.Services;

namespace BlazingOrchard.Core.Services
{
    public class DisplayManager : IDisplayManager
    {
        private readonly IShapeFactory _shapeFactory;

        public DisplayManager(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }
        
        public async ValueTask<Shape> BuildDisplayAsync<T>(T model, CancellationToken cancellationToken = default)
        {
            //var shape = await _shapeFactory.CreateAsync();
            throw new NotImplementedException();
        }
    }
}