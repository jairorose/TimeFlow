namespace TimeManagementSystem.Services.Validators;

public static class StringValidator
{
    public static bool GetValidString(string userInput, out string validString)
    {
        validString = userInput.Trim(' ');
            
        if (String.IsNullOrEmpty(validString))
        {
            return false;
        }

        int maxLength = 45;

        if (validString.Length > maxLength)
        {
            return false;
        }

        return true;
    }
}