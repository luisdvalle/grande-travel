using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TravelWebApp.Migrations
{
    public partial class addingTravelPackageTblMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelPackage",
                columns: table => new
                {
                    TravelPackageId = table.Column<string>(nullable: false),
                    Activated = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPackage", x => x.TravelPackageId);
                    table.ForeignKey(
                        name: "FK_TravelPackage_ProfileTbl_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileTbl",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelPackage_ProfileId",
                table: "TravelPackage",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelPackage");
        }
    }
}
