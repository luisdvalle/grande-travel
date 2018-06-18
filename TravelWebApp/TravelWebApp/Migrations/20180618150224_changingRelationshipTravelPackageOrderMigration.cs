using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TravelWebApp.Migrations
{
    public partial class changingRelationshipTravelPackageOrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TravelPackageId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_TravelPackageId",
                table: "Order",
                column: "TravelPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_TravelPackage_TravelPackageId",
                table: "Order",
                column: "TravelPackageId",
                principalTable: "TravelPackage",
                principalColumn: "TravelPackageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_TravelPackage_TravelPackageId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_TravelPackageId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TravelPackageId",
                table: "Order");
        }
    }
}
