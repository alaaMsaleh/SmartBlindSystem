using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MedicalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "MedicalProfile");

            migrationBuilder.RenameColumn(
                name: "BoodType",
                table: "MedicalProfile",
                newName: "BloodType");

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "MedicalProfile",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "MedicalProfile",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allergies_MedicalProfile_MedicalProfileId",
                        column: x => x.MedicalProfileId,
                        principalTable: "MedicalProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChronicDiseases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MedicalProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChronicDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChronicDiseases_MedicalProfile_MedicalProfileId",
                        column: x => x.MedicalProfileId,
                        principalTable: "MedicalProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistoryEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistoryEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistoryEntries_MedicalProfile_MedicalProfileId",
                        column: x => x.MedicalProfileId,
                        principalTable: "MedicalProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_MedicalProfileId",
                table: "Allergies",
                column: "MedicalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChronicDiseases_MedicalProfileId",
                table: "ChronicDiseases",
                column: "MedicalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistoryEntries_MedicalProfileId",
                table: "MedicalHistoryEntries",
                column: "MedicalProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "ChronicDiseases");

            migrationBuilder.DropTable(
                name: "MedicalHistoryEntries");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "MedicalProfile");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "MedicalProfile");

            migrationBuilder.RenameColumn(
                name: "BloodType",
                table: "MedicalProfile",
                newName: "BoodType");

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "MedicalProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
