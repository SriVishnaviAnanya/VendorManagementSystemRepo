using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorManagementSystemRepo.Migrations
{
    /// <inheritdoc />
    public partial class stage1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplianceChecklists",
                columns: table => new
                {
                    ComplianceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    NDASigned = table.Column<bool>(type: "bit", nullable: false),
                    CertificationsValid = table.Column<bool>(type: "bit", nullable: false),
                    RegulatoryCompliant = table.Column<bool>(type: "bit", nullable: false),
                    ComplianceScore = table.Column<int>(type: "int", nullable: false),
                    ComplianceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceChecklists", x => x.ComplianceId);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                });

            migrationBuilder.CreateTable(
                name: "NonComplianceLogs",
                columns: table => new
                {
                    NonComplianceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Escalated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Penalty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonComplianceLogs", x => x.NonComplianceId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "VendorPerformances",
                columns: table => new
                {
                    PerformanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    DeliveryQuality = table.Column<int>(type: "int", nullable: false),
                    SLAAdherence = table.Column<int>(type: "int", nullable: false),
                    SLARemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ComplianceScore = table.Column<int>(type: "int", nullable: false),
                    IssueCount = table.Column<int>(type: "int", nullable: false),
                    Penalty = table.Column<int>(type: "int", nullable: false),
                    FinalScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendorRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SLARatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPerformances", x => x.PerformanceId);
                });

            migrationBuilder.CreateTable(
                name: "ContractVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractVersions_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceChecklists_VendorId",
                table: "ComplianceChecklists",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractVersions_ContractId",
                table: "ContractVersions",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_NonComplianceLogs_ContractId",
                table: "NonComplianceLogs",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_NonComplianceLogs_VendorId",
                table: "NonComplianceLogs",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPerformances_VendorId",
                table: "VendorPerformances",
                column: "VendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplianceChecklists");

            migrationBuilder.DropTable(
                name: "ContractVersions");

            migrationBuilder.DropTable(
                name: "NonComplianceLogs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VendorPerformances");

            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
