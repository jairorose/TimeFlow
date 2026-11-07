namespace TimeFlow.Services.Validators;

using System.Globalization;

public static class DateTimeValidator
{
    public static bool GetValidDateTime(string? userInput, out DateTime date)
    {
        date = DateTime.MinValue;

        if (String.IsNullOrEmpty(userInput))
        {
            return false;
        }

        bool validation = DateTime.TryParseExact(userInput, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return validation;
    }

    public static bool GetValidDate(string? userInput, out DateTime date)
    {
        date = DateTime.MinValue;

        if (String.IsNullOrEmpty(userInput))
        {
            return false;
        }

        bool validation = DateTime.TryParseExact(userInput, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return validation;
    }

    public static bool GetValidMonth(string? userInput, out DateTime date)
    {
        date = DateTime.MinValue;

        if (String.IsNullOrEmpty(userInput))
        {
            return false;
        }

        bool validation = DateTime.TryParseExact(userInput, "MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return validation;
    }

    public static bool GetValidYear(string? userInput, out DateTime date)
    {
        date = DateTime.MinValue;

        if (String.IsNullOrEmpty(userInput))
        {
            return false;
        }

        bool validation = DateTime.TryParseExact(userInput, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return validation;
    }
    
    public static bool ValidateStartTime(DateTime start, DateTime end)
    {
        if (DateTime.Compare(start, end) < 0)
        {
            DateTime maxDateTime = end.AddHours(-24); 

            if (DateTime.Compare(maxDateTime, start) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    public static bool ValidateEndTime(DateTime start, DateTime end)
    {
        if (DateTime.Compare(start, end) < 0)
        {
            DateTime maxDateTime = start.AddHours(24);

            if (DateTime.Compare(end, maxDateTime) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
} 