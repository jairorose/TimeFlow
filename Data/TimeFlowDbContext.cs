using Microsoft.EntityFrameworkCore;
using TimeFlow.Models;

namespace TimeFlow.Data;

public class TimeFlowDbContext : DbContext
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dbFolder = Path.Combine(folder, "TimeFlow");

        Directory.CreateDirectory(dbFolder);

        var path = Path.Combine(dbFolder, "TimeFlow.db");

        optionsBuilder.UseSqlite($"Data Source={path}");
    }
}