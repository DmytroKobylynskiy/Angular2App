using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Angular2App.Migrations
{
    public partial class TaxiModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "TaxiOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<string>(nullable: false),
                    Distanse = table.Column<float>(nullable: false),
                    Duration = table.Column<float>(nullable: false),
                    EndPoint = table.Column<string>(nullable: false),
                    FreightCar = table.Column<bool>(nullable: false),
                    OrderOwnerId = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<string>(nullable: true),
                    PassengerName = table.Column<string>(maxLength: 60, nullable: false),
                    PassengerPhone = table.Column<string>(nullable: false),
                    ReceiverId = table.Column<string>(nullable: true),
                    StartPoint = table.Column<string>(nullable: false),
                    Time = table.Column<string>(nullable: false),
                    WithAnimals = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiOrders", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CarExist",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DriverLicense",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarExist",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DriverLicense",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TaxiOffers");

            migrationBuilder.DropTable(
                name: "TaxiOrders");
        }
    }
}
