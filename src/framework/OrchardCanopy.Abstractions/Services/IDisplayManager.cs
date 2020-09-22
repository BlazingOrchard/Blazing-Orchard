using System.Threading;
using System.Threading.Tasks;
using OrchardCanopy.Abstractions.Models;

namespace OrchardCanopy.Abstractions.Services
{
    public interface IDisplayManager
    {
        ValueTask<Shape> BuildDisplayAsync<T>(T model, CancellationToken cancellationToken = default);
    }
}