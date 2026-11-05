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

    public OperationResult Create(string name)
    {
        string normalizedName = Project.NormalizeName(name);

        string? error = GetProjectNameError(normalizedName);

        if (error != null)
            return OperationResult.Failure(error);
        
        Project project = new Project(normalizedName);

        using var db = new TimeFlowDbContext(); // Make connection to the database
        db.Projects.Add(project);
        db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult Update(int id, string name)
    {
        string normalizedName = Project.NormalizeName(name);
        
        string? error = GetProjectNameError(normalizedName, id);

        if (error != null)
            return OperationResult.Failure(error);

        using var db = new TimeFlowDbContext();
        Project? project = db.Projects.Find(id);

        if (project == null)
            return OperationResult.Failure($"Project with id {id} not found.");

        project.Rename(normalizedName);
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

    public bool IsNameAvailable(string name, int? excludeId = null)
    {
        using var db = new TimeFlowDbContext();
        
        IQueryable<Project> query = db.Projects;

        if (excludeId != null)
            query = query.Where(p => p.Id != excludeId);

        List<string> existingNames = query.Select(p => p.Name).ToList();

        bool nameExists = existingNames.Any(existing => string.Equals(existing, name, StringComparison.OrdinalIgnoreCase));

        return !nameExists;
    }

    public string? GetProjectNameError(string name, int? projectId = null)
    {
        string normalizedName = Project.NormalizeName(name);

        string? error = Project.GetNameError(normalizedName);

        if (error != null)
            return error;
        
        if (projectId != null && !ProjectExists(projectId.Value))
            return $"Project with id {projectId} not found.";
        
        if (!IsNameAvailable(normalizedName, projectId)) 
            return "Project name already exists.";
        
        return null;
    }

    private bool ProjectExists(int id)
    {
        using var db = new TimeFlowDbContext();
        return db.Projects.Any(p => p.Id == id);
    }
}