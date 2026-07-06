namespace TimeManagementSystem.Menus;

public static class MainMenu
{
    public static void Show()
    {
        do
        {
            Console.WriteLine("1. Projects");
            Console.WriteLine("2. Time Entries");
            Console.WriteLine("3. Reports");
            Console.WriteLine("4. Settings");
            Console.WriteLine("5. Exit");
            Console.WriteLine("");
            Console.WriteLine("Select an option");

            string readInput = Console.ReadLine();

            switch (readInput)
            {
                case "1":
                    ProjectMenu.Show();
                    break;
                case "2":
                    TimeEntryMenu.Show();
                    break;
                case "3":
                    ReportMenu.Show();
                    break;
                case "4":
                    //ShowSettingMenu();
                    break;
                case "5":
                    //Exit();
                    break;
                default:
                    Console.WriteLine("Optie niet herkent");
                    break; 
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            readInput = Console.ReadLine();
        } while (true);
    }
}