using Microsoft.EntityFrameworkCore;

namespace automato.Infrastructure.Sqlite;

public class AutomatoDbContext : DbContext
{
    public AutomatoDbContext(DbContextOptions<AutomatoDbContext> options)
        : base(options)
    {
    }
}
