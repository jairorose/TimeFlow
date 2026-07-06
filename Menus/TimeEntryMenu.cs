namespace TimeManagementSystem.Menus;

using TimeManagementSystem.Data;
using TimeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using TimeManagementSystem.Services;

public static class TimeEntryMenu
{
    public static void Show()
    {
        Console.WriteLine("========== Time Entries ==========");
        Console.WriteLine();
        Console.WriteLine("1. Add Time Entry");
        Console.WriteLine("2. View Time Entries");
        Console.WriteLine("3. Edit Time Entry");
        Console.WriteLine("4. Delete Time Entry");
        Console.WriteLine("5. Back to Main Menu");
        Console.WriteLine();
        Console.WriteLine("Select an option:");

        string readInput = Console.ReadLine();

        switch (readInput)
        {
            case "1":
                TimeEntryService.CreateTimeEntry();
                break;
            case "2":
                TimeEntryService.ShowTimeEntries();
                break;
            case "3":
                TimeEntryService.EditTimeEntry();
                break;
            case "4":
                TimeEntryService.DeleteTimeEntry();
                break;
            case "5":
                MainMenu.Show();
                break;
            default:
                //
                break;
        }
    }
}