using TimeFlow.Menus;
using TimeFlow.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        var path = "/Users/jairo419/Documents/Software ontwikkeling/Coderen/TimeFlow development/TimeFlow/DB/TimeFlowDev.db";
        builder.Services.AddDbContext<TimeFlowDbContext>(options => options.UseSqlite($"Data Source={path}"));

        IHost host = builder.Build();
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TimeFlowDbContext>();
        db.Database.Migrate();

        MainMenu.Show();
    }
}