using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class CreateMinAndMaxInParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "setupValueMax",
                table: "PhaseParameters");

            migrationBuilder.DropColumn(
                name: "setupValueMin",
                table: "PhaseParameters");
        }
    }
}
