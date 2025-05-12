using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;
using TaskBoardDemo.Source.Application.Ports.Output;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;

namespace TaskBoardDemo.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ITaskUseCase _taskUseCase;
        public List<GetAllTasksResponse> Tasks { get; set; } = new();

        public IndexModel(ITaskUseCase taskUseCase) => _taskUseCase = taskUseCase;

        public async Task OnGetAsync()
        {
            var tasks = await _taskUseCase.GetAllTasksAsync();
            Tasks = TaskMapper.ToGetTaskByIdResponseList(tasks);
        }
    }
}