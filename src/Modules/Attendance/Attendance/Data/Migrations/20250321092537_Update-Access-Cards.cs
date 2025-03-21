using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccessCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Attendance",
                table: "AccessCards",
                newName: "OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "OwnerType",
                schema: "Attendance",
                table: "AccessCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerType",
                schema: "Attendance",
                table: "AccessCards");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                schema: "Attendance",
                table: "AccessCards",
                newName: "UserId");
        }
    }
}
