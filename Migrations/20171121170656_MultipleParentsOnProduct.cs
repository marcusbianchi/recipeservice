using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class MultipleParentsOnProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parentProducId",
                table: "Products");

            migrationBuilder.AddColumn<int[]>(
                name: "parentProductsIds",
                table: "Products",
                type: "int4[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parentProductsIds",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "parentProducId",
                table: "Products",
                nullable: true);
        }
    }
}
