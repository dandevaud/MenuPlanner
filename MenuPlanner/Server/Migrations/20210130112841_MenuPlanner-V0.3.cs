using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanner.Server.Migrations
{
    public partial class MenuPlannerV03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Menus_MenuId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuIngredients_Ingredients_IngredientId",
                table: "MenuIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Menus");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_Image_MenuId",
                table: "Images",
                newName: "IX_Images_MenuId");

            migrationBuilder.AlterColumn<string>(
                name: "Steps",
                table: "Menus",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Portion",
                table: "Menus",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PortionDescription",
                table: "Menus",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "MenuIngredients",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ImageId");

            migrationBuilder.CreateTable(
                name: "IngredientToIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ing1IngredientId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Ing2IngredientId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientToIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientToIngredient_Ingredients_Ing1IngredientId",
                        column: x => x.Ing1IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientToIngredient_Ingredients_Ing2IngredientId",
                        column: x => x.Ing2IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "MenuTag",
                columns: table => new
                {
                    MenusMenuId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagsTagId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuTag", x => new { x.MenusMenuId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_MenuTag_Menus_MenusMenuId",
                        column: x => x.MenusMenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuTag_Tag_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientToIngredient_Ing1IngredientId",
                table: "IngredientToIngredient",
                column: "Ing1IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientToIngredient_Ing2IngredientId",
                table: "IngredientToIngredient",
                column: "Ing2IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuTag_TagsTagId",
                table: "MenuTag",
                column: "TagsTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Menus_MenuId",
                table: "Images",
                column: "Id",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuIngredients_Ingredients_IngredientId",
                table: "MenuIngredients",
                column: "Id",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Menus_MenuId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuIngredients_Ingredients_IngredientId",
                table: "MenuIngredients");

            migrationBuilder.DropTable(
                name: "IngredientToIngredient");

            migrationBuilder.DropTable(
                name: "MenuTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Portion",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "PortionDescription",
                table: "Menus");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Images_MenuId",
                table: "Image",
                newName: "IX_Image_MenuId");

            migrationBuilder.AlterColumn<string>(
                name: "Steps",
                table: "Menus",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Menus",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "MenuIngredients",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Menus_MenuId",
                table: "Image",
                column: "Id",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuIngredients_Ingredients_IngredientId",
                table: "MenuIngredients",
                column: "Id",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
