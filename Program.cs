using Microsoft.EntityFrameworkCore;

using TimeManagementSystem.Data;
using TimeManagementSystem.Models;

class Program
{
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
                    ShowTimeEntryMenu();
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

    static void ShowTimeEntryMenu()
    {
        Console.WriteLine("========== Time Entries ==========");
        Console.WriteLine();
        Console.WriteLine("1. Add Time Entry");
        Console.WriteLine("2. View Time Entries");
        Console.WriteLine("3. Edit Time Entry");
        Console.WriteLine("4. Delete Time Entry");
        Console.WriteLine("5. Back to Main Menu");
        Console.WriteLine();
        Console.WriteLine("Select an option:");

        string readInput = Console.ReadLine();

        switch (readInput)
        {
            case "1":
                CreateTimeEntry();
                break;
            case "2":
                ShowTimeEntries();
                break;
            case "3":
                //EditProject();
                break;
            case "4":
                //DeleteProject();
                break;
            case "5":
                //ShowMainMenu();
                break;
            default:
                //
                break;
        }
    }
    static void CreateTimeEntry()
    {
        using var db = new TimeFlowDbContext();
        
        // First show projects to attach the time entry to
        Console.WriteLine("========== Create Time Entry ==========");
        Console.WriteLine();
        Console.WriteLine("Available Projects:");

        var projects = db.Projects.ToList();
        int projectCounter = 0;

        foreach (var project in projects)
        {
            projectCounter++;
            Console.WriteLine($"{projectCounter}. {project.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Select a project number");

        string userInput = Console.ReadLine();
        int projectIndex = Int32.Parse(userInput) - 1;
        int projectId = projects[projectIndex].Id;

        // Get the description of the time entry
        Console.WriteLine();
        Console.WriteLine("Add a description:");
        
        string description = Console.ReadLine();

        // Get the start time of the time entry
        Console.WriteLine();
        Console.WriteLine("Start Date & Time (dd-MM-yyyy HH:mm):");

        string readInput = Console.ReadLine();
        DateTime startTime = DateTime.ParseExact(readInput, "dd-MM-yyyy HH:mm", null);

        // Get the end time of the time entry
        Console.WriteLine();
        Console.WriteLine("End Date & Time   (dd-MM-yyyy HH:mm):");

        readInput = Console.ReadLine();
        DateTime endTime = DateTime.ParseExact(readInput, "dd-MM-yyyy HH:mm", null);

        // Save data to database
        var timeEntry = new TimeEntry
        {
            Description = description,
            StartTime = startTime,
            EndTime = endTime,
            ProjectId = projectId
        };

        db.TimeEntries.Add(timeEntry);
        db.SaveChanges();

        Console.WriteLine();
        Console.WriteLine("Time entry added successfully!");
    }

    static void ShowTimeEntries()
    {
        Console.WriteLine("========== Create Time Entry ==========");

        using var db = new TimeFlowDbContext();

        var timeEntries = db.TimeEntries
            .Include(t => t.Project)
            .ToList();
        int timeEntryCounter = 0;

        foreach (var timeEntry in timeEntries)
        {  
            timeEntryCounter++;

            TimeSpan duration = timeEntry.EndTime.Subtract(timeEntry.StartTime);

            Console.WriteLine();
            Console.WriteLine($"{timeEntryCounter}.");
            Console.WriteLine($"Project: {timeEntry.Project.Name}");
            Console.WriteLine($"Description: {timeEntry.Description}");
            Console.WriteLine($"Start: {timeEntry.StartTime}");
            Console.WriteLine($"Duration: {(int)duration.TotalHours:00}:{(int)duration.Minutes:00}");
        }
    }
}