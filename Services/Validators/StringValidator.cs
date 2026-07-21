namespace TimeFlow.Services.Validators;

public static class StringValidator
{
    public static bool GetValidString(string? userInput, out string validString)
    {   
        validString = "";

        if (!String.IsNullOrEmpty(userInput))
        {
            validString = userInput.Trim(' ');
        } 
        
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