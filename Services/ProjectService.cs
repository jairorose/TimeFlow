namespace TimeManagementSystem.Services;

using System.Data.Common;
using TimeManagementSystem.Data;
using TimeManagementSystem.Models;
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

        List<Project> projects = GetAll();

        projects[id].Name = name;
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        using var db = new TimeFlowDbContext();

        List<Project> projects = GetAll();

        projects.Remove(projects[id]);
        db.SaveChanges();
    }
}