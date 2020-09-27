using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using Refit;

namespace BlazingOrchard.OrchardCore.Client.Services
{
    public interface IContentTypeClient
    {
        [Get("/api/content-types/{contentTypeName}")]
        Task<ContentTypeDefinition> GetByNameAsync(string contentTypeName, CancellationToken cancellationToken = default);
    }
}
