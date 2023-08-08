using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobilePhoneStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "MobilePhones");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "MobilePhones",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhones_Brands_BrandId",
                table: "MobilePhones");

            migrationBuilder.DropIndex(
                name: "IX_MobilePhones_BrandId",
                table: "MobilePhones");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "MobilePhones");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "MobilePhones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
