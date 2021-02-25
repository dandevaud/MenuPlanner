using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanner.Server.Migrations.MenuPlanner
{
    public partial class V_120 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CookTime",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "Diet",
                table: "Menus",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "PrepTime",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "Diet",
                table: "Ingredients",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookTime",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Diet",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "PrepTime",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Diet",
                table: "Ingredients");
        }
    }
}
