using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceTracker.Migrations
{
    public partial class CooldownToRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "StartCooldown",
                table: "DeviceUser");
            */
            migrationBuilder.AddColumn<DateTime>(
                name: "StartCooldown",
                table: "Rule",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropColumn(
                name: "StartCooldown",
                table: "Rule");
            */
            migrationBuilder.AddColumn<DateTime>(
                name: "StartCooldown",
                table: "DeviceUser",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
