namespace OpenData.Sync;

using Microsoft.Extensions.DependencyInjection;
using OpenData.Sync.Port;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddSyncDependencies(this IServiceCollection services)
    {
        services.AddScoped<IOpenDataSyncService, OpenDataSyncService>();
        return services;
    }
}

