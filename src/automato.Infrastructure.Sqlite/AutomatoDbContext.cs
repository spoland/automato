using automato.Domain.Jobs;
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

            b.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<ScheduledJob>(
        b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Name);
            b.Property(e => e.CronExpression);
            b.Property(e => e.ScheduledTaskId);

            b.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<SftpDownloadTask>(
        b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Name);
            b.Property(e => e.LocalPath);
            b.Property(e => e.RemotePath);
            b.Property(e => e.SftpServerId);
            b.Property(e => e.SearchPattern);
            b.Property(e => e.DeleteDownloadedFiles);
            b.Property(e => e.DeleteEmptyDirectories);

            b.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutomatoDbContext).Assembly);
    }

    public DbSet<SftpServer> SftpServers { get; set; }
}
