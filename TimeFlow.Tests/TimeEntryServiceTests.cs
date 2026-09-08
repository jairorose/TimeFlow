using TimeFlow.Services;

namespace TimeFlow.Tests;

public class TimeEntryServiceTests
{
    [Theory]
    [InlineData(2026, 01, 01, 10, 00, 2026, 01, 01, 12, 00)]
    [InlineData(2026, 01, 01, 10, 00, 2026, 01, 02, 09, 59)]
    public void ValidateStartTime_ValidStartAndEndTime_ReturnsTrue(int sYear, int sMonth, int sDay, int sHour, int sMinute,
        int eYear, int eMonth, int eDay, int eHour, int eMinute)
    {
        // Arrange
        DateTime startTime = new DateTime(sYear, sMonth, sDay, sHour, sMinute, 0);
        DateTime endTime = new DateTime(eYear, eMonth, eDay, eHour, eMinute, 0);

        // Act
        TimeEntryService timeEntryService = new TimeEntryService();

        bool result = timeEntryService.ValidateStartTime(startTime, endTime);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(2026, 01, 01, 10, 00, 2026, 01, 01, 09, 00)] // Start time hour later
    [InlineData(2026, 01, 02, 10, 00, 2026, 01, 01, 11, 00)] // Start time day later
    [InlineData(2026, 02, 01, 10, 00, 2026, 01, 01, 11, 00)] // Start time month later
    [InlineData(2027, 01, 01, 10, 00, 2026, 01, 01, 11, 00)] // Start time year later
    public void ValidateStartTime_StartTimeIsLaterThanEndTime_ReturnsFalse(int sYear, int sMonth, int sDay, int sHour, int sMinute,
        int eYear, int eMonth, int eDay, int eHour, int eMinute)
    {
        // Arrange
        DateTime startTime = new DateTime(sYear, sMonth, sDay, sHour, sMinute, 0);
        DateTime endTime = new DateTime(eYear, eMonth, eDay, eHour, eMinute, 0);

        // Act
        TimeEntryService timeEntryService = new TimeEntryService();

        bool result = timeEntryService.ValidateStartTime(startTime, endTime);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(2026, 01, 01, 10, 00, 2026, 01, 01, 10, 00)]
    public void ValidateStartTime_StartTimeIsSameAsEndTime_ReturnsFalse(int sYear, int sMonth, int sDay, int sHour, int sMinute,
        int eYear, int eMonth, int eDay, int eHour, int eMinute)
    {
        // Arrange
        DateTime startTime = new DateTime(sYear, sMonth, sDay, sHour, sMinute, 0);
        DateTime endTime = new DateTime(eYear, eMonth, eDay, eHour, eMinute, 0);

        // Act
        TimeEntryService timeEntryService = new TimeEntryService();

        bool result = timeEntryService.ValidateStartTime(startTime, endTime);

        // Assert
        Assert.False(result);
    }
}