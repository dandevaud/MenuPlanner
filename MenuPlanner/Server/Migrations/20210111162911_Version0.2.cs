using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanner.Server.Migrations
{
    public partial class Version02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Ingredients_IngredientId1",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_IngredientId1",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IngredientId1",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientIngredient",
                columns: table => new
                {
                    ChildIngredientsIngredientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentIngredientsIngredientId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientIngredient", x => new { x.ChildIngredientsIngredientId, x.ParentIngredientsIngredientId });
                    table.ForeignKey(
                        name: "FK_IngredientIngredient_Ingredients_ChildIngredientsIngredientId",
                        column: x => x.ChildIngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientIngredient_Ingredients_ParentIngredientsIngredientId",
                        column: x => x.ParentIngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientIngredient_ParentIngredientsIngredientId",
                table: "IngredientIngredient",
                column: "ParentIngredientsIngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientIngredient");

            migrationBuilder.AddColumn<Guid>(
                name: "IngredientId1",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientId1",
                table: "Ingredients",
                column: "IngredientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Ingredients_IngredientId1",
                table: "Ingredients",
                column: "IngredientId1",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
