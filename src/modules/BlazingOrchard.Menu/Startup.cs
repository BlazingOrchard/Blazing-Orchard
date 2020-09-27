using BlazingOrchard.Contents;
using BlazingOrchard.Contents.Display.Extensions;
using BlazingOrchard.Menu.Drivers;
using BlazingOrchard.Menu.Models;
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
                .AddContentPart<LinkMenuItemPart>()
                .AddContentPart<ContentMenuItemPart>()
                .AddContentPartDisplayDriver<MenuItemsListPartDisplay>();
        }
    }
}