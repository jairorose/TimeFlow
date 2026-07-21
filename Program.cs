using Microsoft.EntityFrameworkCore;

using TimeFlow.Menus;
using TimeFlow.Services.Validators;

class Program
{
    static void Main(string[] args)
    {
        if (MainMenu.Show())
        {
            return;
        }
    }
}