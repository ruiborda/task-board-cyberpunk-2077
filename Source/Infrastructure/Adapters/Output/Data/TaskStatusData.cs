using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

public class TaskStatusData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public Guid Id { get; set; }

    [Required]
    [Column("Name")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<TaskData> Tasks { get; set; } = new List<TaskData>();
}
