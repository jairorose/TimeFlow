namespace TimeManagementSystem.Services.Validators;

public static class MenuValidator
{
    public static int GetValidMenuChoice(string userInput, int min, int max)
    {
        int menuOption = -1;

        if (String.IsNullOrEmpty(userInput))
        {
            return menuOption;
        }

        bool parseInt = Int32.TryParse(userInput, out menuOption);

        if (parseInt)
        {
            if (!(menuOption >= min && menuOption <= max))
            {
                menuOption = -1;
            }
        }
        else
        {
            menuOption = -1;
        }
        
        return menuOption;
    }
}