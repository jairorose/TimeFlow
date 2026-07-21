using Microsoft.EntityFrameworkCore;
using TimeFlow.Data;
using TimeFlow.Services;

namespace TimeFlow.Menus;

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

            Console.WriteLine("1. Projects");
            Console.WriteLine("2. Time Entries");
            Console.WriteLine("3. Reports");
            Console.WriteLine();
            Console.WriteLine("0. Exit");
            Console.WriteLine("");
            Console.WriteLine("Select an option:");

            int minOption = 0;
            int maxOption = 3;
            int choice = ConsoleInputService.PromptMenuChoice(minOption, maxOption);

            switch (choice)
            {
                case 1:
                    ProjectMenu.Show();
                    break;
                case 2:
                    TimeEntryMenu.Show();
                    break;
                case 3:
                    ReportMenu.Show();
                    break;
                case 0:
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