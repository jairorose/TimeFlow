namespace TimeManagementSystem.Menus;

using TimeManagementSystem.Models;
using TimeManagementSystem.Services;
using TimeManagementSystem.Services.Validators;

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

        int choice= -1;

        while (true)
        {
            string readInput = Console.ReadLine();

            int minOption = 0;
            int maxOption = 4;

            choice = MenuValidator.GetValidMenuChoice(readInput, minOption, maxOption);

            if (choice != -1)
            {
                break;
            }
            else
            {
                Console.WriteLine($"Invalid menu option. Please select a number between {minOption} and {maxOption}");
            }
        }

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
                MainMenu.Show();
                break;
            default:
                // Add exception
                break;
        }
    }

    private static void CreateProject()
    {
        Console.WriteLine("========== Create Project ==========");
        Console.WriteLine();
        Console.WriteLine("Enter project name:");

        string projectName;
        bool validInput;

        do
        {
            string readInput = Console.ReadLine();
            
            validInput = StringValidator.GetValidString(readInput, out projectName);
            
            if (!validInput)
            {
                Console.WriteLine("Invalid input. Value cannot be empty and must be 45 characters or less.");
            }

        } while (!validInput);

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

        int projectNumber;

        while (true)
        {
            string readInput = Console.ReadLine();

            int minOption = 1;
            int maxOption = projects.Count;

            projectNumber = MenuValidator.GetValidMenuChoice(readInput, minOption, maxOption);

            if (projectNumber != -1)
            {
                break;
            }
            else
            {
                Console.WriteLine($"Invalid menu option. Please select a number between {minOption} and {maxOption}");
            }
        }

        for (int i = 0; i < projects.Count; i++)
        {
            if (projectNumber-1 == i)
            {
                Console.WriteLine();
                Console.WriteLine($"Current project name: {projects[i].Name}");
                Console.WriteLine();
                Console.WriteLine("Enter new project name:");

                string projectName;
                bool validInput;

                do
                {
                    string readInput = Console.ReadLine();
                    
                    validInput = StringValidator.GetValidString(readInput, out projectName);
                    
                    if (!validInput)
                    {
                        Console.WriteLine("Invalid input. Value cannot be empty and must be 45 characters or less.");
                    }

                } while (!validInput);

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

        string readInput = Console.ReadLine(); // Validate

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

                string choice;
                bool validInput;

                do
                {
                    readInput = Console.ReadLine();
                    
                    validInput = StringValidator.GetValidString(readInput, out choice);
                    
                    if (!validInput)
                    {
                        Console.WriteLine("Invalid input.");
                    }

                } while (!validInput);

                Console.WriteLine();

                if (choice == "Y")
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