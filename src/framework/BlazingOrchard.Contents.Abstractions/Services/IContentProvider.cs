using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;

namespace BlazingOrchard.Contents.Services
{
    public interface IContentProvider
    {
        Task<ContentItem?> GetByAliasAsync(string alias, CancellationToken cancellationToken = default);
    }
}
