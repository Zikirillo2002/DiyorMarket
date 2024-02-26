using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiyorMarket.Infrastructure.Persistence.Migrations
{
    public partial class AddStockQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityInStock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityInStock",
                table: "Product");
        }
    }
}
