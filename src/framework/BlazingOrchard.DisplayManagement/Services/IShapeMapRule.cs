using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public interface IShapeMapRule
    {
        bool Matches(IShape shape);
        ValueTask<ComponentDescriptor> DescribeComponentAsync(IShape shape, CancellationToken cancellationToken = default);
    }
}