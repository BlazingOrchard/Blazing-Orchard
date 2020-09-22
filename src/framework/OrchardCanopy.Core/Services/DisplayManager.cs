using System.Threading;
using System.Threading.Tasks;
using OrchardCanopy.Abstractions.Models;
using OrchardCanopy.Abstractions.Services;

namespace OrchardCanopy.Core.Services
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
            var shape = await _shapeFactory.CreateAsync()
        }
    }
}