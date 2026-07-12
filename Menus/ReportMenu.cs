using TimeManagementSystem.Models;
using TimeManagementSystem.Services;

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
        Console.WriteLine("5. Report by project");
        Console.WriteLine("6. Back to Main Menu");
        Console.WriteLine();
        Console.WriteLine("Select an option:");

        string readInput = Console.ReadLine();

        switch (readInput)
        {
            case "1":
                ShowDailyReport();
                break;
            case "2":
                //ShowWeeklyReport();
                break;
            case "3":
                ShowMonthlyReport();
                break;
            case "4":
                ShowYearlyReport();
                break;
            case "5":
                //ShowReportByProject();
                break;
            case "6":
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

        string userInput = Console.ReadLine();
        Console.WriteLine();

        DateTime day = DateTime.ParseExact(userInput, "dd-MM-yyyy", null);

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

    private static void ShowMonthlyReport()
    {
        Console.WriteLine();
        Console.WriteLine("========== Monthly Report ==========");
        Console.WriteLine();
        Console.Write("Enter date (mm-yyyy): ");

        string userInput = Console.ReadLine();
        Console.WriteLine();

        DateTime month = DateTime.ParseExact(userInput, "MM-yyyy", null);

        var timeEntries = reportService.GetEntriesByMonth(month);

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

    private static void ShowYearlyReport()
    {
        Console.WriteLine();
        Console.WriteLine("========== Yearly Report ==========");
        Console.WriteLine();
        Console.Write("Enter year (yyyy): ");

        string userInput = Console.ReadLine();
        Console.WriteLine();

        DateTime year = DateTime.ParseExact(userInput, "yyyy", null);

        var timeEntries = reportService.GetEntriesByYear(year);

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