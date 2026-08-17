namespace TimeFlow.Services;

using TimeFlow.Data;
using TimeFlow.Models;
using TimeFlow.Services.Results;

public enum ProjectDeletionResult
{
    Deleted,
    NotFound,
    HasTimeEntries
}

public class ProjectService
{
    public List<Project> GetAll()
    {
        using var db = new TimeFlowDbContext();

        List<Project> projects = db.Projects.ToList();

        return projects;
    }

    public void Create(string name)
    {
        using var db = new TimeFlowDbContext(); // Make connection to the database
                
        var project = new Project
        {
            Name = name
        };

        db.Projects.Add(project);
        db.SaveChanges();
    }

    public OperationResult Update(int id, string name)
    {
        using var db = new TimeFlowDbContext();

        Project? project = db.Projects.Find(id);

        if (project == null)
        {
            return OperationResult.Failure($"Project with id {id} not found.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return OperationResult.Failure("Project name cannot be empty.");
        }

        project.Name = name;
        db.SaveChanges();

        return OperationResult.Success();
    }

    public ProjectDeletionResult Delete(int id)
    {
        using var db = new TimeFlowDbContext();

        Project? project = db.Projects.Find(id);

        if (project is null)
        {
            return ProjectDeletionResult.NotFound;
        }

        if (db.TimeEntries.Any(entry => entry.ProjectId == id))
        {
            return ProjectDeletionResult.HasTimeEntries;
        }

        db.Projects.Remove(project);
        db.SaveChanges();

        return ProjectDeletionResult.Deleted;
    }

    public bool ValidateProjectName(string name)
    {
        List<Project> projects = GetAll();

        foreach (Project project in projects)
        {
            if (project.Name == name)
            {
                return false;
            }
        }

        return true;
    }
}