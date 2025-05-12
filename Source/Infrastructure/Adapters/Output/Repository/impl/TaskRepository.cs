using Microsoft.EntityFrameworkCore;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Context;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Repository.impl;

public class TaskRepository(TaskBoardDbContext dbContext) : ITaskRepository
{
    public Task<TaskData?> GetTaskByIdAsync(Guid id)
    {
        return dbContext.Tasks
            .Include(t => t.CreatedByUser)
            .Include(t => t.Assignments)
                .ThenInclude(a => a.User)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskData?> CreateTaskAsync(TaskData task)
    {
        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();
        
        // Return fresh task with all related data
        return await dbContext.Tasks
            .Include(t => t.CreatedByUser)
            .Include(t => t.Assignments)
            .ThenInclude(a => a.User)
            .FirstOrDefaultAsync(t => t.Id == task.Id);
    }

    public async Task<TaskData?> UpdateTaskAsync(TaskData task)
    {
        // First, retrieve the existing task with its assignments
        var existingTask = await dbContext.Tasks
            .Include(t => t.Assignments)
            .FirstOrDefaultAsync(t => t.Id == task.Id);
            
        if (existingTask == null) return null;
        
        // Update basic properties
        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;
        existingTask.StatusId = task.StatusId;
        existingTask.UpdatedAt = task.UpdatedAt;
        
        // Handle assignments separately since they're not directly included in the task update
        if (task.Assignments != null && task.Assignments.Any())
        {
            // Clear existing assignments
            dbContext.TaskAssignments.RemoveRange(existingTask.Assignments);
            
            // Add new assignments
            foreach (var assignment in task.Assignments)
            {
                dbContext.TaskAssignments.Add(assignment);
            }
        }
        
        await dbContext.SaveChangesAsync();
        
        // Return fresh task with all related data
        return await dbContext.Tasks
            .Include(t => t.CreatedByUser)
            .Include(t => t.Assignments)
            .ThenInclude(a => a.User)
            .FirstOrDefaultAsync(t => t.Id == task.Id);
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return false;
        dbContext.Tasks.Remove(task);
        var result = await dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<TaskData>> GetAllTasksAsync()
    {
        return await dbContext.Tasks
            .Include(u => u.CreatedByUser)
            .Include(t=>t.Assignments)
            .ThenInclude(a => a.User)
            .ToListAsync();
    }
}
