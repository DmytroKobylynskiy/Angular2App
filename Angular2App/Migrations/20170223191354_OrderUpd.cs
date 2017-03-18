using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angular2App.Migrations
{
    public partial class OrderUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "TaxiOrders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StartPoint",
                table: "TaxiOrders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassengerPhone",
                table: "TaxiOrders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassengerName",
                table: "TaxiOrders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndPoint",
                table: "TaxiOrders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "TaxiOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "TaxiOrders",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "StartPoint",
                table: "TaxiOrders",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "PassengerPhone",
                table: "TaxiOrders",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "PassengerName",
                table: "TaxiOrders",
                maxLength: 60,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "EndPoint",
                table: "TaxiOrders",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "TaxiOrders",
                nullable: false);
        }
    }
}
