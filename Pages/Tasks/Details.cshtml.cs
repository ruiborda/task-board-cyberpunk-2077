using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;

namespace TaskBoardDemo.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly ITaskUseCase _taskUseCase;
        public GetTaskByIdResponse Task { get; set; } = new();

        public DetailsModel(ITaskUseCase taskUseCase) => _taskUseCase = taskUseCase;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var entity = await _taskUseCase.GetTaskByIdAsync(id);
            if (entity == null) return NotFound();
            Task = TaskMapper.ToGetTaskByIdResponse(entity);
            return Page();
        }
    }
}