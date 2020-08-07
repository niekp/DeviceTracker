using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceTracker.Migrations
{
    public partial class Cooldown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartCooldown",
                table: "DeviceUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartCooldown",
                table: "DeviceUser");
        }
    }
}
