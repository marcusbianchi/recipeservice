using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class ChangedColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "setupValue",
                table: "PhaseProducts");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "PhaseProducts",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "PhaseProducts");

            migrationBuilder.AddColumn<string>(
                name: "setupValue",
                table: "PhaseProducts",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
