using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BlazingOrchard.Extensions;
using BlazingOrchard.Web.Application;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Caching.Memory;
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
                .AddSingleton<IMemoryCache, MemoryCache>()
                .AddSingleton<HtmlEncoder, DefaultHtmlEncoder>()
                .AddSingleton<IConfiguration>(builder.Configuration)
                .AddBlazingOrchard()
                .AddModules(Application.Modules.GetAssemblies());

            await builder.Build().RunAsync();
        }
    }
    
    public class DefaultHtmlEncoder : HtmlEncoder
    {
        public override unsafe int FindFirstCharacterToEncode(char* text, int textLength)
        {
            throw new System.NotImplementedException();
        }

        public override unsafe bool TryEncodeUnicodeScalar(int unicodeScalar, char* buffer, int bufferLength, out int numberOfCharactersWritten)
        {
            throw new System.NotImplementedException();
        }

        public override bool WillEncode(int unicodeScalar)
        {
            throw new System.NotImplementedException();
        }

        public override int MaxOutputCharactersPerInputCharacter { get; }
    }
}
