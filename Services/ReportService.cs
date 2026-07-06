using Microsoft.EntityFrameworkCore;
using TimeManagementSystem.Data;
using TimeManagementSystem.Models;

namespace TimeManagementSystem.Services;

public class ReportService
{
    public List<TimeEntry> GetEntriesByDay(DateTime day)
    {
        using var db = new TimeFlowDbContext();

        List<TimeEntry> timeEntries = db.TimeEntries
            .Include(t => t.Project)
            .ToList();

        List<TimeEntry> timeEntriesReport = new List<TimeEntry>();

        foreach (TimeEntry timeEntry in timeEntries)
        {
            if (timeEntry.StartTime.Date == day.Date)
            {
                timeEntriesReport.Add(timeEntry);
            }
        }

        return timeEntriesReport;
    }
}