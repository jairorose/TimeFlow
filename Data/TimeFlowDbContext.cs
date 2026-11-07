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
        
        string fileName = "TimeFlowDev.db";
        string folder = Directory.GetCurrentDirectory();
        Directory.CreateDirectory(folder + "/DB/");

        string path = Path.Combine(folder, "DB", fileName);

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