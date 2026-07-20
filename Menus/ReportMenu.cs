using TimeManagementSystem.Models;
using TimeManagementSystem.Services;
using TimeManagementSystem.Services.Validators;

namespace TimeManagementSystem.Menus;

public static class ReportMenu
{
    private static readonly ReportService reportService = new ReportService();

    public static void Show()
    {
        Console.WriteLine("========== Reports ==========");
        Console.WriteLine();
        Console.WriteLine("1. Daily Report");
        Console.WriteLine("2. Weekly Report");
        Console.WriteLine("3. Monthly Report");
        Console.WriteLine("4. Yearly Report");
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
                ShowDailyReport();
                break;
            case 2:
                ShowWeeklyReport();
                break;
            case 3:
                ShowMonthlyReport();
                break;
            case 4:
                ShowYearlyReport();
                break;
            case 0:
                MainMenu.Show();
                break;
            default:
                //
                break;
        }
    }

    private static void ShowDailyReport()
    {
        Console.WriteLine();
        Console.WriteLine("========== Daily Report ==========");
        Console.WriteLine();
        Console.Write("Enter date (dd-mm-yyyy): ");

        DateTime day = ConsoleInputService.PromptUntilValid<DateTime>
            (DateTimeValidator.GetValidDate, "Invalid date format. Please use following format: dd-MM-yyyy (e.g. 19-07-2026)");

        Console.WriteLine();

        List<TimeEntry> timeEntries = reportService.GetEntriesByDay(day);

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

    private static void ShowWeeklyReport()
    {
        Console.WriteLine();
        Console.WriteLine("========== Weekly Report ==========");
        Console.WriteLine();
        Console.Write("Enter date (dd-mm-yyyy): ");

        DateTime date = ConsoleInputService.PromptUntilValid<DateTime>
            (DateTimeValidator.GetValidDate, "Invalid date format. Please use following format: dd-MM-yyyy (e.g. 19-07-2026)");

        Console.WriteLine();

        // Show user start date of the week and end date of the week
        int difference = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;

        DateTime start = date.Date.AddDays(-difference);
        DateTime end = start.AddDays(6);
        Console.WriteLine($"Time entries monday {start:dd-MM-yyyy} to sunday {end:dd-MM-yyyy}:");

        var timeEntries = reportService.GetEntriesByWeek(date);

        PrintDurationPerProject(timeEntries);
    }

    private static void ShowMonthlyReport()
    {
        Console.WriteLine();
        Console.WriteLine("========== Monthly Report ==========");
        Console.WriteLine();
        Console.Write("Enter date (mm-yyyy): ");

        DateTime month = ConsoleInputService.PromptUntilValid<DateTime>
            (DateTimeValidator.GetValidMonth, "Invalid date format. Please use following format: MM-yyyy (e.g. 07-2026)");

        Console.WriteLine();

        var timeEntries = reportService.GetEntriesByMonth(month);

        PrintDurationPerProject(timeEntries);
    }

    private static void ShowYearlyReport()
    {
        Console.WriteLine();
        Console.WriteLine("========== Yearly Report ==========");
        Console.WriteLine();
        Console.Write("Enter year (yyyy): ");

        DateTime year = ConsoleInputService.PromptUntilValid<DateTime>
            (DateTimeValidator.GetValidYear, "Invalid date format. Please use following format: yyyy (e.g. 2026)");

        Console.WriteLine();

        var timeEntries = reportService.GetEntriesByYear(year);

        PrintDurationPerProject(timeEntries);
    }

    private static void PrintDurationPerProject(List<IGrouping<Project, TimeEntry>> timeEntries)
    {
        foreach (var project in timeEntries)
        {
            Console.WriteLine();
            Console.WriteLine(project.Key.Name);

            TimeSpan duration = TimeSpan.Zero;

            foreach (var timeEntry in project.Key.TimeEntries)
            {
                duration += timeEntry.EndTime.Subtract(timeEntry.StartTime);
            }

            Console.WriteLine($"Total time: {duration}");
        }
    }
}