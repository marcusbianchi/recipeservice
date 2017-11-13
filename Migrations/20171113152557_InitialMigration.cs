using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    phaseId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    phaseCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    phaseName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.phaseId);
                });

            migrationBuilder.CreateTable(
                name: "PhaseParameters",
                columns: table => new
                {
                    phaseParameterId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    maxValue = table.Column<string>(type: "text", nullable: true),
                    measurementUnit = table.Column<string>(type: "text", nullable: true),
                    minValue = table.Column<string>(type: "text", nullable: true),
                    parameterId = table.Column<int>(type: "int4", nullable: false),
                    phaseId = table.Column<int>(type: "int4", nullable: true),
                    setupValue = table.Column<string>(type: "text", nullable: true)
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
                    phaseProductId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    measurementUnit = table.Column<string>(type: "text", nullable: true),
                    phaseId = table.Column<int>(type: "int4", nullable: true),
                    phaseId1 = table.Column<int>(type: "int4", nullable: true),
                    productId = table.Column<int>(type: "int4", nullable: false),
                    setupValue = table.Column<string>(type: "text", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_PhaseProducts_Phases_phaseId1",
                        column: x => x.phaseId1,
                        principalTable: "Phases",
                        principalColumn: "phaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhaseParameters_phaseId",
                table: "PhaseParameters",
                column: "phaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseProducts_phaseId",
                table: "PhaseProducts",
                column: "phaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseProducts_phaseId1",
                table: "PhaseProducts",
                column: "phaseId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhaseParameters");

            migrationBuilder.DropTable(
                name: "PhaseProducts");

            migrationBuilder.DropTable(
                name: "Phases");
        }
    }
}
