using automato.App;
using automato.App.Data;
using automato.Domain.Services.SFTP;
using automato.Infrastructure.Hangfire;
using automato.Infrastructure.Sqlite;
using automato.Infrastructure.SshNet;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new KeyNotFoundException("Default database connection string missing from application settings.");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDatabase(connectionString);
builder.Services.AddHangfire(connectionString);
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<ISftpService, SshNetSftpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AutomatoDbContext>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();