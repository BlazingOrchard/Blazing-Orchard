using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class DisplayManager<TModel> : IDisplayManager<TModel>
    {
        private readonly IShapeFactory _shapeFactory;

        public DisplayManager(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }
        
        public async ValueTask<IShape> BuildDisplayAsync(TModel model, CancellationToken cancellationToken = default)
        {
            var shapeType = typeof(TModel).Name;
            var shape = await _shapeFactory.CreateAsync(shapeType);
            return shape;
        }
    }
}