using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenuPlanner.Server.Migrations
{
    public partial class Version_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Menus_Id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Menus_Id",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientIngredient_Ingredients_ChildIngredientsIngredientId",
                table: "IngredientIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientIngredient_Ingredients_ParentIngredientsIngredientId",
                table: "IngredientIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuIngredients_Menus_Id",
                table: "MenuIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuTag_Menus_MenusMenuId",
                table: "MenuTag");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuTag_Tag_TagsTagId",
                table: "MenuTag");

            migrationBuilder.DropTable(
                name: "IngredientToIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuTag",
                table: "MenuTag");

            migrationBuilder.DropIndex(
                name: "IX_MenuTag_TagsTagId",
                table: "MenuTag");

            migrationBuilder.DropIndex(
                name: "IX_MenuIngredients_Id",
                table: "MenuIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientIngredient",
                table: "IngredientIngredient");

            migrationBuilder.DropIndex(
                name: "IX_IngredientIngredient_ParentIngredientsIngredientId",
                table: "IngredientIngredient");

            migrationBuilder.DropIndex(
                name: "IX_Images_Id",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Id",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "MenusMenuId",
                table: "MenuTag");

            migrationBuilder.DropColumn(
                name: "TagsTagId",
                table: "MenuTag");

            migrationBuilder.DropColumn(
                name: "ChildIngredientsIngredientId",
                table: "IngredientIngredient");

            migrationBuilder.DropColumn(
                name: "ParentIngredientsIngredientId",
                table: "IngredientIngredient");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Users",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tag",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Tag",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDate",
                table: "Tag",
                type: "datetime(6)",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Unit",
                table: "Quantity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "QuantityValue",
                table: "Quantity",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<Guid>(
                name: "MenusId",
                table: "MenuTag",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TagsId",
                table: "MenuTag",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "Votes",
                table: "Menus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Video",
                table: "Menus",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Menus",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "TimeOfDay",
                table: "Menus",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Steps",
                table: "Menus",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<short>(
                name: "Season",
                table: "Menus",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "PortionDescription",
                table: "Menus",
                type: "varchar(35) CHARACTER SET utf8mb4",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Portion",
                table: "Menus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Menus",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCategory",
                table: "Menus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Menus",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "AverageRating",
                table: "Menus",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Menus",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDate",
                table: "Menus",
                type: "datetime(6)",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<string>(
                name: "QuantityAsJson",
                table: "MenuIngredients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "MenuIngredients",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Ingredients",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Ingredients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Ingredients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Ingredients",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDate",
                table: "Ingredients",
                type: "datetime(6)",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "ChildIngredientsId",
                table: "IngredientIngredient",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentIngredientsId",
                table: "IngredientIngredient",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Images",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                table: "Images",
                type: "longblob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternativeName",
                table: "Images",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Images",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Images",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Comments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "CommentId",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuTag",
                table: "MenuTag",
                columns: new[] { "MenusId", "TagsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientIngredient",
                table: "IngredientIngredient",
                columns: new[] { "ChildIngredientsId", "ParentIngredientsId" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuTag_TagsId",
                table: "MenuTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientIngredient_ParentIngredientsId",
                table: "IngredientIngredient",
                column: "ParentIngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MenuId",
                table: "Images",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MenuId",
                table: "Comments",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Menus_MenuId",
                table: "Comments",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Menus_MenuId",
                table: "Images",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientIngredient_Ingredients_ChildIngredientsId",
                table: "IngredientIngredient",
                column: "ChildIngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientIngredient_Ingredients_ParentIngredientsId",
                table: "IngredientIngredient",
                column: "ParentIngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuIngredients_Menus_Id",
                table: "MenuIngredients",
                column: "Id",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuTag_Menus_MenusId",
                table: "MenuTag",
                column: "MenusId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuTag_Tag_TagsId",
                table: "MenuTag",
                column: "TagsId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Menus_MenuId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Menus_MenuId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientIngredient_Ingredients_ChildIngredientsId",
                table: "IngredientIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientIngredient_Ingredients_ParentIngredientsId",
                table: "IngredientIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuIngredients_Menus_Id",
                table: "MenuIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuTag_Menus_MenusId",
                table: "MenuTag");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuTag_Tag_TagsId",
                table: "MenuTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuTag",
                table: "MenuTag");

            migrationBuilder.DropIndex(
                name: "IX_MenuTag_TagsId",
                table: "MenuTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientIngredient",
                table: "IngredientIngredient");

            migrationBuilder.DropIndex(
                name: "IX_IngredientIngredient_ParentIngredientsId",
                table: "IngredientIngredient");

            migrationBuilder.DropIndex(
                name: "IX_Images_MenuId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MenuId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "MenusId",
                table: "MenuTag");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "MenuTag");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ChildIngredientsId",
                table: "IngredientIngredient");

            migrationBuilder.DropColumn(
                name: "ParentIngredientsId",
                table: "IngredientIngredient");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tag",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TagId",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Unit",
                table: "Quantity",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "QuantityValue",
                table: "Quantity",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<string>(
                name: "MenusMenuId",
                table: "MenuTag",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TagsTagId",
                table: "MenuTag",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Votes",
                table: "Menus",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Video",
                table: "Menus",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Menus",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeOfDay",
                table: "Menus",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "Steps",
                table: "Menus",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Season",
                table: "Menus",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "PortionDescription",
                table: "Menus",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(35) CHARACTER SET utf8mb4",
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Portion",
                table: "Menus",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Menus",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCategory",
                table: "Menus",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Menus",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<double>(
                name: "AverageRating",
                table: "Menus",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Menus",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<string>(
                name: "QuantityAsJson",
                table: "MenuIngredients",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "MenuIngredients",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Ingredients",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<string>(
                name: "ChildIngredientsIngredientId",
                table: "IngredientIngredient",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentIngredientsIngredientId",
                table: "IngredientIngredient",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Images",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                table: "Images",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternativeName",
                table: "Images",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Images",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Images",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "CommentId",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuTag",
                table: "MenuTag",
                columns: new[] { "MenusMenuId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientIngredient",
                table: "IngredientIngredient",
                columns: new[] { "ChildIngredientsIngredientId", "ParentIngredientsIngredientId" });

            migrationBuilder.CreateTable(
                name: "IngredientToIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ing1IngredientId = table.Column<string>(type: "TEXT", nullable: true),
                    Ing2IngredientId = table.Column<string>(type: "TEXT", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_MenuTag_TagsTagId",
                table: "MenuTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuIngredients_Id",
                table: "MenuIngredients",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientIngredient_ParentIngredientsIngredientId",
                table: "IngredientIngredient",
                column: "ParentIngredientsIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Id",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Id",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientToIngredient_Ing1IngredientId",
                table: "IngredientToIngredient",
                column: "Ing1IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientToIngredient_Ing2IngredientId",
                table: "IngredientToIngredient",
                column: "Ing2IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Menus_Id",
                table: "Comments",
                column: "Id",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Menus_Id",
                table: "Images",
                column: "Id",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientIngredient_Ingredients_ChildIngredientsIngredientId",
                table: "IngredientIngredient",
                column: "ChildIngredientsIngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientIngredient_Ingredients_ParentIngredientsIngredientId",
                table: "IngredientIngredient",
                column: "ParentIngredientsIngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuIngredients_Menus_Id",
                table: "MenuIngredients",
                column: "Id",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuTag_Menus_MenusMenuId",
                table: "MenuTag",
                column: "MenusMenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuTag_Tag_TagsTagId",
                table: "MenuTag",
                column: "TagsTagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
