namespace TimeFlow.Services;

using TimeFlow.Data;
using TimeFlow.Models;
using TimeFlow.Services.Results;

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

    public OperationResult Delete(int id)
    {
        using var db = new TimeFlowDbContext();

        Project? project = db.Projects.Find(id);

        if (project == null)
        {
            return OperationResult.Failure($"Project with id {id} not found.");
        }

        db.Projects.Remove(project);
        db.SaveChanges();

        return OperationResult.Success();
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