using System.Threading;
using System.Threading.Tasks;
using OrchardCore.Client.Models;
using Refit;

namespace OrchardCore.Client.Services
{
    public interface IContentClient
    {
        [Get("/api/content/{contentItemId}")]
        Task<ContentItem> GetByIdAsync(string contentItemId, CancellationToken cancellationToken = default);
    }
}
