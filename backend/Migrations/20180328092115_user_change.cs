using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace backend.Migrations
{
    public partial class user_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_people_saUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_saUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "saUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserID",
                table: "AspNetUsers",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_people_UserID",
                table: "AspNetUsers",
                column: "UserID",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_people_UserID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "saUserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_saUserId",
                table: "AspNetUsers",
                column: "saUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_people_saUserId",
                table: "AspNetUsers",
                column: "saUserId",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
