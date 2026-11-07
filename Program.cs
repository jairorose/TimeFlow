using TimeFlow.Menus;
using TimeFlow.Data;
using TimeFlow.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        string fileName = "TimeFlowDev.db";
        string folder = Directory.GetCurrentDirectory();
        Directory.CreateDirectory(folder + "/DB/");

        string path = Path.Combine(folder, "DB", fileName);
        
        builder.Services.AddDbContext<TimeFlowDbContext>(options => options.UseSqlite($"Data Source={path}"));

        // Register the needed dependencies
        builder.Services.AddScoped<ProjectService>();
        builder.Services.AddScoped<ProjectMenu>();
        builder.Services.AddScoped<TimeEntryService>();
        builder.Services.AddScoped<TimeEntryMenu>();
        builder.Services.AddScoped<ReportService>();
        builder.Services.AddScoped<ReportMenu>();
        builder.Services.AddScoped<MainMenu>();
        
        IHost host = builder.Build();
        using var scope = host.Services.CreateScope();
        
        // Adds menus to scope
        var mainMenu = scope.ServiceProvider.GetRequiredService<MainMenu>();
        
        // Migrate database if it doesn't exist
        var db = scope.ServiceProvider.GetRequiredService<TimeFlowDbContext>();
        db.Database.Migrate();

        mainMenu.Show();
    }
}