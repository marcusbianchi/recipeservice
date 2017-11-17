using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class ChangeOnRelationStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phases_Recipes_recipeId",
                table: "Phases");

            migrationBuilder.DropIndex(
                name: "IX_Phases_recipeId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "recipeId",
                table: "Phases");

            migrationBuilder.AddColumn<int[]>(
                name: "phasesId",
                table: "Recipes",
                type: "int4[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phasesId",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "recipeId",
                table: "Phases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phases_recipeId",
                table: "Phases",
                column: "recipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phases_Recipes_recipeId",
                table: "Phases",
                column: "recipeId",
                principalTable: "Recipes",
                principalColumn: "recipeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
