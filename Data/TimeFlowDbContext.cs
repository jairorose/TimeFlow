using Microsoft.EntityFrameworkCore;
using TimeFlow.Models;

namespace TimeFlow.Data;

public class TimeFlowDbContext : DbContext
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        
        //var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //var dbFolder = Path.Combine(folder, "TimeFlow");

        //Directory.CreateDirectory(dbFolder);

        //var path = Path.Combine(dbFolder, "TimeFlowDev.db");
        var path = "/Users/jairo419/Documents/Software ontwikkeling/Coderen/TimeFlow development/TimeFlow/DB/TimeFlowDev.db";

        optionsBuilder.UseSqlite($"Data Source={path}");
    }

    public TimeFlowDbContext() { }

    public TimeFlowDbContext(DbContextOptions<TimeFlowDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimeEntry>()
            .HasOne(entry => entry.Project)
            .WithMany(project => project.TimeEntries)
            .HasForeignKey(entry => entry.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}