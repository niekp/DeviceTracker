using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceTracker.Migrations
{
    public partial class Rule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    User = table.Column<string>(nullable: true),
                    Active = table.Column<int>(nullable: false),
                    NotifyAfter = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rule");
        }
    }
}
