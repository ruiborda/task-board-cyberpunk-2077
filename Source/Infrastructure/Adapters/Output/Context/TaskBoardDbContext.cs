using Microsoft.EntityFrameworkCore;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Context;

public class TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options) : DbContext(options)
{
    public DbSet<UserData> Users { get; set; } = null!;
    public DbSet<TaskStatusData> TaskStatuses { get; set; } = null!;
    public DbSet<TaskData> Tasks { get; set; } = null!;
    public DbSet<TaskAssignmentData> TaskAssignments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserData>();
        modelBuilder.Entity<TaskStatusData>();
        modelBuilder.Entity<TaskData>();
        modelBuilder.Entity<TaskAssignmentData>();

        modelBuilder.Entity<TaskStatusData>()
            .HasIndex(ts => ts.Name)
            .IsUnique();

        modelBuilder.Entity<TaskStatusData>().HasData(
            new TaskStatusData
            {
                Id = Guid.Parse("5d288d05-fabc-4c31-a3a5-efc0c65fcd03"),
                Name = "Pendiente"
            },
            new TaskStatusData
            {
                Id = Guid.Parse("e1f7b815-e9eb-48c1-a487-d90097a5b03f"),
                Name = "En progreso"
            },
            new TaskStatusData
            {
                Id = Guid.Parse("e969c3bc-2ffd-4315-9da3-ce6ebc3f4599"),
                Name = "Completada"
            }
        );

