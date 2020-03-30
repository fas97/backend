using Microsoft.EntityFrameworkCore.Migrations;

namespace E37SalesApi.Migrations
{
    public partial class SaleModelModified2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
