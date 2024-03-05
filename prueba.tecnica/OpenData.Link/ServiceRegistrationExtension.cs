namespace OpenData.Link;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenData.Sync.Port;
using Polly;
using Polly.Extensions.Http;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddLinkDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("OpenData", client =>
        {
            client.BaseAddress = new Uri(configuration.GetConnectionString("OpenDataConnection"));
        }).AddPolicyHandler(GetRetryPolicy());

        services.AddScoped<IApiRest, ApiRest>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions.HandleTransientHttpError()
                                   .OrResult(response => response.StatusCode == System.Net.HttpStatusCode.NotFound)
                                   .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}