using System.Threading;
using System.Threading.Tasks;
using OrchardCore.Client.Models;

namespace OrchardCanopy.Abstractions.Services
{
    public interface IContentSource
    {
        Task<ContentItem?> GetByIdAsync(string contentItemId, CancellationToken cancellationToken = default);
        Task<ContentItem?> GetByAliasAsync(string alias, CancellationToken cancellationToken = default);
    }
}
