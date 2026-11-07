using TimeFlow.Models;

namespace TimeFlow.Services;

using TimeFlow.Services.Validators;

public static class ConsoleInputService
{
    public static int PromptMenuChoice(int min, int max)
    {
        int choice;

        while (true)
        {
            string? readInput = Console.ReadLine();

            choice = MenuValidator.GetValidMenuChoice(readInput, min, max);

            if (choice != -1)
            {
                break;
            }
            else if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Maximum menu option cannot be lower than the minimum.");
            }
            else
            {
                Console.WriteLine($"Invalid menu option. Please select a number between {min} and {max}");
            }
        }

        return choice;
    }

    public static string PromptValidProjectName(ProjectService projectService, int? projectId = null)
    {
        string projectName;
        bool validInput = false;

        do
        {
            string? readInput = Console.ReadLine();
            projectName = Project.NormalizeName(readInput);

            string? error = projectService.GetProjectNameError(projectName, projectId);

            if (error != null)
            {
                Console.WriteLine(error);
            }
            else
            {
                validInput = true;
            }

        } while (!validInput);

        return projectName;
    }

    public delegate bool TryValidate<T>(string? input, out T value);
    
    public static T PromptUntilValid<T>(TryValidate<T> validate, string errorMessage)
    {

        while (true)
        {
            string? input = Console.ReadLine();

            if (validate(input, out T value))
            {
                return value;
            }

            Console.WriteLine(errorMessage);
        }
    }

    public static DateTime PromptValidStartTime()
    {
        DateTime newStartTime;

        while (true)
        {
            string? readInput = Console.ReadLine();

            bool validDateTime = DateTimeValidator.GetValidDateTime(readInput, out newStartTime);

            if (validDateTime)
            {
                return newStartTime;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use following format: dd-MM-yyyy HH:mm (e.g. 19-07-2026 23:59)");
            }
        }
    }

    public static DateTime PromptValidStartTimeChange(DateTime currentEndTime)
    {
        while (true)
        {
            DateTime startTime = PromptValidStartTime();

            TimeEntryService timeEntryService = new TimeEntryService();

            bool validDateTime = timeEntryService.ValidateStartTime(startTime, currentEndTime);

            if (!validDateTime)
            {
                Console.WriteLine("Invalid date. Make sure start time is earlier then end time and duration is not longer then 24 hours.");
            }
            else
            {
                return startTime;
            }
        }
    }

    public static DateTime PromptValidEndTime(DateTime currentStartTime)
    {
        DateTime newEndTime;
        
        while (true)
        {
            TimeEntryService timeEntryService = new TimeEntryService();

            string? readInput = Console.ReadLine();

            bool validDateTime = DateTimeValidator.GetValidDateTime(readInput, out newEndTime);

            if (validDateTime)
            {
                validDateTime = timeEntryService.ValidateStartTime(currentStartTime, newEndTime);

                if (!validDateTime)
                {
                    Console.WriteLine("Invalid date. Make sure start time is earlier then end time and duration is not longer then 24 hours.");
                }
                else
                {
                    return newEndTime;
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use following format: dd-MM-yyyy HH:mm (e.g. 19-07-2026 23:59)");
            }
        }
    }
}