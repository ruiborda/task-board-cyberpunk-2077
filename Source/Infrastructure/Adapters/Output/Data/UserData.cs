using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

public class UserData
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public Guid Id { get; set; }

    [Required] [Column("Name")] public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Column("Email")]
    public string Email { get; set; } = string.Empty;

    [Required] 
    [Column("PasswordHash")]
    public string? PasswordHash { get; set; } = string.Empty;

    [Required]
    [Column("ProfileImage")]
    public string ProfileImage { get; set; } = string.Empty;
}