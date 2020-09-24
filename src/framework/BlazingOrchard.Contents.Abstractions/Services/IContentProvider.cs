using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;

namespace BlazingOrchard.Contents.Services
{
    public interface IContentProvider
    {
        Task<ContentItem?> GetByHandleAsync(string handle, CancellationToken cancellationToken = default);
    }
}
