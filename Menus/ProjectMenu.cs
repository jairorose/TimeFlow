namespace TimeFlow.Menus;

using TimeFlow.Models;
using TimeFlow.Services;
using TimeFlow.Services.Validators;

public enum ProjectMenuOption
{
    Back,
    CreateProject,
    ViewAllProjects,
    EditProject,
    DeleteProject
}

public static class ProjectMenu
{

    private static readonly ProjectService projectService = new ProjectService();
    public static void Show()
    {
        Console.WriteLine("========== Projects ==========");
        Console.WriteLine();
        Console.WriteLine($"{(int)ProjectMenuOption.CreateProject}. Create Project");
        Console.WriteLine($"{(int)ProjectMenuOption.ViewAllProjects}. View All Projects");
        Console.WriteLine($"{(int)ProjectMenuOption.EditProject}. Edit Project");
        Console.WriteLine($"{(int)ProjectMenuOption.DeleteProject}. Delete Project");
        Console.WriteLine();
        Console.WriteLine($"{(int)ProjectMenuOption.Back}. Back to Main Menu");
        Console.WriteLine();
        Console.WriteLine("Select an option:");

        int minOption = 0;
        int maxOption = (int)Enum.GetValues<ProjectMenuOption>().Last(); // Get last value of enum
        int choice = ConsoleInputService.PromptMenuChoice(minOption, maxOption);

        switch ((ProjectMenuOption)choice)
        {
            case ProjectMenuOption.CreateProject:
                CreateProject();
                break;
            case ProjectMenuOption.ViewAllProjects:
                ShowProjects();
                break;
            case ProjectMenuOption.EditProject:
                EditProject();
                break;
            case ProjectMenuOption.DeleteProject:
                DeleteProject();
                break;
            case ProjectMenuOption.Back:
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
                
                Console.WriteLine();

                var result = projectService.Update(projects[i].Id, projectName);

                if (result.Succeeded)
                {
                    Console.WriteLine("Project updated successfully!");
                }
                else
                {
                    Console.WriteLine($"Project is not able to update: {result.Error}");
                }

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
                    var result = projectService.Delete(projects[i].Id);

                    switch (result)
                    {
                        case ProjectDeletionResult.Deleted:
                            Console.WriteLine("Project deleted successfully!");
                            break;
                        case ProjectDeletionResult.NotFound:
                            Console.WriteLine("Project not found.");
                            break;
                        case ProjectDeletionResult.HasTimeEntries:
                            Console.WriteLine("Deletion not possible. Project still has time entries.");
                            break;
                    }
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