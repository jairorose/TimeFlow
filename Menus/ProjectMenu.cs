namespace TimeManagementSystem.Menus;

using TimeManagementSystem.Models;
using TimeManagementSystem.Services;

public static class ProjectMenu
{

    private static readonly ProjectService projectService = new ProjectService();
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

    private static void CreateProject()
    {
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
                projectService.Create(readInput);

                Console.WriteLine($"Project '{readInput}' created successfully!");
                Console.WriteLine();
            }

        } while (string.IsNullOrEmpty(readInput));
    }

    private static void ShowProjects()
    {
        Console.WriteLine("========== Projects ==========");
        Console.WriteLine();

        int projectCounter = 0;

        List<Project> projects = projectService.GetAll();

        foreach (Project project in projects)
        {
            projectCounter++;
            Console.WriteLine($"[{projectCounter}] {project.Name}");
        }

        Console.WriteLine();
        Console.WriteLine($"Total Projects: {projectCounter}");
    }

    private static void EditProject()
    {
        Console.WriteLine("========== Edit Project ==========");
        Console.WriteLine();
        Console.WriteLine("Available Projects: ");
        Console.WriteLine();

        int projectCounter = 1;

        List<Project> projects = projectService.GetAll();

        foreach (Project project in projects)
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

                projectService.Update(i, readInput);

                Console.WriteLine();
                Console.WriteLine("Project updated successfully!");
                Console.WriteLine();
            }
        }
    }

    private static void DeleteProject()
    {
        Console.WriteLine("========== Delete Project ==========");
        Console.WriteLine();
        Console.WriteLine("Available Projects: ");
        Console.WriteLine();

        int projectCounter = 1;

        List<Project> projects = projectService.GetAll();

        foreach (Project project in projects)
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
                    projectService.Delete(i);

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