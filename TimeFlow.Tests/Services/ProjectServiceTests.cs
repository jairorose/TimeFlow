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

    [Fact]
    public void IsNameAvailable_DuplicateName_ReturnsFalse()
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = $"test-{Guid.NewGuid():N}";
        
        // Act
        projectService.Create(name);
        bool result = projectService.IsNameAvailable(name);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Update_RenameToSameName_Succeeds()
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = $"test-{Guid.NewGuid():N}";

        OperationResult createResult = projectService.Create(name);
        Assert.True(createResult.Succeeded, createResult.Error);

        int id = db.Projects.AsNoTracking().Single(p => p.Name == name).Id;
        
        // Act
        OperationResult updateResult = projectService.Update(id, name);
        
        // Assert
        Assert.True(updateResult.Succeeded, updateResult.Error);
    }
    
    // Helpers for some tests
    private int CreateProjectAndGetId(ProjectService projectService, string name)
    {
        OperationResult result = projectService.Create(name);
        Assert.True(result.Succeeded, result.Error);

        return db.Projects.AsNoTracking().Single(p => p.Name == name).Id;
    }

    private string GetStoredName(int id)
    {
        return db.Projects.AsNoTracking().Single(p => p.Id == id).Name;
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 46 Chars
    public void Update_InvalidName_ReturnsFailureAndKeepsOldName(string newName)
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = $"test-{Guid.NewGuid():N}";
        int id = CreateProjectAndGetId(projectService, name);

        // Act
        OperationResult result = projectService.Update(id, newName);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal(name, GetStoredName(id));
    }

    [Fact]
    public void Update_NameBelongsToOtherProjectWithDifferentCasing_ReturnsFailure()
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string nameA = $"test-{Guid.NewGuid():N}";
        string nameB = $"test-{Guid.NewGuid():N}";
        CreateProjectAndGetId(projectService, nameA);
        int idB = CreateProjectAndGetId(projectService, nameB);

        // Act
        OperationResult result = projectService.Update(idB, nameA.ToUpper());

        // Assert
        Assert.False(result.Succeeded);
        Assert.Contains("already exists", result.Error);
        Assert.Equal(nameB, GetStoredName(idB));
    }

    [Fact]
    public void Update_OnlyCasingChanges_Succeeds()
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = $"test-{Guid.NewGuid():N}";
        int id = CreateProjectAndGetId(projectService, name);

        // Act
        OperationResult result = projectService.Update(id, name.ToUpper());

        // Assert
        Assert.True(result.Succeeded, result.Error);
        Assert.Equal(name.ToUpper(), GetStoredName(id));
    }

    [Fact]
    public void Update_UnknownIdWithExistingName_ReturnsNotFound()
    {
        // Arrange: Order (name valid -> project exists -> name is taken)
        ProjectService projectService = new ProjectService();
        string name = $"test-{Guid.NewGuid():N}";
        CreateProjectAndGetId(projectService, name);

        // Act
        OperationResult result = projectService.Update(int.MaxValue, name);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Contains("not found", result.Error);
    }

    [Fact]
    public void Create_NameDiffersOnlyInCasing_ReturnsFailure()
    {
        // Arrange
        ProjectService projectService = new ProjectService();
        string name = $"test-{Guid.NewGuid():N}";
        projectService.Create(name);

        // Act
        OperationResult result = projectService.Create(name.ToUpper());

        // Assert
        Assert.False(result.Succeeded);
        Assert.Contains("already exists", result.Error);
    }
}