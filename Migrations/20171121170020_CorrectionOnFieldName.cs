using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class CorrectionOnFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "producCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "producDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "producName",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "productCode",
                table: "Products",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productDescription",
                table: "Products",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productName",
                table: "Products",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "productDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "productName",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "producCode",
                table: "Products",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "producDescription",
                table: "Products",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "producName",
                table: "Products",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
