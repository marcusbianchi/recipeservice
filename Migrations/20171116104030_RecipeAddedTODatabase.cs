using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class RecipeAddedTODatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "recipeId",
                table: "Phases",
                type: "int4",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    recipeCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    recipeName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    recipeProductphaseProductId = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.recipeId);
                    table.ForeignKey(
                        name: "FK_Recipes_PhaseProducts_recipeProductphaseProductId",
                        column: x => x.recipeProductphaseProductId,
                        principalTable: "PhaseProducts",
                        principalColumn: "phaseProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phases_recipeId",
                table: "Phases",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_recipeProductphaseProductId",
                table: "Recipes",
                column: "recipeProductphaseProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phases_Recipes_recipeId",
                table: "Phases",
                column: "recipeId",
                principalTable: "Recipes",
                principalColumn: "recipeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phases_Recipes_recipeId",
                table: "Phases");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Phases_recipeId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "recipeId",
                table: "Phases");
        }
    }
}
