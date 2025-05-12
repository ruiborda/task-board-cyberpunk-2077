using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

public class TaskAssignmentData
{
    [Key, Column(Order = 0)]
    public Guid TaskId { get; set; }
    public TaskData Task { get; set; } = null!;

    [Key, Column(Order = 1)]
    public Guid UserId { get; set; }
    public UserData User { get; set; } = null!;

    [Required]
    [Column("AssignedAt")]
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
