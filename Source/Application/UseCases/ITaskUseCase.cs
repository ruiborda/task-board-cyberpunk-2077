using TaskBoardDemo.Source.Domain.Entity;

namespace TaskBoardDemo.Source.Application.UseCases;

public interface ITaskUseCase
{
    Task<TaskEntity?> GetTaskByIdAsync(Guid id);
    Task<TaskEntity?> CreateTaskAsync(TaskEntity task);
    Task<TaskEntity?> UpdateTaskAsync(TaskEntity task);
    Task<TaskEntity?> DeleteTaskAsync(Guid id);
    Task<TaskEntity?> UpdateTaskStatusAsync(Guid id, Guid statusId);
    Task<List<TaskEntity>> GetAllTasksAsync();
}
