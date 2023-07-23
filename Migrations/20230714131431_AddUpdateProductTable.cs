using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnGlamor.Migrations
{
    public partial class AddUpdateProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductDetail",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductInformation",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductReviews",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDetail",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductInformation",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductReviews",
                table: "Products");
        }
    }
}
