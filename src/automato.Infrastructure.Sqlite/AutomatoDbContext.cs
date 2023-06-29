using System.Reflection.Metadata;
using automato.Domain.SFTP;
using Microsoft.EntityFrameworkCore;

namespace automato.Infrastructure.Sqlite;

public class AutomatoDbContext : DbContext
{
    public AutomatoDbContext(DbContextOptions<AutomatoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SftpServer>(
        b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Name);
            b.Property(e => e.Port);
            b.Property(e => e.Hostname);
            b.Property(e => e.Password);
            b.Property(e => e.Username);
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutomatoDbContext).Assembly);
    }

    public DbSet<SftpServer> SftpServers { get; set; }
}
