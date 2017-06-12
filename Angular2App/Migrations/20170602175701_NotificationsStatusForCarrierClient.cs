using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angular2App.Migrations
{
    public partial class NotificationsStatusForCarrierClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationStatus",
                table: "NotificationsOrder");

            migrationBuilder.AddColumn<string>(
                name: "NotificationStatusCarrier",
                table: "NotificationsOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotificationStatusClient",
                table: "NotificationsOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationStatusCarrier",
                table: "NotificationsOrder");

            migrationBuilder.DropColumn(
                name: "NotificationStatusClient",
                table: "NotificationsOrder");

            migrationBuilder.AddColumn<string>(
                name: "NotificationStatus",
                table: "NotificationsOrder",
                nullable: true);
        }
    }
}
