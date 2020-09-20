using System.Threading;
using System.Threading.Tasks;

namespace Orchard.Canopy.Abstractions
{
    public interface IContentSource
    {
        Task<ContentItem?> GetByIdAsync(string contentItemId, CancellationToken cancellationToken = default);
        Task<ContentItem?> GetByAliasAsync(string alias, CancellationToken cancellationToken = default);
    }
}
