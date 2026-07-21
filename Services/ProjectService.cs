namespace TimeFlow.Services;

using TimeFlow.Data;
using TimeFlow.Models;
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

    public void Update(int id, string name)
    {
        using var db = new TimeFlowDbContext();

        Project? project = db.Projects.Find(id);

        if (project != null)
        {
            project.Name = name;
            db.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        using var db = new TimeFlowDbContext();

        Project? project = db.Projects.Find(id);

        if (project != null)
        {
            db.Projects.Remove(project);
            db.SaveChanges();
        }
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