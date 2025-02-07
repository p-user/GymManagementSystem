using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutCatalog.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkoutUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutId",
                schema: "workoutcatalog",
                table: "WorkoutCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutCategories_WorkoutId",
                schema: "workoutcatalog",
                table: "WorkoutCategories",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutCategories_Workouts_WorkoutId",
                schema: "workoutcatalog",
                table: "WorkoutCategories",
                column: "WorkoutId",
                principalSchema: "workoutcatalog",
                principalTable: "Workouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutCategories_Workouts_WorkoutId",
                schema: "workoutcatalog",
                table: "WorkoutCategories");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutCategories_WorkoutId",
                schema: "workoutcatalog",
                table: "WorkoutCategories");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                schema: "workoutcatalog",
                table: "WorkoutCategories");
        }
    }
}
