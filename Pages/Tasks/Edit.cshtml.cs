using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;

namespace TaskBoardDemo.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ITaskUseCase _taskUseCase;

        [BindProperty]
        public UpdateTaskRequest Request { get; set; } = new();

        public EditModel(ITaskUseCase taskUseCase) => _taskUseCase = taskUseCase;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var task = await _taskUseCase.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            Request = new UpdateTaskRequest {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                StatusId = task.StatusId
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (Request.DueDate.HasValue)
                Request.DueDate = DateTime.SpecifyKind(Request.DueDate.Value, DateTimeKind.Utc);

            var entity = TaskMapper.ToEntity(Request);
            try
            {
                var updated = await _taskUseCase.UpdateTaskAsync(entity);
                if (updated == null) return NotFound();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}