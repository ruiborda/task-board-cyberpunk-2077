using System.Diagnostics;
using TaskBoardDemo.Source.Application.Ports.Output;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Domain.Entity;

namespace TaskBoardDemo.Source.Application.Ports.Input;

public class TaskInputPort(ITaskOutputPort taskOutputPort, IUserOutputPort userOutputPort) : ITaskUseCase
{
    public async Task<TaskEntity?> GetTaskByIdAsync(Guid id)
    {
        return await taskOutputPort.GetTaskByIdAsync(id).ConfigureAwait(false);
    }

    public async Task<TaskEntity?> CreateTaskAsync(TaskEntity task)
    {
        var existingTask = await taskOutputPort.GetTaskByIdAsync(task.Id);
        if (existingTask != null) throw new InvalidOperationException("Task already exists.");
        var existingUser = await userOutputPort.GetUserByIdAsync(task.CreatedBy);
        if (existingUser == null) throw new InvalidOperationException("User not found.");
        var taskForAdd = TaskEntity.CreateTask(task);
        var createdTask = await taskOutputPort.CreateTaskAsync(taskForAdd).ConfigureAwait(false);
        return createdTask ?? throw new InvalidOperationException("Task could not be created.");
    }

    public async Task<TaskEntity?> UpdateTaskAsync(TaskEntity task)
    {
        var existingTask = await taskOutputPort.GetTaskByIdAsync(task.Id);
        if (existingTask == null) throw new InvalidOperationException("Task not found.");
        existingTask.UpdateTask(task);
        var updatedTask = await taskOutputPort.UpdateTaskAsync(existingTask).ConfigureAwait(false);
        return updatedTask;
    }

    public async Task<TaskEntity?> DeleteTaskAsync(Guid id)
    {
        var existingTask = await taskOutputPort.GetTaskByIdAsync(id).ConfigureAwait(false);
        if (existingTask == null) throw new InvalidOperationException("Task not found.");
        var deleted = await taskOutputPort.DeleteTaskAsync(id).ConfigureAwait(false);
        if (!deleted) throw new InvalidOperationException($"Failed to delete task with ID {id}.");
        return existingTask;
    }

    public async Task<TaskEntity?> UpdateTaskStatusAsync(Guid id, Guid statusId)
    {
        var existingTask = await taskOutputPort.GetTaskByIdAsync(id);
        if (existingTask == null) throw new InvalidOperationException("Task not found.");
        existingTask.StatusId = statusId;
        existingTask.UpdatedAt = DateTime.UtcNow;
        var updatedTask = await taskOutputPort.UpdateTaskAsync(existingTask).ConfigureAwait(false);
        return updatedTask;
    }

    public async Task<List<TaskEntity>> GetAllTasksAsync()
    {
        return await taskOutputPort.GetAllTasksAsync();
    }
}
