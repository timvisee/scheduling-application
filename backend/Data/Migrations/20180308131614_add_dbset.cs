using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace backend.Data.Migrations
{
    public partial class add_dbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventLocation_events_EventId",
                table: "EventLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLocation_locations_LocationId",
                table: "EventLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventLocation",
                table: "EventLocation");

            migrationBuilder.RenameTable(
                name: "EventLocation",
                newName: "EventLocations");

            migrationBuilder.RenameIndex(
                name: "IX_EventLocation_LocationId",
                table: "EventLocations",
                newName: "IX_EventLocations_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventLocations",
                table: "EventLocations",
                columns: new[] { "EventId", "LocationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventLocations_events_EventId",
                table: "EventLocations",
                column: "EventId",
                principalTable: "events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLocations_locations_LocationId",
                table: "EventLocations",
                column: "LocationId",
                principalTable: "locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventLocations_events_EventId",
                table: "EventLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLocations_locations_LocationId",
                table: "EventLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventLocations",
                table: "EventLocations");

            migrationBuilder.RenameTable(
                name: "EventLocations",
                newName: "EventLocation");

            migrationBuilder.RenameIndex(
                name: "IX_EventLocations_LocationId",
                table: "EventLocation",
                newName: "IX_EventLocation_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventLocation",
                table: "EventLocation",
                columns: new[] { "EventId", "LocationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventLocation_events_EventId",
                table: "EventLocation",
                column: "EventId",
                principalTable: "events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLocation_locations_LocationId",
                table: "EventLocation",
                column: "LocationId",
                principalTable: "locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
