using Microsoft.EntityFrameworkCore;
using TimeFlow.Data;
using TimeFlow.Services;

namespace TimeFlow.Menus;

public enum MainMenuOption
{
    Exit,
    Projects,
    TimeEntries,
    Reports
}

public static class MainMenu
{
    public static void Show()
    {
        using var db = new TimeFlowDbContext();
        db.Database.Migrate();

        do
        {
            Console.WriteLine("");
            Console.WriteLine("========================================");
            Console.WriteLine("         TimeFlow v1.0");
            Console.WriteLine("    Personal Time Management System");
            Console.WriteLine("========================================");
            Console.WriteLine("");
            Console.WriteLine("Track your time. Own your workflow.");
            Console.WriteLine("");

            Console.WriteLine($"{(int)MainMenuOption.Projects}. Projects");
            Console.WriteLine($"{(int)MainMenuOption.TimeEntries}. Time Entries");
            Console.WriteLine($"{(int)MainMenuOption.Reports}. Reports");
            Console.WriteLine();
            Console.WriteLine($"{(int)MainMenuOption.Exit}. Exit");
            Console.WriteLine("");
            Console.WriteLine("Select an option:");

            int minOption = 0;
            int maxOption = 3;
            int choice = ConsoleInputService.PromptMenuChoice(minOption, maxOption);

            switch ((MainMenuOption)choice)
            {
                case MainMenuOption.Projects:
                    ProjectMenu.Show();
                    break;
                case MainMenuOption.TimeEntries:
                    TimeEntryMenu.Show();
                    break;
                case MainMenuOption.Reports:
                    ReportMenu.Show();
                    break;
                case MainMenuOption.Exit:
                    return;
                default:
                    throw new InvalidOperationException("Unexpected menu option."); 
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        } while (true);
    }
}