namespace TimeFlow.Services;

using Microsoft.EntityFrameworkCore;
using TimeFlow.Models;
using TimeFlow.Data;
using TimeFlow.Services.Results;

public class TimeEntryService
{
    private readonly TimeFlowDbContext _db;

    public TimeEntryService(TimeFlowDbContext db)
    {
        _db = db;
    }
    
    public List<TimeEntry> GetAll()
    {
        List<TimeEntry> timeEntries = _db.TimeEntries
            .Include(t => t.Project)
            .ToList();

        return timeEntries;
    }

    public TimeEntry? GetById(int id)
    {
        TimeEntry? timeEntry = _db.TimeEntries.Find(id);

        return timeEntry;
    }

    public void Create(string description, DateTime start, DateTime end, int projectId)
    {
        var timeEntry = new TimeEntry
        {
            Description = description,
            StartTime = start,
            EndTime = end,
            ProjectId = projectId
        };

        _db.TimeEntries.Add(timeEntry);
        _db.SaveChanges();
    } 

    public OperationResult UpdateDescription(int id, string description)
    {
        TimeEntry? timeEntry = _db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        timeEntry.Description = description;
        _db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult UpdateStartTime(int id, DateTime startTime)
    {
        TimeEntry? timeEntry = _db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        timeEntry.StartTime = startTime;
        _db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult UpdateEndTime(int id, DateTime endTime)
    {
        TimeEntry? timeEntry = _db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        timeEntry.EndTime = endTime;
        _db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult UpdateProject(int id, int projectId)
    {
        TimeEntry? timeEntry = _db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        timeEntry.ProjectId = projectId;
        _db.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult DeleteTimeEntry(int id)
    {
        TimeEntry? timeEntry = _db.TimeEntries.Find(id);

        if (timeEntry == null)
        {
            return OperationResult.Failure($"Time entry with id {id} not found.");
        }

        _db.TimeEntries.Remove(timeEntry);
        _db.SaveChanges();

        return OperationResult.Success();
    }
}