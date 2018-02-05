using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraAttibruteTypes",
                columns: table => new
                {
                    extraAttibruteTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    extraAttibruteTypeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraAttibruteTypes", x => x.extraAttibruteTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    phaseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    phaseCode = table.Column<string>(maxLength: 100, nullable: true),
                    phaseName = table.Column<string>(maxLength: 50, nullable: false),
                    predecessorPhaseId = table.Column<int>(nullable: false),
                    sucessorPhasesIds = table.Column<int[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.phaseId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    childrenProductsIds = table.Column<int[]>(nullable: true),
                    enabled = table.Column<bool>(nullable: false),
                    parentProductsIds = table.Column<int[]>(nullable: true),
                    productCode = table.Column<string>(maxLength: 50, nullable: true),
                    productDescription = table.Column<string>(maxLength: 100, nullable: true),
                    productGTIN = table.Column<string>(maxLength: 50, nullable: true),
                    productName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "PhaseParameters",
                columns: table => new
                {
                    phaseParameterId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    measurementUnit = table.Column<string>(maxLength: 50, nullable: false),
                    phaseId = table.Column<int>(nullable: true),
                    setupValue = table.Column<string>(maxLength: 50, nullable: false),
                    tagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseParameters", x => x.phaseParameterId);
                    table.ForeignKey(
                        name: "FK_PhaseParameters_Phases_phaseId",
                        column: x => x.phaseId,
                        principalTable: "Phases",
                        principalColumn: "phaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhaseProducts",
                columns: table => new
                {
                    phaseProductId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    measurementUnit = table.Column<string>(maxLength: 50, nullable: false),
                    phaseId = table.Column<int>(nullable: true),
                    phaseProductType = table.Column<int>(nullable: false),
                    productId = table.Column<int>(nullable: false),
                    value = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseProducts", x => x.phaseProductId);
                    table.ForeignKey(
                        name: "FK_PhaseProducts_Phases_phaseId",
                        column: x => x.phaseId,
                        principalTable: "Phases",
                        principalColumn: "phaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInformations",
                columns: table => new
                {
                    additionalInformationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Information = table.Column<string>(maxLength: 50, nullable: true),
                    Value = table.Column<string>(maxLength: 50, nullable: true),
                    productId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    recipeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    phasesId = table.Column<int[]>(nullable: true),
                    recipeCode = table.Column<string>(maxLength: 50, nullable: true),
                    recipeName = table.Column<string>(maxLength: 50, nullable: true),
                    recipeProductphaseProductId = table.Column<int>(nullable: true)
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
                name: "IX_AdditionalInformations_productId",
                table: "AdditionalInformations",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseParameters_phaseId",
                table: "PhaseParameters",
                column: "phaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseProducts_phaseId",
                table: "PhaseProducts",
                column: "phaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_recipeProductphaseProductId",
                table: "Recipes",
                column: "recipeProductphaseProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInformations");

            migrationBuilder.DropTable(
                name: "ExtraAttibruteTypes");

            migrationBuilder.DropTable(
                name: "PhaseParameters");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PhaseProducts");

            migrationBuilder.DropTable(
                name: "Phases");
        }
    }
}
