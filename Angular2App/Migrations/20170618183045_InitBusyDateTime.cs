using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angular2App.Migrations
{
    public partial class InitBusyDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusyDate",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusyTime",
                table: "Offers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusyDate",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "BusyTime",
                table: "Offers");
        }
    }
}
