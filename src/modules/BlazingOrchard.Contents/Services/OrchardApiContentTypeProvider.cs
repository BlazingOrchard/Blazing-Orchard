using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using OrchardCore.Client.Services;

namespace BlazingOrchard.Contents.Services
{
    public class OrchardApiContentTypeProvider : IContentTypeProvider
    {
        private readonly IContentTypeClient _contentTypeClient;

        public OrchardApiContentTypeProvider(IContentTypeClient contentTypeClient) =>
            _contentTypeClient = contentTypeClient;

        public async Task<ContentTypeDescriptor> GetAsync(string contentType,
            CancellationToken cancellationToken = default) =>
            await _contentTypeClient.GetByNameAsync(contentType, cancellationToken);
    }
}