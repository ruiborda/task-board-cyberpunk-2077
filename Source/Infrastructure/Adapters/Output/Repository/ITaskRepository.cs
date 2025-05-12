using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Repository;

public interface ITaskRepository
{
    Task<TaskData?> GetTaskByIdAsync(Guid id);
    Task<TaskData?> CreateTaskAsync(TaskData task);
    Task<TaskData?> UpdateTaskAsync(TaskData task);
    Task<bool> DeleteTaskAsync(Guid id);
    Task<List<TaskData>> GetAllTasksAsync();
}
