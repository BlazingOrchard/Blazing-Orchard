using System.Collections.Generic;
using System.Reflection;

namespace BlazingOrchard.Web.Client
{
    public static class Modules
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(BlazingOrchard.Contents.Startup).Assembly;
            yield return typeof(BlazingOrchard.Title.Startup).Assembly;
            yield return typeof(BlazingOrchard.Html.Startup).Assembly;
        }
    }
}