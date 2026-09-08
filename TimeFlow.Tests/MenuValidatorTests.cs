using TimeFlow.Services.Validators;

namespace TimeFlow.Tests;

public class MenuValidatorTests
{
    [Theory]
    [InlineData("1", 1, 4)]
    [InlineData("4", 1, 4)]
    public void GetValidMenuChoice_InputWithinRange_ReturnsParsedInt(string input, int min, int max)
    {
        // Arrange
        int expected = Int32.Parse(input);

        // Act
        int result = MenuValidator.GetValidMenuChoice(input, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("0", 1, 4)]
    [InlineData("5", 1, 4)]
    [InlineData("28374838722993948", 1, 4)]
    public void GetValidMenuChoice_InputOutsideRange_ReturnsNegativeOne(string input, int min, int max)
    {
        // Arrange
        int expected = -1;

        // Act
        int result = MenuValidator.GetValidMenuChoice(input, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("aaa", 1, 4)]
    [InlineData("1.6", 1, 4)]
    public void GetValidMenuChoice_NonNumericInput_ReturnsNegativeOne(string input, int min, int max)
    {
        // Arrange
        int expected = -1;

        // Act
        int result = MenuValidator.GetValidMenuChoice(input, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("", 1, 4)]
    [InlineData("   ", 1, 4)]
    [InlineData(null, 1, 4)]
    public void GetValidMenuChoice_NullOrEmptyInput_ReturnsNegativeOne(string? input, int min, int max)
    {
        // Arrange
        int expected = -1;

        // Act
        int result = MenuValidator.GetValidMenuChoice(input, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1", 4, 1)]
    [InlineData("3", 4, 1)]
    [InlineData("10", 4, 4)]
    public void GetValidMenuChoice_MinGreaterThanMax_AlwaysReturnsNegativeOne(string input, int min, int max)
    {
        // Arrange
        int expected = -1;

        // Act
        int result = MenuValidator.GetValidMenuChoice(input, min, max);

        // Assert
        Assert.Equal(expected, result);
    }
}