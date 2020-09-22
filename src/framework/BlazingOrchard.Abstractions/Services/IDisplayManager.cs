using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Abstractions.Models;

namespace BlazingOrchard.Abstractions.Services
{
    public interface IDisplayManager
    {
        ValueTask<Shape> BuildDisplayAsync<T>(T model, CancellationToken cancellationToken = default);
    }
}