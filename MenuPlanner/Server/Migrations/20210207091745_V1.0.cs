using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanner.Server.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quantity",
                columns: table => new
                {
                    Unit = table.Column<int>(type: "int", nullable: false),
                    QuantityValue = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    FirstName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    LastName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "IngredientIngredient",
                columns: table => new
                {
                    ChildIngredientsId = table.Column<Guid>(type: "char(255)", nullable: false),
                    ParentIngredientsId = table.Column<Guid>(type: "char(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientIngredient", x => new { x.ChildIngredientsId, x.ParentIngredientsId });
                    table.ForeignKey(
                        name: "FK_IngredientIngredient_Ingredients_ChildIngredientsId",
                        column: x => x.ChildIngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientIngredient_Ingredients_ParentIngredientsId",
                        column: x => x.ParentIngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    TimeOfDay = table.Column<short>(type: "smallint", nullable: false),
                    Season = table.Column<short>(type: "smallint", nullable: false),
                    Portion = table.Column<int>(type: "int", nullable: false),
                    PortionDescription = table.Column<string>(type: "varchar(35) CHARACTER SET utf8mb4", maxLength: 35, nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Steps = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    AverageRating = table.Column<double>(type: "double", nullable: false),
                    Votes = table.Column<int>(type: "int", nullable: false),
                    MenuCategory = table.Column<int>(type: "int", nullable: false),
                    Video = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Name = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    MenuId = table.Column<Guid>(type: "char(255)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Text = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    AlternativeName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Path = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    MenuId = table.Column<Guid>(type: "char(255)", nullable: true),
                    Name = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(255)", maxLength: 255, nullable: false),
                    IngredientId = table.Column<Guid>(type: "char(255)", nullable: false),
                    MenuIngredientId = table.Column<Guid>(type: "char(255)", nullable: true),
                    QuantityAsJson = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Name = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuIngredients_Menus_MenuIngredientId",
                        column: x => x.MenuIngredientId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuTag",
                columns: table => new
                {
                    MenusId = table.Column<Guid>(type: "char(255)", nullable: false),
                    TagsId = table.Column<Guid>(type: "char(255)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MenuId",
                table: "Comments",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MenuId",
                table: "Images",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientIngredient_ParentIngredientsId",
                table: "IngredientIngredient",
                column: "ParentIngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Name",
                table: "Ingredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuIngredients_IngredientId",
                table: "MenuIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuIngredients_MenuIngredientId",
                table: "MenuIngredients",
                column: "MenuIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Name",
                table: "Menus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_UserId",
                table: "Menus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuTag_TagsId",
                table: "MenuTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "IngredientIngredient");

            migrationBuilder.DropTable(
                name: "MenuIngredients");

            migrationBuilder.DropTable(
                name: "MenuTag");

            migrationBuilder.DropTable(
                name: "Quantity");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
