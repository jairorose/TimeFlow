namespace TimeFlow.Services;

using Microsoft.EntityFrameworkCore;
using TimeFlow.Models;
using TimeFlow.Data;
using TimeFlow.Services.Results;

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

    public TimeEntry? GetById(int id)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry? timeEntry = db.TimeEntries.Find(id);

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

    public OperationResult UpdateDescription(int id, string description)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry? timeEntry = db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        timeEntry.Description = description;
        db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult UpdateStartTime(int id, DateTime startTime)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry? timeEntry = db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        timeEntry.StartTime = startTime;
        db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult UpdateEndTime(int id, DateTime endTime)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry? timeEntry = db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        timeEntry.EndTime = endTime;
        db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult UpdateProject(int id, int projectId)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry? timeEntry = db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        timeEntry.ProjectId = projectId;
        db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult DeleteTimeEntry(int id)
    {
        using var db = new TimeFlowDbContext();

        TimeEntry? timeEntry = db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        db.TimeEntries.Remove(timeEntry);
        db.SaveChanges();

        return OperationResult.Success();
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