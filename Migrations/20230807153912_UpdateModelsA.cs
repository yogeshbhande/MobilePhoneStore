using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobilePhoneStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "MobilePhones",
                newName: "Brand");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "MobilePhones",
                newName: "BrandId");
        }
    }
}
