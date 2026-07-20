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
            else
            {
                Console.WriteLine($"Invalid menu option. Please select a number between {min} and {max}");
            }
        }

        return choice;
    }

    public static string PromptValidProjectName()
    {
        string projectName;
        bool validInput;

        ProjectService projectService = new ProjectService();

        do
        {
            string readInput = Console.ReadLine();
            
            validInput = StringValidator.GetValidString(readInput, out projectName);
            
            if (validInput)
            {
                validInput = projectService.ValidateProjectName(projectName);

                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Make sure project name doesn't already exist.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Value cannot be empty and must be 45 characters or less.");
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
            //TimeEntryService timeEntryService = new TimeEntryService();

            string? readInput = Console.ReadLine();

            bool validDateTime = DateTimeValidator.GetValidDateTime(readInput, out newStartTime);

            if (validDateTime)
            {
                // validDateTime = timeEntryService.ValidateStartTime(newStartTime, currentEndTime);

                // if (!validDateTime)
                // {
                //     Console.WriteLine("Invalid date. Make sure start time is earlier then end time and duration is not longer then 24 hours.");
                // }
                // else
                // {
                    return newStartTime;
                //}
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