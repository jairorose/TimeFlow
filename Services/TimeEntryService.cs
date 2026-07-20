namespace TimeFlow.Services;

using Microsoft.EntityFrameworkCore;
using TimeFlow.Models;
using TimeFlow.Data;

public class TimeEntryService
{
    public List<TimeEntry> GetAll()
    {
        using var db = new TimeFlowDbContext();

        List<TimeEntry> timeEntries = db.TimeEntries
            .Include(t => t.Project)
            .ToList();

        return timeEntries;
    }

    public TimeEntry GetById(int id)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry timeEntry = db.TimeEntries.Find(id);

        return timeEntry;
    }

    public void Create(string description, DateTime start, DateTime end, int projectId)
    {
        using var db = new TimeFlowDbContext();
        
        var timeEntry = new TimeEntry
        {
            Description = description,
            StartTime = start,
            EndTime = end,
            ProjectId = projectId
        };

        db.TimeEntries.Add(timeEntry);
        db.SaveChanges();
    } 

    public void UpdateDescription(int id, string description)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry timeEntry = db.TimeEntries.Find(id);

        timeEntry.Description = description;
        db.SaveChanges();
    }

    public void UpdateStartTime(int id, DateTime startTime)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry timeEntry = db.TimeEntries.Find(id);

        timeEntry.StartTime = startTime;
        db.SaveChanges();
    }

    public void UpdateEndTime(int id, DateTime endTime)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry timeEntry = db.TimeEntries.Find(id);

        timeEntry.EndTime = endTime;
        db.SaveChanges();
    }

    public void UpdateProject(int id, int projectId)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry timeEntry = db.TimeEntries.Find(id);

        timeEntry.ProjectId = projectId;
        db.SaveChanges();
    }

    public void DeleteTimeEntry(int id)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry timeEntry = db.TimeEntries.Find(id);

        db.TimeEntries.Remove(timeEntry);
        db.SaveChanges();
    }

    public bool ValidateStartTime(DateTime start, DateTime end)
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

    public bool ValidateEndTime(DateTime start, DateTime end)
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