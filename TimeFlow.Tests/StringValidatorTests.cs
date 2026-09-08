using TimeFlow.Services.Validators;

namespace TimeFlow.Tests;

public class StringValidatorTests
{
    [Theory]
    [InlineData("Lorem ipsum", "Lorem ipsum")]
    [InlineData(" Test data     ", "Test data")]
    [InlineData("Lorem ipsum lorem ipsum lorem ipsum lorem ips", 
    "Lorem ipsum lorem ipsum lorem ipsum lorem ips")] // 45 chars
    [InlineData("     Lorem ipsum lorem ipsum lorem ipsum lorem ips    ", 
    "Lorem ipsum lorem ipsum lorem ipsum lorem ips")] // More then 45 chars
    public void GetValidString_ValidInputString_ReturnsTrue(string input, string expected)
    {
        // Arrange

        // Act
        bool result = StringValidator.GetValidString(input, out string validString);

        // Assert
        Assert.True(result);
        Assert.Equal(expected, validString);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void GetValidString_NullOrEmptyInput_ReturnsFalse(string? input)
    {
        // Arrange

        // Act
        bool result = StringValidator.GetValidString(input, out string output);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("Lorem ipsum lorem ipsum lorem ipsum lorem ipsu")] // 46 chars
    public void GetValidString_GreaterThanMaxLength_ReturnsFalse(string input)
    {
        // Arrange
        
        // Act
        bool result = StringValidator.GetValidString(input, out string output);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("\tLorem ipsum\t", "\tLorem ipsum\t")]
    [InlineData("\nLorem ipsum\n", "\nLorem ipsum\n")]
    public void GetValidString_InputWithTabOrNewLine_IsNotTrimmed(string input, string expected)
    {
        // Arrange

        // Act
        bool result = StringValidator.GetValidString(input, out string output);

        // Assert
        Assert.True(result);
        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData("\t\t")]
    [InlineData("\n\n")]
    public void GetValidString_InputOnlyTabs_IncorrectlyReturnsTrue(string input)
    {
        // Arrange

        // Act
        bool result = StringValidator.GetValidString(input, out string output);

        // Assert
        Assert.True(result);
        Assert.Equal(input, output);
    }
}