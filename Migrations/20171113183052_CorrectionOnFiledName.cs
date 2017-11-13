using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class CorrectionOnFiledName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sucessorPhasesId",
                table: "Phases");

            migrationBuilder.AddColumn<int[]>(
                name: "sucessorPhasesIds",
                table: "Phases",
                type: "int4[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sucessorPhasesIds",
                table: "Phases");

            migrationBuilder.AddColumn<int[]>(
                name: "sucessorPhasesId",
                table: "Phases",
                nullable: true);
        }
    }
}
