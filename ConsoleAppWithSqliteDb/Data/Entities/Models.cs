// ReSharper disable once CheckNamespace

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppWithSqliteDb.Data.Entities.Models;

public class TodoItemModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? TaskName { get; set; }
    public string? Description { get; set; }
    public Priority TaskPriority { get; set; }
    public DateTime PlannedFinishDate { get; set; } = DateTime.Now.AddHours(1);
    public bool IsTaskDone { get; set; }
    public DateTime FinishDate { get; set; }

    public TodoItemModel(){}

    public TodoItemModel(string taskName, string taskDescription, Priority taskPriority = Priority.Neutral)
    {
        TaskName = taskName;
        Description = taskDescription;
        TaskPriority = taskPriority;
    }

    public TodoItemModel(string taskName, string taskDescription, DateTime plannedFinishDate, Priority taskPriority = Priority.Neutral)
    {
        TaskName = taskName;
        Description = taskDescription;
        PlannedFinishDate = plannedFinishDate;
        TaskPriority = taskPriority;
    }

    public void FinishTask()
    {
        IsTaskDone = true;
        FinishDate = DateTime.Now;
    }
}

public enum Priority
{
    MustHave,
    Important,
    Neutral
}