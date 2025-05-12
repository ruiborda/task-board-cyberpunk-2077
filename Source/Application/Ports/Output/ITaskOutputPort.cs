using TaskBoardDemo.Source.Domain.Entity;

namespace TaskBoardDemo.Source.Application.Ports.Output;

public interface ITaskOutputPort
{
    Task<TaskEntity?> GetTaskByIdAsync(Guid id);
    Task<TaskEntity?> CreateTaskAsync(TaskEntity task);
    Task<TaskEntity?> UpdateTaskAsync(TaskEntity task);
    Task<bool> DeleteTaskAsync(Guid id);
    Task<TaskEntity?> UpdateTaskStatusAsync(Guid id, Guid statusId);
    Task<List<TaskEntity>> GetAllTasksAsync();
}
