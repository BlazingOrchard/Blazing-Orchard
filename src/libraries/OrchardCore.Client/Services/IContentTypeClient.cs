using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using Refit;

namespace OrchardCore.Client.Services
{
    public interface IContentTypeClient
    {
        [Get("/api/content-types/{contentTypeName}")]
        Task<ContentTypeDescriptor> GetByNameAsync(string contentTypeName, CancellationToken cancellationToken = default);
    }
}
