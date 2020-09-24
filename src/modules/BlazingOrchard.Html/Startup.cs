using BlazingOrchard.Contents;
using BlazingOrchard.Contents.Display.Extensions;
using BlazingOrchard.DisplayManagement.Extensions;
using BlazingOrchard.Html.Drivers;
using BlazingOrchard.Html.Models;
using BlazingOrchard.Html.Services;
using BlazingOrchard.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.Html
{
    public class Startup : IStartup
    {
        public void AddServices(IServiceCollection services)
        {
            services
                .AddContentPart<HtmlBodyPart>()
                .AddContentPartDisplayDriver<HtmlBodyPartDisplay>()
                .AddShapeMap<HtmlShapes>();
        }
    }
}