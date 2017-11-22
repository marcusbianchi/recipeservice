using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class ChangedParameterToTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parameterId",
                table: "PhaseParameters");

            migrationBuilder.AddColumn<int>(
                name: "tagId",
                table: "PhaseParameters",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tagId",
                table: "PhaseParameters");

            migrationBuilder.AddColumn<int>(
                name: "parameterId",
                table: "PhaseParameters",
                nullable: false,
                defaultValue: 0);
        }
    }
}
