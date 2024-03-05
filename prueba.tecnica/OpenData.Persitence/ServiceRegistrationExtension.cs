namespace OpenData.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenData.Persistence.Context;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
    {
        services.AddDbContext<OpenDataContext>(options => options.UseInMemoryDatabase("OpenDataMemory"));
        return services;
    }
}