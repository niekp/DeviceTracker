using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceTracker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identifier = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Block_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    BlockId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ping_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Block",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ping_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Block_DeviceId",
                table: "Block",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ping_BlockId",
                table: "Ping",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Ping_DeviceId",
                table: "Ping",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ping");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Device");
        }
    }
}
