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

public class MainMenu
{
    private readonly ProjectMenu _projectMenu;
    private readonly TimeEntryMenu _timeEntryMenu;

    public MainMenu(ProjectMenu projectMenu, TimeEntryMenu timeEntryMenu)
    {
        _projectMenu = projectMenu;
        _timeEntryMenu = timeEntryMenu;
    }
    
    public void Show()
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
            int maxOption = (int)Enum.GetValues<MainMenuOption>().Last();
            int choice = ConsoleInputService.PromptMenuChoice(minOption, maxOption);

            switch ((MainMenuOption)choice)
            {
                case MainMenuOption.Projects:
                    _projectMenu.Show();
                    break;
                case MainMenuOption.TimeEntries:
                    _timeEntryMenu.Show();
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