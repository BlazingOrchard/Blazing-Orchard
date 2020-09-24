using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.OrchardCore.Client.Services;

namespace BlazingOrchard.Contents.Services
{
    public class OrchardApiContentProvider : IContentProvider
    {
        private readonly IContentClient _contentClient;
        public OrchardApiContentProvider(IContentClient contentClient) => _contentClient = contentClient;

        public async Task<ContentItem?> GetByHandleAsync(string handle,
            CancellationToken cancellationToken = default) =>
            await _contentClient.GetByHandleAsync(handle, cancellationToken);
    }
}