using BlazingOrchard.Alias.Models;
using BlazingOrchard.Alias.Services;
using BlazingOrchard.Contents;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Alias
{
    public class Startup : IStartup
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddContentPart<AliasPart>();
            services.AddSingleton<IShapeTableProvider, AliasShapes>();
        }
    }
}