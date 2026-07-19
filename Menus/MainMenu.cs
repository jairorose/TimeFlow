using TimeManagementSystem.Services.Validators;

namespace TimeManagementSystem.Menus;

public static class MainMenu
{
    public static void Show()
    {
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
                    ProjectMenu.Show();
                    break;
                case 2:
                    TimeEntryMenu.Show();
                    break;
                case 3:
                    ReportMenu.Show();
                    break;
                case 0:
                    //Exit();
                    break;
                default:
                    throw new InvalidOperationException("Unexpected menu option"); 
            }

            // Console.WriteLine();
            // Console.WriteLine("Press Enter to continue...");
            // readInput = Console.ReadLine();
        } while (true);
    }
}