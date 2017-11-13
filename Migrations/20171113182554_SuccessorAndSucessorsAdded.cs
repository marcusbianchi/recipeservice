using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class SuccessorAndSucessorsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "predecessorPhaseId",
                table: "Phases",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int[]>(
                name: "sucessorPhasesId",
                table: "Phases",
                type: "int4[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "predecessorPhaseId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "sucessorPhasesId",
                table: "Phases");
        }
    }
}
