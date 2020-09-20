using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Orchard.Canopy.Abstractions.Services;
using Orchard.Canopy.OrchardApiClient.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orchard.Canopy.Web.Client
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
                .AddSingleton<IAutorouteEntries, StubAutorouteEntries>();

            await builder.Build().RunAsync();
        }
    }
}
