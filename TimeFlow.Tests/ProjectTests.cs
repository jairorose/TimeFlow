namespace TimeFlow.Tests;

using TimeFlow.Models;

public class ProjectTests
{
    [Theory]
    [InlineData("Reading")]
    [InlineData("Chess")]
    public void Constructor_ValidName_SetsName(string name)
    {
        // Arrange
        Project project = new Project(name);
        
        // Act
        
        // Assert
        Assert.Equal(name, project.Name);
    }

    [Theory]
    [InlineData("  Painting ", "Painting")]
    [InlineData("      Programming", "Programming")]
    [InlineData("   Test project   ", "Test project")]
    public void Constructor_LeadingTabsOrSpaces_SetsName(string name, string expected)
    {
        // Arrange
        Project project = new Project(name);
        
        // Act
        
        // Assert
        Assert.Equal(expected, project.Name);
    }

    [Fact]
    public void Constructor_NameOfMaxLength_SetsName()
    {
        // Arrange
        string name = new string('a', Project.NameMaxLength);
        Project project = new Project(name);
        
        // Act
        
        // Assert
        Assert.Equal(name, project.Name);
    }

    [Fact]
    public void Constructor_NameExceedsMaxLength_ThrowsArgument()
    {
        // Arrange
        string name = new string('a', Project.NameMaxLength + 1);
        
        // Act
        
        // Assert
        Assert.Throws<ArgumentException>(() => new Project(name));
    }

    [Theory]
    [InlineData("")]
    [InlineData("\t")] // Tab
    [InlineData("\n\n")] // Tab
    [InlineData("  ")] // Space
    public void Constructor_EmptyOrWhiteSpaceName_ThrowsArgument(string name)
    {
        // Arrange
        
        // Act
        
        // Assert
        Assert.Throws<ArgumentException>(() => new Project(name));
    }
    
    [Theory]
    [InlineData("Reading", "Chess")]
    [InlineData("Programming", "School")]
    public void Rename_ValidName_ChangesName(string name, string expected)
    {
        // Arrange
        Project project = new Project(name);
        
        // Act
        project.Rename(expected);
        
        // Assert
        Assert.Equal(expected, project.Name);
    }
    
    [Theory]
    [InlineData("   Reading ", "Reading")]
    [InlineData(" Programming project   ", "Programming project")]
    [InlineData("\t Chess\n\n", "Chess")]
    public void NormalizeName_LeadingTabsOrSpace_ReturnsValidName(string name, string expected)
    {
        // Arrange
        
        // Act
        string result = Project.NormalizeName(name);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("   ", "")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void NormalizeName_EmptyOrWhiteSpaceName_ReturnsEmptyString(string name, string expected)
    {
        // Arrange
        
        // Act
        string result = Project.NormalizeName(name);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void GetNameError_EmptyName_ReturnsError(string? name)
    {
        Assert.NotNull(Project.GetNameError(name));
    }

    [Fact]
    public void GetNameError_TooLongName_ReturnsError()
    {
        string name = new string('a', Project.NameMaxLength + 1);

        Assert.NotNull(Project.GetNameError(name));
    }

    [Fact]
    public void GetNameError_ValidName_ReturnsNull()
    {
        Assert.Null(Project.GetNameError("Reading"));
    }
}