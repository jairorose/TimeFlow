namespace TimeManagementSystem.Services;

using Microsoft.EntityFrameworkCore;
using TimeManagementSystem.Models;
using TimeManagementSystem.Data;

public static class TimeEntryService
{
    public static void CreateTimeEntry()
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

    public static void ShowTimeEntries()
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

    public static void EditTimeEntry()
    {
        Console.WriteLine("========== Edit Time Entry ==========");
        Console.WriteLine();
        Console.WriteLine("Available Time Entries: ");
        Console.WriteLine();

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

        Console.WriteLine();
        Console.WriteLine("Select a Time Entry: ");

        string userInput = Console.ReadLine();
        int timeEntryIndex = Int32.Parse(userInput) - 1;

        Console.WriteLine();
        Console.WriteLine("Current values:");
        Console.WriteLine();
        Console.WriteLine($"Description: {timeEntries[timeEntryIndex].Description}");
        Console.WriteLine($"Start: {timeEntries[timeEntryIndex].StartTime}");
        Console.WriteLine($"End: {timeEntries[timeEntryIndex].EndTime}");
        Console.WriteLine($"Project: {timeEntries[timeEntryIndex].Project.Name}");
        Console.WriteLine();
        Console.WriteLine("What would you like to update?");
        Console.WriteLine();
        Console.WriteLine("1. Description");
        Console.WriteLine("2. Start time");
        Console.WriteLine("3. End time");
        Console.WriteLine("4. Project");
        Console.WriteLine();
        Console.WriteLine("Select option:");
        
        userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1":
                Console.WriteLine($"Current description: {timeEntries[timeEntryIndex].Description}");
                Console.WriteLine();
                Console.WriteLine("New description:");
                string newDescription = Console.ReadLine();
                timeEntries[timeEntryIndex].Description = newDescription;
                break;
            case "2":
                Console.WriteLine($"Current start date & time (dd-MM-yyyy HH:mm): {timeEntries[timeEntryIndex].StartTime}");
                Console.WriteLine();
                Console.WriteLine("New start date & time:");
                string newStartTimeInput = Console.ReadLine();
                DateTime newStartTime = DateTime.ParseExact(newStartTimeInput, "dd-MM-yyyy HH:mm", null);
                timeEntries[timeEntryIndex].StartTime = newStartTime;
                break;
            case "3":
                Console.WriteLine($"Current end date & time (dd-MM-yyyy HH:mm): {timeEntries[timeEntryIndex].EndTime}");
                Console.WriteLine();
                Console.WriteLine("New end date & time:");
                string newEndTimeInput = Console.ReadLine();
                DateTime newEndTime = DateTime.ParseExact(newEndTimeInput, "dd-MM-yyyy HH:mm", null);
                timeEntries[timeEntryIndex].EndTime = newEndTime;
                break;
            case "4":
                Console.WriteLine("This feature is not possible yet.");
                //Console.WriteLine($"Current project: {timeEntries[timeEntryIndex].Project.Name}");
                //string newProject = Console.ReadLine();
                //timeEntries[timeEntryIndex] = newProject;
                break;
            default:
                // 
                break;
        }

        db.SaveChanges();
    }

    public static void DeleteTimeEntry()
    {
        Console.WriteLine("========== Delete Time Entry ==========");
        Console.WriteLine();
        Console.WriteLine("Available Time Entries: ");
        Console.WriteLine();

        int timeEntryCounter = 1;

        using var db = new TimeFlowDbContext();

        var timeEntries = db.TimeEntries
            .Include(t => t.Project)
            .ToList();

        foreach (var timeEntry in timeEntries)
        {
            TimeSpan duration = timeEntry.EndTime.Subtract(timeEntry.StartTime);

            Console.WriteLine();
            Console.WriteLine($"{timeEntryCounter}.");
            Console.WriteLine($"Project: {timeEntry.Project.Name}");
            Console.WriteLine($"Description: {timeEntry.Description}");
            Console.WriteLine($"Start: {timeEntry.StartTime}");
            Console.WriteLine($"Duration: {(int)duration.TotalHours:00}:{(int)duration.Minutes:00}");
            timeEntryCounter++;
        }

        Console.WriteLine();
        Console.WriteLine("Select a time entry number");

        string readInput = Console.ReadLine();

        // Convert user input to real number
        int userInput = Int32.Parse(readInput);

        for(int i = 0; i < timeEntries.Count; i++)
        {
            if (userInput-1 == i)
            {
                Console.WriteLine();
                Console.WriteLine($"Are you sure you want to delete: ");
                Console.WriteLine();
                Console.WriteLine($"Project: {timeEntries[i].Project.Name}");
                Console.WriteLine($"Description: {timeEntries[i].Description}");
                Console.WriteLine();
                Console.WriteLine("(Y/N):");

                readInput = Console.ReadLine();

                Console.WriteLine();

                if (readInput == "Y")
                {
                    db.TimeEntries.Remove(timeEntries[i]);
                    db.SaveChanges();

                    Console.WriteLine("Time Entry deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Time Entry deletion cancelled");
                }
                
                Console.WriteLine();
            }
        }
    }
}