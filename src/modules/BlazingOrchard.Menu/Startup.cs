using BlazingOrchard.Contents;
using BlazingOrchard.Contents.Display.Extensions;
using BlazingOrchard.DisplayManagement.Extensions;
using BlazingOrchard.Menu.Drivers;
using BlazingOrchard.Menu.Models;
using BlazingOrchard.Menu.Services;
using BlazingOrchard.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Menu
{
    public class Startup : IStartup
    {
        public void AddServices(IServiceCollection services)
        {
            services
                .AddContentPart<MenuItemsListPart>()
                .AddContentPartDisplayDriver<MenuItemsListPartDisplay>()
                .AddShapeMap<MenuShapes>();
        }
    }
}