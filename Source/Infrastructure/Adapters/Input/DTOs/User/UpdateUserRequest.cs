using System.ComponentModel.DataAnnotations;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;

public class UpdateUserRequest
{
    [Required] public Guid Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string ProfileImage { get; set; } = string.Empty;

    [StringLength(100, MinimumLength = 6)] public string? Password { get; set; } = null;
}