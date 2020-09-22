using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.DisplayManagement.Services
{
    public interface IDisplayDriver<in TModel, in TDisplayContext>
        where TDisplayContext : BuildDisplayContext
    {
        ValueTask<IDisplayResult> BuildDisplayAsync(TModel model, TDisplayContext context);
    }

    public interface IDisplayDriver<TModel> : IDisplayDriver<TModel, BuildDisplayContext>
    {
    }
}