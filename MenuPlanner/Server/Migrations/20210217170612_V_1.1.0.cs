using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanner.Server.Migrations
{
    public partial class V_110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MenuIngredients",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grouping",
                table: "MenuIngredients",
                type: "varchar(32) CHARACTER SET utf8mb4",
                maxLength: 32,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MenuIngredients");

            migrationBuilder.DropColumn(
                name: "Grouping",
                table: "MenuIngredients");
        }
    }
}
