namespace TimeManagementSystem.Services.Validators;

public static class StringValidator
{
    public static bool GetValidString(string userInput, out string validString, int maxLength = 45)
    {
        validString = userInput.Trim(' ');
            
        if (String.IsNullOrEmpty(validString))
        {
            return false;
        }

        if (validString.Length > maxLength)
        {
            return false;
        }

        return true;
    }
}