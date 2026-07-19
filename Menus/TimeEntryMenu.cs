namespace TimeManagementSystem.Menus;

using TimeManagementSystem.Models;
using TimeManagementSystem.Services;
using TimeManagementSystem.Services.Validators;

public static class TimeEntryMenu
{
    private static readonly ProjectService projectService = new ProjectService();

    private static readonly TimeEntryService timeEntryService = new TimeEntryService();
    public static void Show()
    {
        Console.WriteLine("========== Time Entries ==========");
        Console.WriteLine();
        Console.WriteLine("1. Add Time Entry");
        Console.WriteLine("2. View Time Entries");
        Console.WriteLine("3. Edit Time Entry");
        Console.WriteLine("4. Delete Time Entry");
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
                CreateTimeEntry();
                break;
            case 2:
                ShowTimeEntries();
                break;
            case 3:
                EditTimeEntry();
                break;
            case 4:
                DeleteTimeEntry();
                break;
            case 0:
                MainMenu.Show();
                break;
            default:
                // Add exception
                break;
        }
    }

    private static void CreateTimeEntry()
    {   
        // First show projects to attach the time entry to
        Console.WriteLine("========== Create Time Entry ==========");
        Console.WriteLine();
        Console.WriteLine("Available Projects:");

        List<Project> projects = projectService.GetAll();
        int projectCounter = 0;

        foreach (Project project in projects)
        {
            projectCounter++;
            Console.WriteLine($"{projectCounter}. {project.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Select a project number");

        int projectNumber;

        // Validate user input
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

        int projectIndex = projectNumber - 1;
        int projectId = projects[projectIndex].Id;

        // Get the description of the time entry
        Console.WriteLine();
        Console.WriteLine("Add a description:");
        
        string description;
        bool validInput;

        do
        {
            string readInput = Console.ReadLine();
            
            validInput = StringValidator.GetValidString(readInput, out description);
            
            if (!validInput)
            {
                Console.WriteLine("Invalid input. Value cannot be empty and must be 45 characters or less.");
            }

        } while (!validInput);

        // Get the start time of the time entry
        Console.WriteLine();
        Console.WriteLine("Start Date & Time (dd-MM-yyyy HH:mm):");

        DateTime startTime;
        DateTime endTime;
        bool validDateTime;

        do
        {
            string readInput = Console.ReadLine();

            validDateTime = DateTimeValidator.GetValidDateTime(readInput, out startTime);

            if (!validDateTime)
            {
                Console.WriteLine("Invalid date format. Please use following format: dd-MM-yyyy HH:mm (e.g. 19-07-2026 23:59)");
            }
        } while (!validDateTime);

        // Get the end time of the time entry
        Console.WriteLine();
        Console.WriteLine("End Date & Time   (dd-MM-yyyy HH:mm):");

        do
        {
            string readInput = Console.ReadLine();

            validDateTime = DateTimeValidator.GetValidDateTime(readInput, out endTime);

            if (!validDateTime)
            {
                Console.WriteLine("Invalid date format. Please use following format: dd-MM-yyyy HH:mm (e.g. 19-07-2026 23:59)");
            }
        } while (!validDateTime);

        timeEntryService.Create(description, startTime, endTime, projectId);

        Console.WriteLine();
        Console.WriteLine("Time entry added successfully!");
    }

    private static void ShowTimeEntries()
    {
        Console.WriteLine("========== Create Time Entry ==========");

        List<TimeEntry> timeEntries = timeEntryService.GetAll();

        int timeEntryCounter = 0;

        foreach (TimeEntry timeEntry in timeEntries)
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

    private static void EditTimeEntry()
    {
        Console.WriteLine("========== Edit Time Entry ==========");
        Console.WriteLine();
        Console.WriteLine("Available Time Entries: ");
        Console.WriteLine();

        List<TimeEntry> timeEntries = timeEntryService.GetAll();

        int timeEntryCounter = 0;

        foreach (TimeEntry timeEntry in timeEntries)
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

        int timeEntryNumber;

        // Validate user input
        while (true)
        {
            string readInput = Console.ReadLine();

            int minOption = 1;
            int maxOption = timeEntries.Count;

            timeEntryNumber = MenuValidator.GetValidMenuChoice(readInput, minOption, maxOption);

            if (timeEntryNumber != -1)
            {
                break;
            }
            else
            {
                Console.WriteLine($"Invalid menu option. Please select a number between {minOption} and {maxOption}");
            }
        }

        int timeEntryIndex = timeEntryNumber - 1;

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
        Console.WriteLine("0. Back");
        Console.WriteLine();
        Console.WriteLine("Select option:");

        int choice = -1;

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

        bool validDateTime;

        switch (choice)
        {
            case 1:
                Console.WriteLine($"Current description: {timeEntries[timeEntryIndex].Description}");
                Console.WriteLine();
                Console.WriteLine("New description:");

                string newDescription;
                bool validInput;
                int maxLengthDescription = 75;

                do
                {
                    string readInput = Console.ReadLine();
                    
                    validInput = StringValidator.GetValidString(readInput, out newDescription, maxLengthDescription);
                    
                    if (!validInput)
                    {
                        Console.WriteLine($"Invalid input. Value cannot be empty and must be {maxLengthDescription} characters or less.");
                    }

                } while (!validInput);

                timeEntryService.UpdateDescription(timeEntries[timeEntryIndex].Id, newDescription);
                break;
            case 2:
                Console.WriteLine($"Current start date & time (dd-MM-yyyy HH:mm): {timeEntries[timeEntryIndex].StartTime}");
                Console.WriteLine();
                Console.WriteLine("New start date & time:");

                DateTime newStartTime;
                
                do
                {
                    string readInput = Console.ReadLine();

                    validDateTime = DateTimeValidator.GetValidDateTime(readInput, out newStartTime);

                    if (!validDateTime)
                    {
                        Console.WriteLine("Invalid date format. Please use following format: dd-MM-yyyy HH:mm (e.g. 19-07-2026 23:59)");
                    }
                } while (!validDateTime);

                timeEntryService.UpdateStartTime(timeEntries[timeEntryIndex].Id, newStartTime);
                break;
            case 3:
                Console.WriteLine($"Current end date & time (dd-MM-yyyy HH:mm): {timeEntries[timeEntryIndex].EndTime}");
                Console.WriteLine();
                Console.WriteLine("New end date & time:");

                DateTime newEndTime;
                
                do
                {
                    string readInput = Console.ReadLine();

                    validDateTime = DateTimeValidator.GetValidDateTime(readInput, out newEndTime);

                    if (!validDateTime)
                    {
                        Console.WriteLine("Invalid date format. Please use following format: dd-MM-yyyy HH:mm (e.g. 19-07-2026 23:59)");
                    }
                } while (!validDateTime);

                timeEntryService.UpdateEndTime(timeEntries[timeEntryIndex].Id, newEndTime);
                break;
            case 4:
                Console.WriteLine($"Current project: {timeEntries[timeEntryIndex].Project.Name}");
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

                Console.WriteLine("New Project:");
                
                int projectNumber;

                // Validate user input
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

                int projectId = projectNumber - 1; // -1 because zero based index

                timeEntryService.UpdateProject(timeEntries[timeEntryIndex].Id, projects[projectId].Id);
                break;
            case 0:
                break;
            default:
                throw new InvalidOperationException("Unexpected menu option");
        }
    }

    private static void DeleteTimeEntry()
    {
        Console.WriteLine("========== Delete Time Entry ==========");
        Console.WriteLine();
        Console.WriteLine("Available Time Entries: ");
        Console.WriteLine();

        int timeEntryCounter = 1;

        List<TimeEntry> timeEntries = timeEntryService.GetAll();

        foreach (TimeEntry timeEntry in timeEntries)
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

        int timeEntryNumber;

        while (true)
        {
            string readInput = Console.ReadLine();

            int minOption = 1;
            int maxOption = timeEntries.Count;

            timeEntryNumber = MenuValidator.GetValidMenuChoice(readInput, minOption, maxOption);

            if (timeEntryNumber != -1)
            {
                break;
            }
            else
            {
                Console.WriteLine($"Invalid menu option. Please select a number between {minOption} and {maxOption}");
            }
        }

        for(int i = 0; i < timeEntries.Count; i++)
        {
            if (timeEntryNumber-1 == i)
            {
                Console.WriteLine();
                Console.WriteLine($"Are you sure you want to delete: ");
                Console.WriteLine();
                Console.WriteLine($"Project: {timeEntries[i].Project.Name}");
                Console.WriteLine($"Description: {timeEntries[i].Description}");
                Console.WriteLine();
                Console.WriteLine("(Y/N):");

                string choice;
                bool validInput;

                do
                {
                    string readInput = Console.ReadLine();
                    
                    validInput = StringValidator.GetValidString(readInput, out choice);
                    
                    if (!validInput)
                    {
                        Console.WriteLine("Invalid input.");
                    }

                } while (!validInput);

                Console.WriteLine();

                if (choice == "Y")
                {
                    timeEntryService.DeleteTimeEntry(timeEntries[i].Id);

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