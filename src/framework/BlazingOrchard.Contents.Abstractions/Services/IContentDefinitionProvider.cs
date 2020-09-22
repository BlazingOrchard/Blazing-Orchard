using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrchardCore.Client.Models;

namespace BlazingOrchard.Contents.Services
{
    public interface IContentDefinitionProvider
    {
        Task<IEnumerable<ContentTypeDefinition>> ListAsync(CancellationToken cancellationToken = default);
    }
}