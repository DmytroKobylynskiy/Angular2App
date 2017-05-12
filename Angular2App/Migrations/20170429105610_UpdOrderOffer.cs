using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Angular2App.Migrations
{
    public partial class UpdOrderOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationsOrder",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    OrderOwnerId = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationsOrder", x => x.NotificationId);
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderOwnerId",
                table: "TaxiOrders");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "TaxiOrders");

            migrationBuilder.DropTable(
                name: "NotificationsOrder");
        }
    }
}
