using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;

namespace TaskBoardDemo.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ITaskUseCase _taskUseCase;
        private readonly IUserUseCase _userUseCase;

        [BindProperty]
        public CreateTaskRequest TaskRequest { get; set; } = new();
        
        public List<SelectListItem> Statuses { get; set; } = [];
        public List<SelectListItem> Users { get; set; } = [];
        public List<GetAllUsersResponse> AllUsers { get; set; } = [];

        public CreateModel(ITaskUseCase taskUseCase, IUserUseCase userUseCase)
        {
            _taskUseCase = taskUseCase;
            _userUseCase = userUseCase;
        }

        public async Task OnGetAsync()
        {
            // Default to "Pendiente" status
            TaskRequest.StatusId = new Guid("5d288d05-fabc-4c31-a3a5-efc0c65fcd03");
            await LoadFormDataAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadFormDataAsync();
                return Page();
            }
            
            if (TaskRequest.DueDate.HasValue)
                TaskRequest.DueDate = DateTime.SpecifyKind(TaskRequest.DueDate.Value, DateTimeKind.Utc);

            var entity = TaskMapper.ToEntity(TaskRequest);
            try
            {
                await _taskUseCase.CreateTaskAsync(entity);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadFormDataAsync();
                return Page();
            }
        }
        
        private async Task LoadFormDataAsync()
        {
            // Load all users for select lists
            var users = await _userUseCase.GetAllUsersAsync();
            AllUsers = users.Select(UserMapper.ToGetAllUsersResponse).ToList();
            
            // Set up select list for creator
            Users = AllUsers.Select(u => new SelectListItem 
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            
            // If no creator selected and users exist, select first user
            if (TaskRequest.CreatedBy == Guid.Empty && Users.Any())
            {
                var firstUserId = AllUsers.FirstOrDefault()?.Id;
                if (firstUserId.HasValue)
                    TaskRequest.CreatedBy = firstUserId.Value;
            }
            
            // Set up statuses
            Statuses = new List<SelectListItem>
            {
                new() { Value = "5d288d05-fabc-4c31-a3a5-efc0c65fcd03", Text = "Pendiente", Selected = TaskRequest.StatusId == new Guid("5d288d05-fabc-4c31-a3a5-efc0c65fcd03") },
                new() { Value = "e1f7b815-e9eb-48c1-a487-d90097a5b03f", Text = "En progreso", Selected = TaskRequest.StatusId == new Guid("e1f7b815-e9eb-48c1-a487-d90097a5b03f") },
                new() { Value = "e969c3bc-2ffd-4315-9da3-ce6ebc3f4599", Text = "Completada", Selected = TaskRequest.StatusId == new Guid("e969c3bc-2ffd-4315-9da3-ce6ebc3f4599") }
            };
        }
    }
}