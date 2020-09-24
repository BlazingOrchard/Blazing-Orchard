using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using Refit;

namespace BlazingOrchard.OrchardCore.Client.Services
{
    public interface IContentClient
    {
        [Get("/api/content-items/by-alias/{alias}")]
        Task<ContentItem> GetByAliasAsync(string alias, CancellationToken cancellationToken = default);
    }
}