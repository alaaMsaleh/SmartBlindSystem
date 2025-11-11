using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlindSystem.Infrastructure.IdentityData.IdentityMigrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatteryLevel = table.Column<double>(type: "float", nullable: false),
                    LastSync = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrimWareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "Id", "BatteryLevel", "DeviceName", "FrimWareVersion", "LastSync", "OwnerUserId", "SerialNumber" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 80.0, "SmartStick", "1.0.0", new DateTime(2025, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "SS-0001" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 60.0, "SmartGlass", "1.0.0", new DateTime(2025, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "SG-0001" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 90.0, "Bracelet", "1.0.0", new DateTime(2025, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "BR-0001" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "AspNetUsers");
        }
    }
}
