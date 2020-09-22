using System.Threading;
using System.Threading.Tasks;
using OrchardCore.Client.Models;

namespace BlazingOrchard.Contents.Services
{
    public interface IContentProvider
    {
        Task<ContentItem?> GetByIdAsync(string contentItemId, CancellationToken cancellationToken = default);
        Task<ContentItem?> GetByAliasAsync(string alias, CancellationToken cancellationToken = default);
    }
}
