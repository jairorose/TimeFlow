using Microsoft.EntityFrameworkCore;
using TimeManagementSystem.Models;

namespace TimeManagementSystem.Data;

public class TimeFlowDbContext : DbContext
{
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/Users/jairo419/Documents/Software ontwikkeling/Coderen/TimeManagementSystem/timeflow.db");
    }
}