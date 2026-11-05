namespace TimeFlow.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; private set; }

    public const int NameMaxLength = 45;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();

    public Project(string name)
    {
        name = NormalizeName(name);

        string? error = GetNameError(name);

        if (error != null) throw new ArgumentException(error, nameof(name));
        
        Name = name;
    }

    public void Rename(string name)
    {
        name = NormalizeName(name);

        string? error = GetNameError(name);
        
        if (error != null) throw new ArgumentException(error, nameof(name));
        
        Name = name;
    }

    public static string NormalizeName(string? name)
    {
        if (String.IsNullOrEmpty(name))
        {
            return "";
        }
        
        return name.Trim();
    }

    public static string? GetNameError(string? name)
    {
        name = NormalizeName(name);

        if (name == "")
            return "Project name cannot be empty.";

        if (name.Length > NameMaxLength)
            return $"Project name is longer than {NameMaxLength} characters.";

        return null;
    }
}