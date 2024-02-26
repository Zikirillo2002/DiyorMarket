using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiyorMarket.Infrastructure.Persistence.Migrations
{
    public partial class AddLowStockAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LowQuantityAmount",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LowQuantityAmount",
                table: "Product");
        }
    }
}
