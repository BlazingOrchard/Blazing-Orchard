using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using OrchardCore.Client.Models;

namespace BlazingOrchard.Contents.Services
{
    public interface IContentPartDisplayDriver
    {
        Task<IDisplayResult> BuildDisplayAsync(ContentPart contentItem, BuildDisplayContext context);
    }
}