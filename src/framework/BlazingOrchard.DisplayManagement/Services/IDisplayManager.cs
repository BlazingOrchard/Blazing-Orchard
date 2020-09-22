using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public interface IDisplayManager<TModel>
    {
        ValueTask<IShape> BuildDisplayAsync(TModel model, CancellationToken cancellationToken = default);
    }
}