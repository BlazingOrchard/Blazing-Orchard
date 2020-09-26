using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazingOrchard.Extensions
{
    public static class CopyServiceCollectionExtensions
    {
        public static IServiceCollection Copy(this IServiceCollection services)
        {
            var descriptors = new ServiceDescriptor[services.Count];
            services.CopyTo(descriptors, 0);
            var copy = new ServiceCollection();

            foreach (var descriptor in descriptors) 
                copy.Add(descriptor);

            return copy;
        }
    }
}