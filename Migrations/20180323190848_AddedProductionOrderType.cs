using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class AddedProductionOrderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productionOrderTypeId",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RecipeTypes",
                columns: table => new
                {
                    recipeTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    typeDescription = table.Column<string>(maxLength: 50, nullable: false),
                    typeScope = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTypes", x => x.recipeTypeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeTypes");

            migrationBuilder.DropColumn(
                name: "productionOrderTypeId",
                table: "Recipes");
        }
    }
}
