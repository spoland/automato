using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.Extensions.DependencyInjection;

namespace automato.Infrastructure.Hangfire;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHangfire(this IServiceCollection services, string connectionString)
    {
        // Add Hangfire services.
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage(connectionString));

        // Add the processing server as IHostedService
        services.AddHangfireServer();

        return services;
    }
}
