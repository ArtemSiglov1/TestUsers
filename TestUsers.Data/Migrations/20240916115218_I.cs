using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestUsers.Data.Migrations
{
    /// <inheritdoc />
    public partial class I : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoriesParameterValue_ProductCategoryParameter_Pr~",
                table: "ProductCategoriesParameterValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoryParameter_ProductCategories_ProductCategoryId",
                table: "ProductCategoryParameter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategoryParameter",
                table: "ProductCategoryParameter");

            migrationBuilder.RenameTable(
                name: "ProductCategoryParameter",
                newName: "ProductCategoryParameters");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategoryParameter_ProductCategoryId",
                table: "ProductCategoryParameters",
                newName: "IX_ProductCategoryParameters_ProductCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "UsersContact",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UsersContact",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Language",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Language",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategoryParameters",
                table: "ProductCategoryParameters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoriesParameterValue_ProductCategoryParameters_P~",
                table: "ProductCategoriesParameterValue",
                column: "ProductCategoryParameterId",
                principalTable: "ProductCategoryParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoryParameters_ProductCategories_ProductCategory~",
                table: "ProductCategoryParameters",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoriesParameterValue_ProductCategoryParameters_P~",
                table: "ProductCategoriesParameterValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoryParameters_ProductCategories_ProductCategory~",
                table: "ProductCategoryParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategoryParameters",
                table: "ProductCategoryParameters");

            migrationBuilder.RenameTable(
                name: "ProductCategoryParameters",
                newName: "ProductCategoryParameter");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategoryParameters_ProductCategoryId",
                table: "ProductCategoryParameter",
                newName: "IX_ProductCategoryParameter_ProductCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "UsersContact",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UsersContact",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Language",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Language",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategoryParameter",
                table: "ProductCategoryParameter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoriesParameterValue_ProductCategoryParameter_Pr~",
                table: "ProductCategoriesParameterValue",
                column: "ProductCategoryParameterId",
                principalTable: "ProductCategoryParameter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoryParameter_ProductCategories_ProductCategoryId",
                table: "ProductCategoryParameter",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
