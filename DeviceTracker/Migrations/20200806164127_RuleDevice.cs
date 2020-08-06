using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceTracker.Migrations
{
    public partial class RuleDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Rule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rule_DeviceId",
                table: "Rule",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Device_DeviceId",
                table: "Rule",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Device_DeviceId",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_DeviceId",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Rule");
        }
    }
}
