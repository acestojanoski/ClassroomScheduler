using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ClassroomScheduler.Migrations
{
    public partial class EventReptitionsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRepetition_Events_EventId",
                table: "EventRepetition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRepetition",
                table: "EventRepetition");

            migrationBuilder.RenameTable(
                name: "EventRepetition",
                newName: "EventRepetitions");

            migrationBuilder.RenameIndex(
                name: "IX_EventRepetition_EventId",
                table: "EventRepetitions",
                newName: "IX_EventRepetitions_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRepetitions",
                table: "EventRepetitions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRepetitions_Events_EventId",
                table: "EventRepetitions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRepetitions_Events_EventId",
                table: "EventRepetitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRepetitions",
                table: "EventRepetitions");

            migrationBuilder.RenameTable(
                name: "EventRepetitions",
                newName: "EventRepetition");

            migrationBuilder.RenameIndex(
                name: "IX_EventRepetitions_EventId",
                table: "EventRepetition",
                newName: "IX_EventRepetition_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRepetition",
                table: "EventRepetition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRepetition_Events_EventId",
                table: "EventRepetition",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
