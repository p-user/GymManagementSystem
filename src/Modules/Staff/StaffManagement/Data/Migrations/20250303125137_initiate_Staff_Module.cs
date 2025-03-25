using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class initiate_Staff_Module : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Staff");

            migrationBuilder.CreateTable(
                name: "Trainers",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthenticationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainerCertifications",
                schema: "Staff",
                columns: table => new
                {
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateEarned = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerCertifications", x => new { x.TrainerId, x.Id });
                    table.ForeignKey(
                        name: "FK_TrainerCertifications_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalSchema: "Staff",
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerClients",
                schema: "Staff",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerClients", x => new { x.TrainerId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_TrainerClients_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalSchema: "Staff",
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerReviews",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerReviews_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalSchema: "Staff",
                        principalTable: "Trainers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainerSpecializations",
                schema: "Staff",
                columns: table => new
                {
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecializationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerSpecializations", x => new { x.TrainerId, x.Id });
                    table.ForeignKey(
                        name: "FK_TrainerSpecializations_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalSchema: "Staff",
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerReviews_TrainerId",
                schema: "Staff",
                table: "TrainerReviews",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerCertifications",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "TrainerClients",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "TrainerReviews",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "TrainerSpecializations",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Trainers",
                schema: "Staff");
        }
    }
}
