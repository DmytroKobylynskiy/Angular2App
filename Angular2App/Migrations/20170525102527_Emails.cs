using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Angular2App.Migrations
{
    public partial class Emails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "OfferOwnerEmail",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderOwnerEmail",
                table: "NotificationsOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverEmail",
                table: "NotificationsOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferOwnerEmail",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OrderOwnerEmail",
                table: "NotificationsOrder");

            migrationBuilder.DropColumn(
                name: "ReceiverEmail",
                table: "NotificationsOrder");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarExist = table.Column<bool>(nullable: false),
                    DriverLicense = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });
        }
    }
}
