using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Data.Migrations
{
    /// <inheritdoc />
    public partial class Discount_entity_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Membership");

            migrationBuilder.CreateTable(
                name: "Discounts",
                schema: "Membership",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsageLimit = table.Column<int>(type: "int", nullable: true),
                    UsageCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    AppliesToAllPlans = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AuthenticationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPlans",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    MaxVisitsPerWeek = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountMembershipPlan",
                schema: "Membership",
                columns: table => new
                {
                    ApplicablePlansId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountsApplicableCode = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountMembershipPlan", x => new { x.ApplicablePlansId, x.DiscountsApplicableCode });
                    table.ForeignKey(
                        name: "FK_DiscountMembershipPlan_Discounts_DiscountsApplicableCode",
                        column: x => x.DiscountsApplicableCode,
                        principalSchema: "Membership",
                        principalTable: "Discounts",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountMembershipPlan_MembershipPlans_ApplicablePlansId",
                        column: x => x.ApplicablePlansId,
                        principalSchema: "Membership",
                        principalTable: "MembershipPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GymMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembershipEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitsRemaining = table.Column<int>(type: "int", nullable: false),
                    TotalPricePayed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiscountCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_Discounts_DiscountCode",
                        column: x => x.DiscountCode,
                        principalSchema: "Membership",
                        principalTable: "Discounts",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Memberships_Members_GymMemberId",
                        column: x => x.GymMemberId,
                        principalSchema: "Membership",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Memberships_MembershipPlans_MembershipPlanId",
                        column: x => x.MembershipPlanId,
                        principalSchema: "Membership",
                        principalTable: "MembershipPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountMembershipPlan_DiscountsApplicableCode",
                schema: "Membership",
                table: "DiscountMembershipPlan",
                column: "DiscountsApplicableCode");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_DiscountCode",
                schema: "Membership",
                table: "Memberships",
                column: "DiscountCode");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_GymMemberId",
                schema: "Membership",
                table: "Memberships",
                column: "GymMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_MembershipPlanId",
                schema: "Membership",
                table: "Memberships",
                column: "MembershipPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountMembershipPlan",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Memberships",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Discounts",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "MembershipPlans",
                schema: "Membership");
        }
    }
}
