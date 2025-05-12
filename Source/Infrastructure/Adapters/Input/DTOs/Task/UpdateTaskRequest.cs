using System.ComponentModel.DataAnnotations;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;

public class UpdateTaskRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    [Required]
    public Guid StatusId { get; set; }
    [Required]
    public List<Guid> AssignedUserIds { get; set; } = new List<Guid>();
}
