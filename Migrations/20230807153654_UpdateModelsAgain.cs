using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobilePhoneStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhones_Brands_BrandId",
                table: "MobilePhones");

            migrationBuilder.DropIndex(
                name: "IX_MobilePhones_BrandId",
                table: "MobilePhones");

            migrationBuilder.AlterColumn<string>(
                name: "BrandId",
                table: "MobilePhones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "MobilePhones",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhones_BrandId",
                table: "MobilePhones",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhones_Brands_BrandId",
                table: "MobilePhones",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
