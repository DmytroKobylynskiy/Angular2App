using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Angular2App.Migrations
{
    public partial class InitialOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderOwnerId",
                table: "TaxiOrders");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "TaxiOrders");

            migrationBuilder.DropTable(
                name: "TaxiOffers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxiOffers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Auto = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OfferOwnerId = table.Column<string>(nullable: true),
                    OfferStatus = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiOffers", x => x.Id);
                });

            migrationBuilder.AddColumn<string>(
                name: "OrderOwnerId",
                table: "TaxiOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "TaxiOrders",
                nullable: true);
        }
    }
}
