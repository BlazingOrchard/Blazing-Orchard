using BlazingOrchard.ContentFields.Drivers;
using BlazingOrchard.ContentFields.Models;
using BlazingOrchard.Contents;
using BlazingOrchard.Contents.Display.Extensions;
using BlazingOrchard.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingOrchard.ContentFields
{
    public class Startup : IStartup
    {
        public void AddServices(IServiceCollection services)
        {
            services
                .AddContentField<TextField>()
                .AddContentFieldDisplayDriver<TextFieldDisplay>();
        }
    }
}