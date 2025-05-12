using System.Diagnostics;
using System.Text.Json;
using TaskBoardDemo.Source.Application.Ports.Output;
using TaskBoardDemo.Source.Domain.Entity;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Repository;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output;

public class TaskOutputAdapter(ITaskRepository taskRepository) : ITaskOutputPort
{
    public async Task<TaskEntity?> GetTaskByIdAsync(Guid id)
    {
        var task = await taskRepository.GetTaskByIdAsync(id);
        if (task != null) return TaskMapper.ToEntity(task);
        Debug.WriteLine($"Task with ID {id} not found.");
        return null;
    }

    public async Task<TaskEntity?> CreateTaskAsync(TaskEntity task)
    {
        var taskData = TaskMapper.ToData(task);
        var taskDataSaved = await taskRepository.CreateTaskAsync(taskData);
        if (taskDataSaved != null) return TaskMapper.ToEntity(taskDataSaved);
        Debug.WriteLine($"Task with title {task.Title} could not be created.");
        return null;
    }

    public Task<TaskEntity?> UpdateTaskAsync(TaskEntity task)
    {
        var taskData = TaskMapper.ToData(task);
        return taskRepository.UpdateTaskAsync(taskData)
            .ContinueWith(t => t.Result != null ? TaskMapper.ToEntity(t.Result) : null);
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var result = await taskRepository.DeleteTaskAsync(id);
        if (!result) Debug.WriteLine($"Task with ID {id} could not be deleted.");
        return result;
    }

    public async Task<TaskEntity?> UpdateTaskStatusAsync(Guid id, Guid statusId)
    {
        var task = await taskRepository.GetTaskByIdAsync(id);
        if (task == null) return null;
        task.StatusId = statusId;
        task.UpdatedAt = DateTime.UtcNow;
        var updatedTask = await taskRepository.UpdateTaskAsync(task);
        return updatedTask != null ? TaskMapper.ToEntity(updatedTask) : null;
    }

    public async Task<List<TaskEntity>> GetAllTasksAsync()
    {
        var tasks = await taskRepository.GetAllTasksAsync();
        return tasks.Select(TaskMapper.ToEntity).ToList();
    }
}
