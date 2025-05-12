using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;

namespace TaskBoardDemo.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly ITaskUseCase _taskUseCase;

        [BindProperty]
        public Guid Id { get; set; }

        public GetTaskByIdResponse Task { get; set; } = new();

        public DeleteModel(ITaskUseCase taskUseCase) => _taskUseCase = taskUseCase;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;
            var entity = await _taskUseCase.GetTaskByIdAsync(id);
            if (entity == null) return NotFound();
            Task = TaskMapper.ToGetTaskByIdResponse(entity);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var deleted = await _taskUseCase.DeleteTaskAsync(Id);
                if (deleted == null) return NotFound();
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