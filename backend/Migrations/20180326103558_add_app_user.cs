using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace backend.Migrations
{
    public partial class add_app_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "people",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_people_ApplicationUserId",
                table: "people",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_people_AspNetUsers_ApplicationUserId",
                table: "people",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_people_AspNetUsers_ApplicationUserId",
                table: "people");

            migrationBuilder.DropIndex(
                name: "IX_people_ApplicationUserId",
                table: "people");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "people");
        }
    }
}
