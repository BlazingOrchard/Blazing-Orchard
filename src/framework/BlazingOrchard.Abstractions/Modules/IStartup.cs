using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Modules
{
    public interface IStartup
    {
        void AddServices(IServiceCollection services);
    }
}