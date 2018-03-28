using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace backend.Migrations
{
    public partial class change_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
