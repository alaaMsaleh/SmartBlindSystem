using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class devicemodul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatteryLevel = table.Column<double>(type: "float", nullable: false),
                    LastSync = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrimWareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    VibrationPattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CameraResolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SensorRang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}

