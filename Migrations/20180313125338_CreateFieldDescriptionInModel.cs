using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class CreateFieldDescriptionInModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "recipeDescription",
                table: "Recipes",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recipeDescription",
                table: "Recipes");
        }
    }
}
