namespace TimeManagementSystem.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}