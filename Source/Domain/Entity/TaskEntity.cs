namespace TaskBoardDemo.Source.Domain.Entity;

public class TaskEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid StatusId { get; set; }
    public Guid CreatedBy { get; set; }

    public User CreatedByUser { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<User> AssignedUsers { get; set; } = [];

    public void UpdateTask(TaskEntity newTask)
    {
        Title = newTask.Title;
        Description = newTask.Description;
        DueDate = newTask.DueDate;
        StatusId = newTask.StatusId;
        UpdatedAt = DateTime.UtcNow;
        AssignedUsers = newTask.AssignedUsers;
    }

    public static TaskEntity CreateTask(TaskEntity task)
    {
        return new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            StatusId = task.StatusId,
            CreatedBy = task.CreatedBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            AssignedUsers = task.AssignedUsers
        };
    }
}