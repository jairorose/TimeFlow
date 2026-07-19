using Microsoft.EntityFrameworkCore;

using TimeManagementSystem.Menus;
using TimeManagementSystem.Services.Validators;

class Program
{
    static void Main(string[] args)
    {
        // string input = "";
        // string input2 = "        ";
        // string input3 = "   Fitness   ";
        // string input4 = "sjahdiuahsdiahsdhauisdhasdhiahiusdhuaidhuhadijsdhdhsaduasho";

        // Console.WriteLine(StringValidator.GetValidString(input, out string output) + ", (" + output + ")");
        // Console.WriteLine(StringValidator.GetValidString(input2, out output) + ", (" + output + ")");
        // Console.WriteLine(StringValidator.GetValidString(input3, out output) + ", (" + output + ")");
        // Console.WriteLine(StringValidator.GetValidString(input4, out output) + ", (" + output + ")");
        MainMenu.Show();
    }
}