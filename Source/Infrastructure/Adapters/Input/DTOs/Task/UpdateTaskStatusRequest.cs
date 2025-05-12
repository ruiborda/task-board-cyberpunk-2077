using System.ComponentModel.DataAnnotations;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;

public class UpdateTaskStatusRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid StatusId { get; set; }
}

public class UpdateTaskStatusResponse
{
    public Guid Id { get; set; }
    public Guid StatusId { get; set; }
    public DateTime UpdatedAt { get; set; }
}