using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Api.Migrations
{
    public partial class addpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayedInfo",
                columns: table => new
                {
                    PayedId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PayedDateTime = table.Column<DateTime>(nullable: false),
                    PayDecimal = table.Column<decimal>(nullable: false),
                    PayedDesc = table.Column<string>(nullable: true),
                    Payer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayedInfo", x => x.PayedId);
                });

            migrationBuilder.CreateTable(
                name: "RefundInfo",
                columns: table => new
                {
                    RefundId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RefundAmount = table.Column<decimal>(nullable: false),
                    RefundDesc = table.Column<string>(nullable: true),
                    Refunder = table.Column<string>(nullable: true),
                    RefundDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundInfo", x => x.RefundId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayedInfo");

            migrationBuilder.DropTable(
                name: "RefundInfo");
        }
    }
}
