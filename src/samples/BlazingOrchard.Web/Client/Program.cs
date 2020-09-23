using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.Contents.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.Extensions;
using BlazingOrchard.Web.Client.Drivers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddSingleton<IConfiguration>(builder.Configuration)
                .AddModules(GetModuleAssemblies())
                .AddSingleton<IContentDisplayDriver, ArticleDriver>()
                .AddSingleton<IShapeMapProvider, DemoShapeMapProvider>();

            await builder.Build().RunAsync();
        }

        private static IEnumerable<Assembly> GetModuleAssemblies()
        {
            yield return typeof(BlazingOrchard.Contents.Startup).Assembly;
        }
    }
}
