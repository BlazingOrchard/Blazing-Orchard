using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orchard.Canopy.Abstractions;
using Orchard.Canopy.OrchardApiClient.Services;
using Refit;
using System;

namespace Orchard.Canopy.OrchardApiClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrchardApiClient(this IServiceCollection services, Action<OptionsBuilder<OrchardApiClientOptions>> configureOptions = default)
        {
            var optionsBuilder = services.AddOptions<OrchardApiClientOptions>();
            configureOptions?.Invoke(optionsBuilder);
            services.AddSingleton<IContentSource, OrchardApiContentSource>();

            services.AddRefitClient<IContentClient>().ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<OrchardApiClientOptions>>().Value;
                client.BaseAddress = options.ServerUrl;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3QjBEQUIwQTE0Mzc3MkZEQzI3QURFQUMyRjNGOTk5NUMzMTJCMTgiLCJ0eXAiOiJhdCtqd3QiLCJ4NXQiOiIxN0Rhc0tGRGR5X2NKNjNxd3ZQNW1Wd3hLeGcifQ.eyJvYzplbnR5cCI6ImFwcGxpY2F0aW9uIiwic3ViIjoiY2Fub3B5IiwibmFtZSI6IkNhbm9weSIsInJvbGUiOiJBZG1pbmlzdHJhdG9yIiwiUGVybWlzc2lvbiI6WyJNYW5hZ2VTZXR0aW5ncyIsIkFjY2Vzc0FkbWluUGFuZWwiLCJNYW5hZ2VBZG1pblNldHRpbmdzIiwiTWFuYWdlQWRtaW5NZW51IiwiUHVibGlzaENvbnRlbnQiLCJFZGl0Q29udGVudCIsIkRlbGV0ZUNvbnRlbnQiLCJQcmV2aWV3Q29udGVudCIsIkNsb25lQ29udGVudCIsIkFjY2Vzc0NvbnRlbnRBcGkiLCJNYW5hZ2VUZW1wbGF0ZXMiLCJNYW5hZ2VBZG1pblRlbXBsYXRlcyIsIlZpZXdDb250ZW50VHlwZXMiLCJFZGl0Q29udGVudFR5cGVzIiwiU2V0SG9tZXBhZ2UiLCJDb250ZW50UHJldmlldyIsIkltcG9ydCIsIkV4cG9ydCIsIk1hbmFnZUxheWVycyIsIk1hbmFnZUluZGV4ZXMiLCJNYW5hZ2VNZWRpYUNvbnRlbnQiLCJNYW5hZ2VBdHRhY2hlZE1lZGlhRmllbGRzRm9sZGVyIiwiTWFuYWdlTWVudSIsIk1hbmFnZVF1ZXJpZXMiLCJNYW5hZ2VVc2VycyIsIk1hbmFnZVJvbGVzIiwiQXNzaWduUm9sZXMiLCJTaXRlT3duZXIiLCJNYW5hZ2VUYXhvbm9teSIsIkFwcGx5VGhlbWUiLCJNYW5hZ2VBcHBsaWNhdGlvbnMiLCJNYW5hZ2VTY29wZXMiLCJNYW5hZ2VDbGllbnRTZXR0aW5ncyIsIk1hbmFnZVNlcnZlclNldHRpbmdzIiwiTWFuYWdlVmFsaWRhdGlvblNldHRpbmdzIl0sIm9pX3Byc3QiOiJjYW5vcHkiLCJqdGkiOiI4MDczMWM4MC0zNmM4LTQ0NTgtYjBjNi1iOTkyMTJkMjMyYTMiLCJjbGllbnRfaWQiOiJjYW5vcHkiLCJvaV90a25faWQiOiJhOWYyMzk2OThhMWI0MGZjYjkxYzU4NzNhZGIyOGIwMSIsImF1ZCI6Im9jdDpEZWZhdWx0IiwiZXhwIjoxNjAwNjM5MTMzLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjQ0NTUvIiwiaWF0IjoxNjAwNjM1NTMzfQ.8RImHB4nQqyfSHS1s8vCFH9DimzYkWzj6YEclCo_2-AVaYw-_h0SzJL77uyEUIz-wGfm8_prVkCQvbtu32ZVOEedSB5oLc5HGIVbqpBSBN_qnw6aYgHmHL9FK4t2NRJOIjdgCaUVUDj6pzUKmqddLl8JUG2lUBG3ZH2ZJKpd0Qp2_oCtq1BlqgY0VZZ62Jd-WLTTSwEJE4q4bWSwHh7L2WEC4lZPa9zEFYNs-lTi4yN4xWOuyfgZLyjeqMkMZNygKKnC2qx6rOVIW2srL81rQ3Juos5ladQ6IXp02rK-rjAMz-2bzN6zvnWsjnru5xKT6rlqs5c-aQ9YNSZfEiwu1w");
            });

            return services;
        }
    }
}
