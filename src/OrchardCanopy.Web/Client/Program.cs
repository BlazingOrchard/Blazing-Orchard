using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCanopy.Abstractions.Services;
using OrchardCanopy.Core.Services;
using OrchardCore.Client.Extensions;

namespace OrchardCanopy.Web.Client
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
