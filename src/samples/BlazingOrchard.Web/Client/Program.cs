using System.Threading.Tasks;
using BlazingOrchard.Abstractions.Services;
using BlazingOrchard.Core.Services;
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
                .AddSingleton<IContentSource, OrchardApiContentSource>()
                .AddSingleton<IShapeFactory, ShapeFactory>()
                .AddSingleton<IDisplayManager, DisplayManager>();

            await builder.Build().RunAsync();
        }
    }
}
