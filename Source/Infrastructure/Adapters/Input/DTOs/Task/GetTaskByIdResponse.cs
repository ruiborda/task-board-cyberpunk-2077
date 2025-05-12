using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.Task;

public class GetTaskByIdResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid StatusId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<GetUserByIdResponse> AssignedUsers { get; set; } = new List<GetUserByIdResponse>();
}
