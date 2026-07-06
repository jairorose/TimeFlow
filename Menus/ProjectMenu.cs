namespace TimeManagementSystem.Menus;

using TimeManagementSystem.Data;
using TimeManagementSystem.Models;

public static class ProjectMenu
{
    public static void Show()
    {
        Console.WriteLine("========== Projects ==========");
        Console.WriteLine();
        Console.WriteLine("1. Create Project");
        Console.WriteLine("2. View All Projects");
        Console.WriteLine("3. Edit Project");
        Console.WriteLine("4. Delete Project");
        Console.WriteLine("5. Back to Main Menu");
        Console.WriteLine();
        Console.WriteLine("Select an option:");

        string readInput = Console.ReadLine();

        switch (readInput)
        {
            case "1":
                CreateProject();
                break;
            case "2":
                ShowProjects();
                break;
            case "3":
                EditProject();
                break;
            case "4":
                DeleteProject();
                break;
            case "5":
                MainMenu.Show();
                break;
            default:
                //
                break;
        }
    }

    static void CreateProject()
    {   
        using var db = new TimeFlowDbContext(); // Make connection to the database

        string readInput = "";

        do
        {
            Console.WriteLine("========== Create Project ==========");
            Console.WriteLine();
            Console.WriteLine("Enter project name:");

            readInput = Console.ReadLine();
            readInput = readInput.Trim(' ');
            
            if (!string.IsNullOrEmpty(readInput))
            {
                string projectName = readInput;
                Console.WriteLine($"Project '{projectName}' created successfully!");
                Console.WriteLine(); // White space
                //projects.Add(projectName);
                
                var project = new Project
                {
                    Name = projectName
                };

                db.Projects.Add(project);
                db.SaveChanges();
            }

        } while (string.IsNullOrEmpty(readInput));
        
    }

    static void ShowProjects()
    {
        Console.WriteLine("========== Projects ==========");
        Console.WriteLine();

        int projectCounter = 0;

        using var db = new TimeFlowDbContext();

        var projects = db.Projects.ToList();

        foreach (var project in projects)
        {
            projectCounter++;
            Console.WriteLine($"[{projectCounter}] {project.Name}");
        }

        Console.WriteLine();
        Console.WriteLine($"Total Projects: {projectCounter}");
    }

    static void EditProject()
    {
        Console.WriteLine("========== Edit Project ==========");
        Console.WriteLine();
        Console.WriteLine("Available Projects: ");
        Console.WriteLine();

        int projectCounter = 1;

        using var db = new TimeFlowDbContext();

        var projects = db.Projects.ToList();

        foreach (var project in projects)
        {
            Console.WriteLine($"[{projectCounter}] {project.Name}");
            projectCounter++;
        }

        Console.WriteLine();
        Console.WriteLine("Select a project number");

        string readInput = Console.ReadLine();

        // Convert user input to real number
        int userInput = Int32.Parse(readInput);

        for(int i = 0; i < projects.Count; i++)
        {
            if (userInput-1 == i)
            {
                Console.WriteLine();
                Console.WriteLine($"Current project name: {projects[i].Name}");
                Console.WriteLine();
                Console.WriteLine("Enter new project name:");

                readInput = Console.ReadLine();

                projects[i].Name = readInput;
                db.SaveChanges();

                Console.WriteLine();
                Console.WriteLine("Project updated successfully!");
                Console.WriteLine();
            }
        }
    }

    static void DeleteProject()
    {
        Console.WriteLine("========== Delete Project ==========");
        Console.WriteLine();
        Console.WriteLine("Available Projects: ");
        Console.WriteLine();

        int projectCounter = 1;

        using var db = new TimeFlowDbContext();

        var projects = db.Projects.ToList();

        foreach (var project in projects)
        {
            Console.WriteLine($"[{projectCounter}] {project.Name}");
            projectCounter++;
        }

        Console.WriteLine();
        Console.WriteLine("Select a project number");

        string readInput = Console.ReadLine();

        // Convert user input to real number
        int userInput = Int32.Parse(readInput);

        for(int i = 0; i < projects.Count; i++)
        {
            if (userInput-1 == i)
            {
                Console.WriteLine();
                Console.WriteLine($"Are you sure you want to delete: {projects[i].Name}?");
                Console.WriteLine();
                Console.WriteLine("(Y/N):");

                readInput = Console.ReadLine();

                Console.WriteLine();

                if (readInput == "Y")
                {
                    db.Projects.Remove(projects[i]);
                    db.SaveChanges();

                    Console.WriteLine("Project deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Project deletion cancelled");
                }
                
                Console.WriteLine();
            }
        }
    }
}