using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBoardDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    ProfileImage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => new { x.TaskId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5d288d05-fabc-4c31-a3a5-efc0c65fcd03"), "Pendiente" },
                    { new Guid("e1f7b815-e9eb-48c1-a487-d90097a5b03f"), "En progreso" },
                    { new Guid("e969c3bc-2ffd-4315-9da3-ce6ebc3f4599"), "Completada" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "ProfileImage" },
                values: new object[,]
                {
                    { new Guid("47809d2b-f27b-46ec-bc1a-1631ccdb6629"), "juan@gmail.com", "Juan", "$2a$11$ISa.OtzV/z749.siWa4eCeYxD4z5ZW64JH8yRo3Sx7v/CpCHd3kRe", "https://api.dicebear.com/9.x/adventurer/svg?seed=Juan" },
                    { new Guid("582c1bd5-c2fb-42eb-ba97-300956fae069"), "pedro@gmail.com", "Pedro", "$2a$11$k1JlK5TBemoH.HogQHpsS.MjPAtJiwZ2fNO6fD3x3AxneCPoQjpce", "https://api.dicebear.com/9.x/adventurer/svg?seed=Pedro" },
                    { new Guid("a86193b7-280c-4fca-b7f4-1956563f8d47"), "maria@gmail.com", "Maria", "$2a$11$k1JlK5TBemoH.HogQHpsS.MjPAtJiwZ2fNO6fD3x3AxneCPoQjpce", "https://api.dicebear.com/9.x/adventurer/svg?seed=Maria" },
                    { new Guid("c4c0122a-d3f4-4f51-bec8-046f27e134d1"), "ana@gmail.com", "Ana", "$2a$11$AvgbZtrqb3QYahLjPgEAReoGhKWJKmU8rshLFQL9VwT.S67Xm0wAy", "https://api.dicebear.com/9.x/adventurer/svg?seed=Ana" },
                    { new Guid("d7895c16-c97e-4b27-b070-516dd7166d89"), "luis@gmail.com", "Luis", "$2a$11$YGDf30Qh0Qodt4qm.m58ZOY/aeSY36YHThpd5Hcne/Z606W0xQdAe", "https://api.dicebear.com/9.x/adventurer/svg?seed=Luis" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DueDate", "StatusId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("37439628-c72b-4087-9269-4b4845d24aef"), new DateTime(2025, 5, 7, 9, 15, 0, 0, DateTimeKind.Utc), new Guid("a86193b7-280c-4fca-b7f4-1956563f8d47"), "Resolve null-reference error when updating task status via API.", new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("e1f7b815-e9eb-48c1-a487-d90097a5b03f"), "Fix bug #42 in TaskController", new DateTime(2025, 5, 7, 9, 15, 0, 0, DateTimeKind.Utc) },
                    { new Guid("434bcb00-de9e-440f-a12d-3ca17bc4c156"), new DateTime(2025, 5, 3, 13, 20, 0, 0, DateTimeKind.Utc), new Guid("47809d2b-f27b-46ec-bc1a-1631ccdb6629"), "Cover main CRUD operations in TaskService with xUnit tests.", new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("e969c3bc-2ffd-4315-9da3-ce6ebc3f4599"), "Write unit tests for TaskService", new DateTime(2025, 5, 5, 13, 20, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4823a311-d377-497e-80a9-ad3c030c6f92"), new DateTime(2025, 5, 4, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("d7895c16-c97e-4b27-b070-516dd7166d89"), "Clean up and reorganize service classes for better maintainability.", new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("e969c3bc-2ffd-4315-9da3-ce6ebc3f4599"), "Refactor services layer", new DateTime(2025, 5, 8, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e"), new DateTime(2025, 5, 5, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("47809d2b-f27b-46ec-bc1a-1631ccdb6629"), "Set up JWT-based auth, configure login & registration endpoints.", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("5d288d05-fabc-4c31-a3a5-efc0c65fcd03"), "Implement user authentication", new DateTime(2025, 5, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2047ded-82bc-4ab5-a666-e57a027f4099"), new DateTime(2025, 5, 8, 11, 45, 0, 0, DateTimeKind.Utc), new Guid("c4c0122a-d3f4-4f51-bec8-046f27e134d1"), "Improve performance of monthly summary reports by adding indexes.", new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("e1f7b815-e9eb-48c1-a487-d90097a5b03f"), "Optimize SQL queries for reports", new DateTime(2025, 5, 8, 11, 45, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f27d145e-662f-4601-ae80-90c83e430676"), new DateTime(2025, 5, 6, 14, 30, 0, 0, DateTimeKind.Utc), new Guid("582c1bd5-c2fb-42eb-ba97-300956fae069"), "Create tables and relationships for tasks, users and assignments.", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("5d288d05-fabc-4c31-a3a5-efc0c65fcd03"), "Design database schema", new DateTime(2025, 5, 6, 14, 30, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "TaskAssignments",
                columns: new[] { "TaskId", "UserId", "AssignedAt" },
                values: new object[,]
                {
                    { new Guid("9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e"), new Guid("582c1bd5-c2fb-42eb-ba97-300956fae069"), new DateTime(2025, 5, 5, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9d32a5ca-fa04-4fda-bcf2-40e7a1a5687e"), new Guid("a86193b7-280c-4fca-b7f4-1956563f8d47"), new DateTime(2025, 5, 5, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f27d145e-662f-4601-ae80-90c83e430676"), new Guid("c4c0122a-d3f4-4f51-bec8-046f27e134d1"), new DateTime(2025, 5, 6, 13, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_UserId",
                table: "TaskAssignments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatuses_Name",
                table: "TaskStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedBy",
                table: "Tasks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAssignments");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskStatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
