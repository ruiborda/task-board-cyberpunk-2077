namespace TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public string ProfileImage { get; set; } = string.Empty;
}