using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

public class TaskData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public Guid Id { get; set; }

    [Required]
    [Column("Title")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Column("Description")]
    public string? Description { get; set; }

    [Column("DueDate")]
    public DateTime? DueDate { get; set; }

    [Required]
    [Column("StatusId")]
    public Guid StatusId { get; set; }
    public TaskStatusData Status { get; set; } = null!;

    [Required]
    [Column("CreatedBy")]
    public Guid CreatedBy { get; set; }
    public UserData CreatedByUser { get; set; } = null!;

    [Required]
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("UpdatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TaskAssignmentData> Assignments { get; set; } = new List<TaskAssignmentData>();
}
