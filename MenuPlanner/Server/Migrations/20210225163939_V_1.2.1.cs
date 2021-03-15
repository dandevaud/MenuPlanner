using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanner.Server.Migrations.MenuPlanner
{
    public partial class V_121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuTag");

            migrationBuilder.DropTable(
                name: "Quantity");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Tags",
                type: "char(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MenuId",
                table: "Tags",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Menus_MenuId",
                table: "Tags",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Menus_MenuId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_MenuId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "MenuTag",
                columns: table => new
                {
                    MenusId = table.Column<string>(type: "char(255)", nullable: false),
                    TagsId = table.Column<string>(type: "char(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuTag", x => new { x.MenusId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_MenuTag_Menus_MenusId",
                        column: x => x.MenusId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quantity",
                columns: table => new
                {
                    QuantityValue = table.Column<double>(type: "double", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuTag_TagsId",
                table: "MenuTag",
                column: "TagsId");
        }
    }
}
