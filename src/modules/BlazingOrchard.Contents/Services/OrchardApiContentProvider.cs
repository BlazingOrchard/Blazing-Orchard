using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using OrchardCore.Client.Services;

namespace BlazingOrchard.Contents.Services
{
    public class OrchardApiContentProvider : IContentProvider
    {
        private readonly IContentClient _contentClient;
        public OrchardApiContentProvider(IContentClient contentClient) => _contentClient = contentClient;

        public async Task<ContentItem?> GetByAliasAsync(string contentItemId,
            CancellationToken cancellationToken = default) =>
            await _contentClient.GetByAliasAsync(contentItemId, cancellationToken);
    }
}