using TaskBoardDemo.Source.Domain.Entity;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;

public static class TaskMapper
{
    public static TaskEntity ToEntity(TaskData data)
    {
        return new TaskEntity
        {
            Id = data.Id,
            Title = data.Title,
            Description = data.Description,
            DueDate = data.DueDate,
            StatusId = data.StatusId,
            CreatedBy = data.CreatedBy,
            CreatedByUser = new User
            {
                Id = data.CreatedBy,
                Name = data.CreatedByUser.Name,
                Email = data.CreatedByUser.Email,
                ProfileImage = data.CreatedByUser.ProfileImage,
                PasswordHash = data.CreatedByUser.PasswordHash
            },
            CreatedAt = data.CreatedAt,
            UpdatedAt = data.UpdatedAt,
            AssignedUsers = data.Assignments.Select(user => new User
            {
                Id = user.User.Id,
                Name = user.User.Name,
                Email = user.User.Email,
                ProfileImage = user.User.ProfileImage
            }).ToList()
        };
    }

    public static TaskData ToData(TaskEntity entity)
    {
        var taskData = new TaskData
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            StatusId = entity.StatusId,
            CreatedBy = entity.CreatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
        
        // Set up assignments if AssignedUsers is provided
        if (entity.AssignedUsers != null && entity.AssignedUsers.Any())
        {
            taskData.Assignments = entity.AssignedUsers.Select(user => new TaskAssignmentData
            {
                TaskId = entity.Id,
                UserId = user.Id,
                AssignedAt = DateTime.UtcNow
            }).ToList();
        }
        
        return taskData;
    }

    public static GetTaskByIdResponse ToGetTaskByIdResponse(TaskEntity entity)
    {
        return new GetTaskByIdResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            StatusId = entity.StatusId,
            CreatedBy = entity.CreatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            AssignedUsers = entity.AssignedUsers?.Select(user => new GetUserByIdResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfileImage = user.ProfileImage
            }).ToList() ?? new List<GetUserByIdResponse>()
        };
    }

    public static GetAllTasksResponse ToGetAllTasksResponse(TaskEntity entity)
    {
        return new GetAllTasksResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            StatusId = entity.StatusId,
            CreatedBy = entity.CreatedBy,
            CreatedByUser = new GetUserByIdResponse
            {
                Id = entity.CreatedBy,
                Name = entity.CreatedByUser.Name,
                Email = entity.CreatedByUser.Email,
                ProfileImage = entity.CreatedByUser.ProfileImage
            },
            UpdatedAt = entity.UpdatedAt,
            AssignedUsers = entity.AssignedUsers.Select(user => new GetUserByIdResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfileImage = user.ProfileImage
            }).ToList()
        };
    }

    public static CreateTaskResponse ToCreateTaskResponse(TaskEntity entity)
    {
        return new CreateTaskResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            StatusId = entity.StatusId,
            CreatedBy = entity.CreatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            AssignedUsers = entity.AssignedUsers?.Select(user => new GetUserByIdResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfileImage = user.ProfileImage
            }).ToList() ?? new List<GetUserByIdResponse>()
        };
    }

    public static UpdateTaskResponse ToUpdateTaskResponse(TaskEntity entity)
    {
        return new UpdateTaskResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            StatusId = entity.StatusId,
            CreatedBy = entity.CreatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            AssignedUsers = entity.AssignedUsers?.Select(user => new GetUserByIdResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfileImage = user.ProfileImage
            }).ToList() ?? new List<GetUserByIdResponse>()
        };
    }

    public static TaskEntity ToEntity(CreateTaskRequest request)
    {
        return new TaskEntity
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            StatusId = request.StatusId,
            CreatedBy = request.CreatedBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            AssignedUsers = request.AssignedUserIds.Select(id => new User { Id = id }).ToList()
        };
    }

    public static TaskEntity ToEntity(UpdateTaskRequest request)
    {
        return new TaskEntity
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            StatusId = request.StatusId,
            UpdatedAt = DateTime.UtcNow,
            AssignedUsers = request.AssignedUserIds.Select(id => new User { Id = id }).ToList()
        };
    }

    public static UpdateTaskStatusResponse ToUpdateTaskStatusResponse(TaskEntity entity)
    {
        return new UpdateTaskStatusResponse
        {
            Id = entity.Id,
            StatusId = entity.StatusId,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static List<GetAllTasksResponse> ToGetTaskByIdResponseList(List<TaskEntity> tasks)
    {
        return tasks.Select(ToGetAllTasksResponse).ToList();
    }
}