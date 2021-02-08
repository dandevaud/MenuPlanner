using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanner.Server.Migrations
{
    public partial class V_101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuIngredients_Ingredients_IngredientId",
                table: "MenuIngredients");

            migrationBuilder.AlterColumn<Guid>(
                name: "IngredientId",
                table: "MenuIngredients",
                type: "char(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(255)");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuIngredients_Ingredients_IngredientId",
                table: "MenuIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuIngredients_Ingredients_IngredientId",
                table: "MenuIngredients");

            migrationBuilder.AlterColumn<string>(
                name: "IngredientId",
                table: "MenuIngredients",
                type: "char(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuIngredients_Ingredients_IngredientId",
                table: "MenuIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
