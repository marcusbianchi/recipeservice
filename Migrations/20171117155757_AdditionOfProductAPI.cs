using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class AdditionOfProductAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    childrenProductsIds = table.Column<int[]>(type: "int4[]", nullable: true),
                    enabled = table.Column<bool>(type: "bool", nullable: false),
                    parentProducId = table.Column<int>(type: "int4", nullable: true),
                    producCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    producDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    producName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    productGTIN = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInformations",
                columns: table => new
                {
                    additionalInformationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Information = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    productId = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInformations", x => x.additionalInformationId);
                    table.ForeignKey(
                        name: "FK_AdditionalInformations_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInformations_productId",
                table: "AdditionalInformations",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInformations");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
