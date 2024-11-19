using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTrackr.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "ProjectTaskId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "ProjectTaskId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "ProjectTaskId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "ProjectTaskId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