        modelBuilder.Entity<TaskData>()
            .HasOne(t => t.Status)
            .WithMany(s => s.Tasks)
            .HasForeignKey(t => t.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<TaskData>()
            .HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<TaskData>()
            .Property(t => t.CreatedAt)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<TaskData>()
            .Property(t => t.UpdatedAt);

        modelBuilder.Entity<TaskAssignmentData>()
            .HasKey(ta => new { ta.TaskId, ta.UserId });
        modelBuilder.Entity<TaskAssignmentData>()
            .HasOne(ta => ta.Task)
            .WithMany(t => t.Assignments)
            .HasForeignKey(ta => ta.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<TaskAssignmentData>()
            .HasOne(ta => ta.User)
            .WithMany()
            .HasForeignKey(ta => ta.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<TaskAssignmentData>()
            .Property(ta => ta.AssignedAt)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TaskData>().HasData(
            new TaskData
            {
                Id = Guid.Parse("9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e"),
                Title = "Implement user authentication",
                Description = "Set up JWT-based auth, configure login & registration endpoints.",
                DueDate = new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                StatusId = Guid.Parse("5d288d05-fabc-4c31-a3a5-efc0c65fcd03"),
                CreatedBy = Guid.Parse("47809d2b-f27b-46ec-bc1a-1631ccdb6629"),
                CreatedAt = new DateTime(2025, 5, 5, 10, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 5, 10, 0, 0, DateTimeKind.Utc)
            },
            new TaskData
            {
                Id = Guid.Parse("f27d145e-662f-4601-ae80-90c83e430676"),
                Title = "Design database schema",
                Description = "Create tables and relationships for tasks, users and assignments.",
                DueDate = new DateTime(2025, 5, 20, 0, 0, 0, DateTimeKind.Utc),
                StatusId = Guid.Parse("5d288d05-fabc-4c31-a3a5-efc0c65fcd03"),
                CreatedBy = Guid.Parse("582c1bd5-c2fb-42eb-ba97-300956fae069"),
                CreatedAt = new DateTime(2025, 5, 6, 14, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 6, 14, 30, 0, DateTimeKind.Utc)
            },
            new TaskData
            {
                Id = Guid.Parse("37439628-c72b-4087-9269-4b4845d24aef"),
                Title = "Fix bug #42 in TaskController",
                Description = "Resolve null-reference error when updating task status via API.",
                DueDate = new DateTime(2025, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                StatusId = Guid.Parse("e1f7b815-e9eb-48c1-a487-d90097a5b03f"),
                CreatedBy = Guid.Parse("a86193b7-280c-4fca-b7f4-1956563f8d47"),
                CreatedAt = new DateTime(2025, 5, 7, 9, 15, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 7, 9, 15, 0, DateTimeKind.Utc)
            },
            new TaskData
            {
                Id = Guid.Parse("a2047ded-82bc-4ab5-a666-e57a027f4099"),
                Title = "Optimize SQL queries for reports",
                Description = "Improve performance of monthly summary reports by adding indexes.",
                DueDate = new DateTime(2025, 5, 18, 0, 0, 0, DateTimeKind.Utc),
                StatusId = Guid.Parse("e1f7b815-e9eb-48c1-a487-d90097a5b03f"),
                CreatedBy = Guid.Parse("c4c0122a-d3f4-4f51-bec8-046f27e134d1"),
                CreatedAt = new DateTime(2025, 5, 8, 11, 45, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 8, 11, 45, 0, DateTimeKind.Utc)
            },
            new TaskData
            {
                Id = Guid.Parse("4823a311-d377-497e-80a9-ad3c030c6f92"),
                Title = "Refactor services layer",
                Description = "Clean up and reorganize service classes for better maintainability.",
                DueDate = new DateTime(2025, 5, 8, 0, 0, 0, DateTimeKind.Utc),
                StatusId = Guid.Parse("e969c3bc-2ffd-4315-9da3-ce6ebc3f4599"),
                CreatedBy = Guid.Parse("d7895c16-c97e-4b27-b070-516dd7166d89"),
                CreatedAt = new DateTime(2025, 5, 4, 16, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 8, 16, 0, 0, DateTimeKind.Utc)
            },
            new TaskData
            {
                Id = Guid.Parse("434bcb00-de9e-440f-a12d-3ca17bc4c156"),
                Title = "Write unit tests for TaskService",
                Description = "Cover main CRUD operations in TaskService with xUnit tests.",
                DueDate = new DateTime(2025, 5, 5, 0, 0, 0, DateTimeKind.Utc),
                StatusId = Guid.Parse("e969c3bc-2ffd-4315-9da3-ce6ebc3f4599"),
                CreatedBy = Guid.Parse("47809d2b-f27b-46ec-bc1a-1631ccdb6629"),
                CreatedAt = new DateTime(2025, 5, 3, 13, 20, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 5, 13, 20, 0, DateTimeKind.Utc)
            }
        );


        modelBuilder.Entity<UserData>().HasData(
            new UserData
            {
                Id = Guid.Parse("47809d2b-f27b-46ec-bc1a-1631ccdb6629"),
                Name = "Juan",
                Email = "juan@gmail.com",
                PasswordHash = "$2a$11$ISa.OtzV/z749.siWa4eCeYxD4z5ZW64JH8yRo3Sx7v/CpCHd3kRe",
                ProfileImage = "https://api.dicebear.com/9.x/adventurer/svg?seed=Juan"
            },
            new UserData
            {
                Id = Guid.Parse("582c1bd5-c2fb-42eb-ba97-300956fae069"),
                Name = "Pedro",
                Email = "pedro@gmail.com",
                PasswordHash = "$2a$11$k1JlK5TBemoH.HogQHpsS.MjPAtJiwZ2fNO6fD3x3AxneCPoQjpce",
                ProfileImage = "https://api.dicebear.com/9.x/adventurer/svg?seed=Pedro"
            },
            new UserData
            {
                Id = Guid.Parse("a86193b7-280c-4fca-b7f4-1956563f8d47"),
                Name = "Maria",
                Email = "maria@gmail.com",
                PasswordHash = "$2a$11$k1JlK5TBemoH.HogQHpsS.MjPAtJiwZ2fNO6fD3x3AxneCPoQjpce",
                ProfileImage = "https://api.dicebear.com/9.x/adventurer/svg?seed=Maria"
            },
            new UserData
            {
                Id = Guid.Parse("c4c0122a-d3f4-4f51-bec8-046f27e134d1"),
                Name = "Ana",
                Email = "ana@gmail.com",
                PasswordHash = "$2a$11$AvgbZtrqb3QYahLjPgEAReoGhKWJKmU8rshLFQL9VwT.S67Xm0wAy",
                ProfileImage = "https://api.dicebear.com/9.x/adventurer/svg?seed=Ana"
            },
            new UserData
            {
                Id = Guid.Parse("d7895c16-c97e-4b27-b070-516dd7166d89"),
                Name = "Luis",
                Email = "luis@gmail.com",
                PasswordHash = "$2a$11$YGDf30Qh0Qodt4qm.m58ZOY/aeSY36YHThpd5Hcne/Z606W0xQdAe",
                ProfileImage = "https://api.dicebear.com/9.x/adventurer/svg?seed=Luis"
            }
        );

        modelBuilder.Entity<TaskAssignmentData>().HasData(
            new TaskAssignmentData {
                TaskId = Guid.Parse("9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e"),
                UserId = Guid.Parse("582c1bd5-c2fb-42eb-ba97-300956fae069"),
                AssignedAt = new DateTime(2025, 5, 5, 12, 0, 0, DateTimeKind.Utc)
            },
            new TaskAssignmentData {
                TaskId = Guid.Parse("9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e"),
                UserId = Guid.Parse("a86193b7-280c-4fca-b7f4-1956563f8d47"),
                AssignedAt = new DateTime(2025, 5, 5, 12, 0, 0, DateTimeKind.Utc)
            },
            new TaskAssignmentData {
                TaskId = Guid.Parse("f27d145e-662f-4601-ae80-90c83e430676"),
                UserId = Guid.Parse("c4c0122a-d3f4-4f51-bec8-046f27e134d1"),
                AssignedAt = new DateTime(2025, 5, 6, 13, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}