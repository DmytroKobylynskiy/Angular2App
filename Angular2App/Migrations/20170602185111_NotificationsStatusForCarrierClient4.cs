using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Angular2App.Migrations
{
    public partial class NotificationsStatusForCarrierClient4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TaxiOrders",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Offers",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "RequestsChangeRole",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "NotificationId",
                table: "NotificationsOrder",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TaxiOrders",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Offers",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "RequestsChangeRole",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "NotificationId",
                table: "NotificationsOrder",
                nullable: false);
        }
    }
}
