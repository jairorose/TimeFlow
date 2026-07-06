namespace TimeManagementSystem.Models;

public class TimeEntry
{
    public int Id { get; set; }

    public string Description { get; set; } = "";

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;
}