using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using Refit;

namespace BlazingOrchard.OrchardCore.Client.Services
{
    public interface IContentClient
    {
        [Get("/api/content-items/by-handle/{handle}")]
        Task<ContentItem> GetByHandleAsync(string handle, CancellationToken cancellationToken = default);
    }
}