using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace automato.Infrastructure.Sqlite.Migrations;

/// <inheritdoc />
public partial class AddUniqueIndexes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_SftpServers_Name",
            table: "SftpServers",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_SftpDownloadTask_Name",
            table: "SftpDownloadTask",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ScheduledJob_Name",
            table: "ScheduledJob",
            column: "Name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_SftpServers_Name",
            table: "SftpServers");

        migrationBuilder.DropIndex(
            name: "IX_SftpDownloadTask_Name",
            table: "SftpDownloadTask");

        migrationBuilder.DropIndex(
            name: "IX_ScheduledJob_Name",
            table: "ScheduledJob");
    }
}
