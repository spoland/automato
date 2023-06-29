using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace automato.Infrastructure.Sqlite;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AutomatoDbContext>(options => options.UseSqlite($"Data Source={connectionString}"));

        return services;
    }
}
