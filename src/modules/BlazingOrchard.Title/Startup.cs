using BlazingOrchard.Contents;
using BlazingOrchard.Contents.Display.Extensions;
using BlazingOrchard.DisplayManagement.Extensions;
using BlazingOrchard.Modules;
using BlazingOrchard.Title.Drivers;
using BlazingOrchard.Title.Models;
using BlazingOrchard.Title.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Title
{
    public class Startup : IStartup
    {
        public void AddServices(IServiceCollection services)
        {
            services
                .AddContentPart<TitlePart>()
                .AddContentPartDisplayDriver<TitlePartDisplay>()
                .AddShapeMap<TitleShapes>();
        }
    }
}