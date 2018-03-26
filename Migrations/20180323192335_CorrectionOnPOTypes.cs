using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class CorrectionOnPOTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productionOrderTypeId",
                table: "Recipes",
                newName: "recipeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "recipeTypeId",
                table: "Recipes",
                newName: "productionOrderTypeId");
        }
    }
}
