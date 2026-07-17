using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

    public List<IGrouping<Project, TimeEntry>> GetEntriesByWeek(DateTime date)
    {
        int difference = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;

        DateTime start = date.Date.AddDays(-difference);
        DateTime endExclusive = start.AddDays(7);

        using var db = new TimeFlowDbContext();

        var timeEntries = db.TimeEntries
            .Include(t => t.Project)
            .Where(t => t.StartTime >= start && t.StartTime < endExclusive)
            .GroupBy(t => t.Project)
            .ToList();
        
        return timeEntries;
    }

    public List<IGrouping<Project, TimeEntry>> GetEntriesByMonth(DateTime month)
    {
        using var db = new TimeFlowDbContext();

        DateTime end = month.AddMonths(1);

        var timeEntries = db.TimeEntries
            .Include(t => t.Project)
            .Where(t => t.StartTime >= month && t.StartTime < end)
            .GroupBy(t => t.Project)
            .ToList();
        
        return timeEntries;
    }

    public List<IGrouping<Project, TimeEntry>> GetEntriesByYear(DateTime year)
    {
        using var db = new TimeFlowDbContext();

        DateTime end = year.AddYears(1);

        var timeEntries = db.TimeEntries
            .Include(t => t.Project)
            .Where(t => t.StartTime >= year && t.StartTime < end)
            .GroupBy(t => t.Project)
            .ToList();
        
        return timeEntries;
    }
}