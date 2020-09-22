using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Shapes;
using OrchardCore.Client.Models;

namespace BlazingOrchard.Contents.Services
{
    public interface IContentDisplayManager
    {
        ValueTask<IShape> BuildDisplayAsync(ContentItem contentItem, string? displayType = default);
    }
}