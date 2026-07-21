namespace TimeFlow.Menus;

using TimeFlow.Models;
using TimeFlow.Services;
using TimeFlow.Services.Validators;

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
        Console.WriteLine();
        Console.WriteLine("0. Back to Main Menu");
        Console.WriteLine();
        Console.WriteLine("Select an option:");

        int minOption = 0;
        int maxOption = 4;
        int choice = ConsoleInputService.PromptMenuChoice(minOption, maxOption);

        switch (choice)
        {
            case 1:
                CreateProject();
                break;
            case 2:
                ShowProjects();
                break;
            case 3:
                EditProject();
                break;
            case 4:
                DeleteProject();
                break;
            case 0:
                return;
            default:
                throw new InvalidOperationException("Unexpected menu option.");
        }
    }

    private static void CreateProject()
    {
        Console.WriteLine("========== Create Project ==========");
        Console.WriteLine();
        Console.WriteLine("Enter project name:");

        string projectName = ConsoleInputService.PromptValidProjectName();

        projectService.Create(projectName);

        Console.WriteLine($"Project '{projectName}' created successfully!");
        Console.WriteLine();
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

        int minOption = 1;
        int maxOption = projects.Count;
        int projectNumber = ConsoleInputService.PromptMenuChoice(minOption, maxOption);

        for (int i = 0; i < projects.Count; i++)
        {
            if (projectNumber-1 == i)
            {
                Console.WriteLine();
                Console.WriteLine($"Current project name: {projects[i].Name}");
                Console.WriteLine();
                Console.WriteLine("Enter new project name:");

                string projectName = ConsoleInputService.PromptValidProjectName();

                projectService.Update(projects[i].Id, projectName);

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

        int minOption = 1;
        int maxOption = projects.Count;
        int projectNumber = ConsoleInputService.PromptMenuChoice(minOption, maxOption);

        for(int i = 0; i < projects.Count; i++)
        {
            if (projectNumber-1 == i)
            {
                Console.WriteLine();
                Console.WriteLine($"Are you sure you want to delete: {projects[i].Name}?");
                Console.WriteLine();
                Console.WriteLine("(Y/N):");

                string choice = ConsoleInputService.PromptUntilValid<string>
                    (StringValidator.GetValidString, "Invalid input.");

                Console.WriteLine();

                if (choice.ToLower() == "y")
                {
                    projectService.Delete(projects[i].Id);

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