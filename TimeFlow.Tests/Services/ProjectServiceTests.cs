namespace TimeFlow.Tests.Services;

using TimeFlow.Services;
using TimeFlow.Services.Results;
using TimeFlow.Data;
using TimeFlow.Models;
using Microsoft.EntityFrameworkCore;

public class ProjectServiceTests
{
    private TimeFlowDbContext db { get; }
    
    public ProjectServiceTests()
    {
        db = new TimeFlowDbContext(); // Making sure the database exists
        db.Database.Migrate();
    }
    
    [Fact]
    public void Create_ValidName_AddsToDatabase()
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = $"test-{Guid.NewGuid():N}";
        
        // Act
        OperationResult result = projectService.Create(name);
        Assert.True(result.Succeeded, result.Error); // Check if OperationResult is true

        Project insertedProject = db.Projects.Single(p => p.Name == name);

        // Assert
        Assert.Equal(name, insertedProject.Name);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n")]
    public void Create_EmptyOrWhitespaceName_ReturnsFailure(string name)
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        
        // Act
        OperationResult result = projectService.Create(name);
        
        // Assert
        Assert.False(result.Succeeded);
    }

    [Fact]
    public void Create_NameExactlyMaxLength_AddsToDatabase() // Only possible for one time
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = new string('a', Project.NameMaxLength - 32);
        name += Guid.NewGuid().ToString("N");
        
        // Act
        OperationResult result = projectService.Create(name);
        Assert.True(result.Succeeded, result.Error); // Check if OperationResult is true

        Project insertedProject = db.Projects.Single(p => p.Name == name);

        // Assert
        Assert.Equal(name, insertedProject.Name);
    }
    
    [Fact]
    public void Create_NameOverMaxLength_ReturnsFailure() // Only possible for one time
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = new string('a', Project.NameMaxLength + 1);
        
        // Act
        OperationResult result = projectService.Create(name);

        // Assert
        Assert.False(result.Succeeded);
        Assert.False(db.Projects.Any(p => p.Name == name));
    }

    [Fact]
    public void Create_DuplicateName_ReturnsFailure()
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = $"test-{Guid.NewGuid():N}";
        
        // Act
        OperationResult resultA = projectService.Create(name);
        OperationResult resultB = projectService.Create(name);

        // Assert
        Assert.True(resultA.Succeeded, resultA.Error);
        Assert.False(resultB.Succeeded);
        Assert.Equal(1, db.Projects.Count(p => p.Name == name));
    }
}