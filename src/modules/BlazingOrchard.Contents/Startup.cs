using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.Contents.Handlers;
using BlazingOrchard.Contents.Services;
using BlazingOrchard.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlazingOrchard.OrchardCore.Client.Extensions;

namespace BlazingOrchard.Contents
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void AddServices(IServiceCollection services)
        {
            services
                .AddOrchardApiClient(options => options.Bind(Configuration.GetSection("Orchard")))
                .AddSingleton<IContentProvider, OrchardApiContentProvider>()
                .AddSingleton<IContentTypeProvider, OrchardApiContentTypeProvider>()
                .AddSingleton<IContentDisplayManager, ContentDisplayManager>()
                .AddSingleton<IContentDisplayHandler, ContentItemDisplayDriverCoordinator>();
        }
    }
}