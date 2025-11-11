using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlindSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedingalaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "BatteryLevel", "DeviceName", "Discriminator", "FrimWareVersion", "LastSync", "OwnerUserId", "SerialNumber" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 80.0, "SmartStick", "Device", "1.0.0", new DateTime(2025, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "SS-0001" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 60.0, "SmartGlass", "Device", "1.0.0", new DateTime(2025, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "SG-0001" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 90.0, "Bracelet", "Device", "1.0.0", new DateTime(2025, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "BR-0001" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }
    }
}
