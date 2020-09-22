using System.Threading.Tasks;
using BlazingOrchard.Contents.Display;
using BlazingOrchard.Contents.Services;
using BlazingOrchard.Core.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.Services;
using BlazingOrchard.Web.Client.Drivers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Client.Extensions;

namespace BlazingOrchard.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var services = builder.Services;
            builder.RootComponents.Add<App>("#app");

            services
                .AddOrchardApiClient(options =>
                {
                    options.Bind(builder.Configuration.GetSection("Orchard"));
                })
                .AddSingleton<IAutorouteEntries, AutorouteEntries>()
                .AddSingleton<IContentProvider, OrchardApiContentProvider>()
                .AddSingleton<IShapeFactory, ShapeFactory>()
                .AddSingleton<IContentDisplayManager, ContentDisplayManager>()
                .AddSingleton<IContentDisplayHandler, ContentItemDisplayCoordinator>()
                .AddSingleton<IContentDisplayDriver, ArticleDriver>()
                .AddSingleton<IShapeMapProvider, DemoShapeMapProvider>();

            await builder.Build().RunAsync();
        }
    }
}
