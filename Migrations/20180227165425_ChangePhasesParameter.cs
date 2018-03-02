using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class ChangePhasesParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "measurementUnit",
                table: "PhaseParameters");

            migrationBuilder.DropColumn(
                name: "setupValueMax",
                table: "PhaseParameters");

            migrationBuilder.DropColumn(
                name: "setupValueMin",
                table: "PhaseParameters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "measurementUnit",
                table: "PhaseParameters",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "setupValueMax",
                table: "PhaseParameters",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "setupValueMin",
                table: "PhaseParameters",
                maxLength: 50,
                nullable: true);
        }
    }
}
