using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Api.Migrations
{
    public partial class addpaymentorderid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "PayedInfo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "PayedInfo");
        }
    }
}
