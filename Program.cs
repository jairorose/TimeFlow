// Projecten kunenn aanmaken
// Een project heeft: een naam

// Taak aan project kunnen hangen
// Een taak heeft: een begintijd, eindtijd, beschrijving, 

// CRUD: create, read, update, delete

// 1. Project aanmaken
// 2. Projecten bekijken
// 3. Timer starten
// 4. Timer stoppen
// 5. Tijd handmatig toevoegen
// 6. Week overzicht bekijken

//using Microsoft.VisualBasic;

using TimeManagementSystem.Data;
using TimeManagementSystem.Models;

class Program
{
    // Initalizing data
    //private static List<string> projects = new List<string> {"MMA", "Lezen", "School"}; // List of existing projects
    private static List<string[]> timeEntries = new List<string[]>();
    static void Main(string[] args)
    {
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("         TimeFlow v1.0");
        Console.WriteLine("    Personal Time Management System");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("Track your time. Own your workflow.");
        Console.WriteLine("");

        ShowMainMenu();
    }

    static void ShowMainMenu()
    {
        do
        {
            Console.WriteLine("1. Projects");
            Console.WriteLine("2. Time Entries");
            Console.WriteLine("3. Reports");
            Console.WriteLine("4. Settings");
            Console.WriteLine("5. Exit");
            Console.WriteLine("");
            Console.WriteLine("Select an option");

            string readInput = Console.ReadLine();

            switch (readInput)
            {
                case "1":
                    ShowProjectMenu();
                    break;
                case "2":
                    //ShowTimeEntryMenu();
                    break;
                case "3":
                    //ShowReportMenu();
                    break;
                case "4":
                    //ShowSettingMenu();
                    break;
                case "5":
                    //Exit();
                    break;
                default:
                    Console.WriteLine("Optie niet herkent");
                    break; 
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            readInput = Console.ReadLine();
        } while (true);
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
                Console.WriteLine($"Current project name: {projects[i]}");
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
                Console.WriteLine($"Are you sure you want to delete: {projects[i]}?");
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

    static void ShowProjectMenu()
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
                ShowMainMenu();
                break;
            default:
                //
                break;
        }
    }

    static List<string[]> CreateTimeEntry(List<string[]> timeEntries)
    {
        // Time entry aanmaken
        Console.WriteLine("Voeg een taak toe, begin met de beschrijving:");
        string readInput = Console.ReadLine();

        string timeEntryDescription = readInput;

        Console.WriteLine("Geef een begin tijd op: 23:59 23-07-2026");
        readInput = Console.ReadLine();

        string timeEntryStartTime = readInput;

        Console.WriteLine("Geef een eind tijd op: 23:59 23-07-2026");
        readInput = Console.ReadLine();

        string timeEntryEndTime = readInput;

        Console.WriteLine("Geef een gekoppelde project aan");
        readInput = Console.ReadLine();

        string projectName = readInput;

        Console.WriteLine($"Taak is aangemaakt: {timeEntryDescription}, van {timeEntryStartTime} tot {timeEntryEndTime} (Project: {projectName})");
        string[] timeEntry = {timeEntryDescription, timeEntryStartTime, timeEntryEndTime, projectName};

        timeEntries.Add(timeEntry);

        return timeEntries;
    }

    static void ShowTimeEntries(List<string[]> timeEntries)
    {
        Console.WriteLine("Dit zijn alle time entries:");

        foreach (string[] timeEntry in timeEntries)
        {
            string timeEntryDescription = timeEntry[0];
            string timeEntryStartTime = timeEntry[1];
            string timeEntryEndTime = timeEntry[2];
            string projectName = timeEntry[3];

            Console.WriteLine($"{timeEntryDescription}, vanaf {timeEntryStartTime} tot {timeEntryEndTime} (Project: {projectName})");
        }
    }
}