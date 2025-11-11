using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContect",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContect_UsersProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacesProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeRelationShip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmbeddingHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacesProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacesProfile_UsersProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalProfile_UsersProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContect_UserId",
                table: "EmergencyContect",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FacesProfile_UserId",
                table: "FacesProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProfile_UserId",
                table: "MedicalProfile",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmergencyContect");

            migrationBuilder.DropTable(
                name: "FacesProfile");

            migrationBuilder.DropTable(
                name: "MedicalProfile");

            migrationBuilder.DropTable(
                name: "UsersProfile");
        }
    }
}
