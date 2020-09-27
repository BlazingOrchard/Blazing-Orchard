using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;

namespace BlazingOrchard.Contents.Services
{
    public interface IContentTypeProvider
    {
        Task<ContentTypeDefinition> GetAsync(string contentType, CancellationToken cancellationToken = default);
    }
}