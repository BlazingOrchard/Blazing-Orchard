using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.DisplayManagement.Services
{
    public interface IDisplayResult
    {
        Task ApplyAsync(BuildDisplayContext context);
    }
}