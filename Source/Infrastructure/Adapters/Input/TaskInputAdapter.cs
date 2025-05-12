using Microsoft.AspNetCore.Mvc;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Input;

[ApiController]
[Route("api/[controller]")]
public class TaskInputAdapter(ITaskUseCase taskUseCase) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetTaskByIdResponse>> GetTaskById(Guid id)
    {
        var task = await taskUseCase.GetTaskByIdAsync(id);
        if (task == null) return NotFound(new { Message = $"Task with ID {id} not found." });
        return Ok(TaskMapper.ToGetTaskByIdResponse(task));
    }

    [HttpPost]
    public async Task<ActionResult<CreateTaskResponse>> CreateTask([FromBody] CreateTaskRequest request)
    {
        var task = TaskMapper.ToEntity(request);
        try
        {
            var createdTask = await taskUseCase.CreateTaskAsync(task);
            if (createdTask == null)
                return BadRequest(new { Message = "Task could not be created." });
            return Ok(TaskMapper.ToCreateTaskResponse(createdTask));
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPut]
    public async Task<ActionResult<UpdateTaskResponse>> UpdateTask([FromBody] UpdateTaskRequest request)
    {
        var task = TaskMapper.ToEntity(request);
        try
        {
            var updatedTask = await taskUseCase.UpdateTaskAsync(task);
            if (updatedTask == null)
                return NotFound(new { Message = $"Task with ID {request.Id} not found." });
            return Ok(TaskMapper.ToUpdateTaskResponse(updatedTask));
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        try
        {
            var deletedTask = await taskUseCase.DeleteTaskAsync(id);
            if (deletedTask == null)
                return NotFound(new { Message = $"Task with ID {id} not found." });
            return Ok(new { Message = $"Task with ID {id} deleted." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPost("UpdateTaskStatus")]
    public async Task<ActionResult<UpdateTaskStatusResponse>> UpdateTaskStatus([FromBody] UpdateTaskStatusRequest request)
    {
        try
        {
            var updatedTask = await taskUseCase.UpdateTaskStatusAsync(request.Id, request.StatusId);
            if (updatedTask == null)
                return NotFound(new { Message = $"Task with ID {request.Id} not found." });
            return Ok(TaskMapper.ToUpdateTaskStatusResponse(updatedTask));
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllTasksResponse>>> GetAllTasks()
    {
        var tasks = await taskUseCase.GetAllTasksAsync();
        return Ok(TaskMapper.ToGetTaskByIdResponseList(tasks));
    }
}
