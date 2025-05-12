namespace TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;

public class GetUserByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public string ProfileImage { get; set; } = string.Empty;
}