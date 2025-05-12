using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;

namespace TaskBoardDemo.Pages;

public class Index(ITaskUseCase taskUseCase) : PageModel
{
    public string Message { get; set; } = "Welcome to the Task Board Demo!";


    public List<GetAllTasksResponse> Tasks { get; set; } = [];

    public async Task OnGetAsync()
    {
        try
        {
            var tasksData = await taskUseCase.GetAllTasksAsync();
            Tasks = TaskMapper.ToGetTaskByIdResponseList(tasksData);
        }
        catch (Exception ex)
        {
            Message = $"Error: {ex.Message}";
        }
    }
    
}