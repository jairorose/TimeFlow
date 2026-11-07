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
    private readonly TimeFlowDbContext _db;

    public ProjectService(TimeFlowDbContext db)
    {
        _db = db;
    }
    
    public List<Project> GetAll()
    {
        List<Project> projects = _db.Projects.ToList();

        return projects;
    }

    public OperationResult Create(string name)
    {
        string normalizedName = Project.NormalizeName(name);

        string? error = GetProjectNameError(normalizedName);

        if (error != null)
            return OperationResult.Failure(error);
        
        Project project = new Project(normalizedName);

        _db.Projects.Add(project);
        _db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult Update(int id, string name)
    {
        string normalizedName = Project.NormalizeName(name);
        
        string? error = GetProjectNameError(normalizedName, id);

        if (error != null)
            return OperationResult.Failure(error);

        Project? project = _db.Projects.Find(id);

        if (project == null)
            return OperationResult.Failure($"Project with id {id} not found.");

        project.Rename(normalizedName);
        _db.SaveChanges();

        return OperationResult.Success();
    }

    public ProjectDeletionResult Delete(int id)
    {
        Project? project = _db.Projects.Find(id);

        if (project is null)
        {
            return ProjectDeletionResult.NotFound;
        }

        if (_db.TimeEntries.Any(entry => entry.ProjectId == id))
        {
            return ProjectDeletionResult.HasTimeEntries;
        }

        _db.Projects.Remove(project);
        _db.SaveChanges();

        return ProjectDeletionResult.Deleted;
    }

    public bool IsNameAvailable(string name, int? excludeId = null)
    {
        IQueryable<Project> query = _db.Projects;

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
        return _db.Projects.Any(p => p.Id == id);
    }
}