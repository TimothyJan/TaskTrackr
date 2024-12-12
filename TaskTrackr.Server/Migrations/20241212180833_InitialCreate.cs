using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTrackr.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    ProjectTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.ProjectTaskId);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Description", "EndDate", "ProjectName", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, "First project", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project Alpha", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { 2, "Second project", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project Beta", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "Role" },
                values: new object[,]
                {
                    { 1, "alice@example.com", "Alice Johnson", "Manager" },
                    { 2, "bob@example.com", "Bob Smith", "Developer" },
                    { 3, "charlie@example.com", "Charlie Brown", "Tester" }
                });

            migrationBuilder.InsertData(
                table: "ProjectTasks",
                columns: new[] { "ProjectTaskId", "AssignedUserId", "Description", "DueDate", "ProjectId", "StartDate", "Status", "Title" },
                values: new object[] { 1, 2, "Task for Project Alpha", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Not Started", "Task 1" });

            migrationBuilder.InsertData(
                table: "ProjectTasks",
                columns: new[] { "ProjectTaskId", "AssignedUserId", "Description", "DueDate", "Progress", "ProjectId", "StartDate", "Status", "Title" },
                values: new object[,]
                {
                    { 2, 3, "Another Task for Project Alpha", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In Progress", "Task 2" },
                    { 3, 1, "Task for Project Beta", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", "Task 3" },
                    { 4, 1, "Another Task for Project Beta", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 75, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In Progress", "Task 4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_AssignedUserId",
                table: "ProjectTasks",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ProjectId",
                table: "ProjectTasks",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
