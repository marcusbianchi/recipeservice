using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class RemovedAdditionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxValue",
                table: "PhaseParameters");

            migrationBuilder.DropColumn(
                name: "minValue",
                table: "PhaseParameters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "maxValue",
                table: "PhaseParameters",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "minValue",
                table: "PhaseParameters",
                maxLength: 50,
                nullable: true);
        }
    }
}
