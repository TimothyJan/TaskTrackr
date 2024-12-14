using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackr.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectSeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Projects",
                newName: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Projects",
                newName: "EndDate");
        }
    }
}
