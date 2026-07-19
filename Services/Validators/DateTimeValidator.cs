namespace TimeManagementSystem.Services.Validators;

using System.Globalization;

public static class DateTimeValidator
{
    public static bool GetValidDateTime(string userInput, out DateTime date)
    {
        date = DateTime.MinValue;

        if (String.IsNullOrEmpty(userInput))
        {
            return false;
        }

        bool validation = DateTime.TryParseExact(userInput, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return validation;
    }

    public static bool GetValidDate(string userInput, out DateTime date)
    {
        date = DateTime.MinValue;

        if (String.IsNullOrEmpty(userInput))
        {
            return false;
        }

        bool validation = DateTime.TryParseExact(userInput, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return validation;
    }

    public static bool GetValidMonth(string userInput, out DateTime date)
    {
        date = DateTime.MinValue;

        if (String.IsNullOrEmpty(userInput))
        {
            return false;
        }

        bool validation = DateTime.TryParseExact(userInput, "MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return validation;
    }

    public static bool GetValidYear(string userInput, out DateTime date)
    {
        date = DateTime.MinValue;

        if (String.IsNullOrEmpty(userInput))
        {
            return false;
        }

        bool validation = DateTime.TryParseExact(userInput, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return validation;
    }
} 