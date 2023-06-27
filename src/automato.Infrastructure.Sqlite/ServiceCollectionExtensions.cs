using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace automato.Infrastructure.Sqlite;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AutomatoDbContext>(options => options.UseSqlite("Data Source=Data/automato.db;"));

        return services;
    }
}
