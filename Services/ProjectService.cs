namespace TimeManagementSystem.Services;

using System.Data.Common;
using TimeManagementSystem.Data;
using TimeManagementSystem.Models;
public static class ProjectService
{
    public static void Create(string name)
    {
        using var db = new TimeFlowDbContext(); // Make connection to the database
                
        var project = new Project
        {
            Name = name
        };

        db.Projects.Add(project);
        db.SaveChanges();
    }

    public static List<Project> GetAll()
    {
        using var db = new TimeFlowDbContext();

        List<Project> projects = db.Projects.ToList();

        return projects;
    }

    public static void Update(int id, string name)
    {
        using var db = new TimeFlowDbContext();

        List<Project> projects = GetAll();

        projects[id].Name = name;
        db.SaveChanges();
    }

    public static void Delete(int id)
    {
        using var db = new TimeFlowDbContext();

        List<Project> projects = GetAll();

        projects.Remove(projects[id]);
        db.SaveChanges();
    }
}