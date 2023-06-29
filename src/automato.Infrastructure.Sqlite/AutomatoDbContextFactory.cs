using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace automato.Infrastructure.Sqlite;

/// <summary>
/// Design time factory for creating the database context, required for migrations.
/// https://learn.microsoft.com/en-gb/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
/// </summary>
public class AutomatoDbContextFactory : IDesignTimeDbContextFactory<AutomatoDbContext>
{
    public AutomatoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AutomatoDbContext>();
        optionsBuilder.UseSqlite("Data Source=Data/automato.db");

        return new AutomatoDbContext(optionsBuilder.Options);
    }
}
