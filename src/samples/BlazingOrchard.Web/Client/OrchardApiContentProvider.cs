using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Services;
using BlazingOrchard.Services;
using OrchardCore.Client.Models;
using OrchardCore.Client.Services;

namespace BlazingOrchard.Web.Client
{
    public class OrchardApiContentProvider : IContentProvider
    {
        private readonly IAutorouteEntries _autorouteEntries;
        private readonly IContentClient _contentClient;

        public OrchardApiContentProvider(IAutorouteEntries autorouteEntries, IContentClient contentClient)
        {
            _autorouteEntries = autorouteEntries;
            _contentClient = contentClient;
        }

        public async Task<ContentItem?> GetByIdAsync(string contentItemId, CancellationToken cancellationToken = default)
        {
            return await _contentClient.GetByIdAsync(contentItemId, cancellationToken);
        }

        public async Task<ContentItem?> GetByAliasAsync(string alias, CancellationToken cancellationToken = default)
        {
            if (!_autorouteEntries.TryGetEntryByPath(alias, out var contentItemId))
                return null;

            return await GetByIdAsync(contentItemId!, cancellationToken);
        }
    }
}
