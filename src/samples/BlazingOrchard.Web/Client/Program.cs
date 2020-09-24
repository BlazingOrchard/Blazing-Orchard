using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BlazingOrchard.Extensions;
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
                .AddModules(GetModuleAssemblies());

            await builder.Build().RunAsync();
        }

        private static IEnumerable<Assembly> GetModuleAssemblies()
        {
            yield return typeof(BlazingOrchard.Contents.Startup).Assembly;
            yield return typeof(BlazingOrchard.Title.Startup).Assembly;
        }
    }
}
