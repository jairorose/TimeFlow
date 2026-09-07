using TimeFlow.Services.Validators;

namespace TimeFlow.Tests;

public class DateTimeValidatorTests
{
    [Theory]
    [InlineData("01-01-2026 10:00", 2026, 1, 1, 10, 0)]
    [InlineData("19-07-2026 23:59", 2026, 7, 19, 23, 59)]
    [InlineData("01-01-2026 00:00", 2026, 1, 1, 0, 0)]
    [InlineData("29-02-2024 10:00", 2024, 2, 29, 10, 0)]
    public void GetValidDateTime_ValidInput_ReturnsTrue(string input, int year, int month, int day, int hour, int minute)
    {
        // Arrange
        DateTime date;

        int second = 0;
        DateTime expected = new DateTime(year, month, day, hour, minute, second);
        
        // Act
        bool result = DateTimeValidator.GetValidDateTime(input, out date);

        // Assert
        Assert.True(result);
        Assert.Equal(expected, date);
    }

    [Theory]
    [InlineData("01-01-01")]
    [InlineData("")]
    [InlineData("23:00")]
    [InlineData("01-01-2026 9:10")]
    [InlineData("29-02-2026 10:00")]
    [InlineData("    ")]
    [InlineData("01/01/2026 10:00")]
    [InlineData(null!)]
    public void GetValidDateTime_InvalidInput_ReturnsFalse(string? input)
    {
        // Arrange
        DateTime date;

        // Act
        bool result = DateTimeValidator.GetValidDateTime(input, out date);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("01-01-2026", 2026, 1, 1)]
    [InlineData("23-02-2026", 2026, 2, 23)]
    [InlineData("29-02-2024", 2024, 2, 29)]
    public void GetValidDate_ValidInput_ReturnsTrue(string input, int year, int month, int day)
    {
        // Arrange
        DateTime date;
        DateTime expected = new DateTime(year, month, day);

        // Act
        bool result = DateTimeValidator.GetValidDate(input, out date);

        // Assert
        Assert.True(result);
        Assert.Equal(expected, date);
    }

    [Theory]
    [InlineData("")]
    [InlineData("2026")]
    [InlineData("01-26-2026")]
    [InlineData("29-02-2026")]
    [InlineData("01/02/2026")]
    [InlineData("   ")]
    [InlineData("1-1-2026")]
    [InlineData(null!)]
    public void GetValidDate_InvalidInput_ReturnsFalse(string? input)
    {
        // Arrange
        DateTime date;

        // Act
        bool result = DateTimeValidator.GetValidDate(input, out date);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("01-2026", 2026, 1)]
    [InlineData("12-2026", 2026, 12)]
    public void GetValidMonth_ValidInput_ReturnsTrue(string input, int year, int month)
    {
        // Arrange
        DateTime date;

        int day = 1;
        DateTime expected = new DateTime(year, month, day);

        // Act
        bool result = DateTimeValidator.GetValidMonth(input, out date);

        // Assert
        Assert.True(result);
        Assert.Equal(expected, date);
    }

    [Theory]
    [InlineData("23-2026")]
    [InlineData("00-2026")]
    [InlineData("01/2026")]
    [InlineData("08")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("1-2026")]
    [InlineData(null!)]
    public void GetValidMonth_InvalidInput_ReturnsFalse(string? input)
    {
        // Arrange
        DateTime date;

        // Act
        bool result = DateTimeValidator.GetValidMonth(input, out date);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("2026", 2026)]
    [InlineData("2020", 2020)]
    public void GetValidYear_ValidInput_ReturnsTrue(string input, int year)
    {
        // Arrange
        DateTime date;
        
        int month = 1;
        int day = 1;
        DateTime expected = new DateTime(year, month, day);

        // Act
        bool result = DateTimeValidator.GetValidYear(input, out date);

        // Assert
        Assert.True(result);
        Assert.Equal(expected, date);
    }

    [Theory]
    [InlineData("50000")]
    [InlineData("900")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("0000")]
    [InlineData(null!)]
    public void GetValidYear_InvalidInput_ReturnsFalse(string? input)
    {
        // Arrange
        DateTime date;

        // Act
        bool result = DateTimeValidator.GetValidYear(input, out date);

        // Assert
        Assert.False(result);
    }
}
