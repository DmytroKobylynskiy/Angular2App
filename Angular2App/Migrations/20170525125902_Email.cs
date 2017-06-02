using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angular2App.Migrations
{
    public partial class Email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengerName",
                table: "TaxiOrders");

            migrationBuilder.DropColumn(
                name: "PassengerPhone",
                table: "TaxiOrders");

            migrationBuilder.AddColumn<string>(
                name: "OrderOwnerEmail",
                table: "TaxiOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverEmail",
                table: "TaxiOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderOwnerEmail",
                table: "TaxiOrders");

            migrationBuilder.DropColumn(
                name: "ReceiverEmail",
                table: "TaxiOrders");

            migrationBuilder.AddColumn<string>(
                name: "PassengerName",
                table: "TaxiOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassengerPhone",
                table: "TaxiOrders",
                nullable: true);
        }
    }
}
