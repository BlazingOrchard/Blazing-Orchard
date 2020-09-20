using Orchard.Canopy.Abstractions;
using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace Orchard.Canopy.OrchardApiClient
{
    public interface IContentClient
    {
        [Get("/api/content/{contentItemId}")]
        Task<ContentItem> GetByIdAsync(string contentItemId, CancellationToken cancellationToken = default);
    }
}
