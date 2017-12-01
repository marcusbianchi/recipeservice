using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeservice.Migrations
{
    public partial class ChangedProductStructureOnPhase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhaseProducts_Phases_phaseId1",
                table: "PhaseProducts");

            migrationBuilder.DropIndex(
                name: "IX_PhaseProducts_phaseId1",
                table: "PhaseProducts");

            migrationBuilder.DropColumn(
                name: "phaseId1",
                table: "PhaseProducts");

            migrationBuilder.AddColumn<int>(
                name: "phaseProductType",
                table: "PhaseProducts",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phaseProductType",
                table: "PhaseProducts");

            migrationBuilder.AddColumn<int>(
                name: "phaseId1",
                table: "PhaseProducts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhaseProducts_phaseId1",
                table: "PhaseProducts",
                column: "phaseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PhaseProducts_Phases_phaseId1",
                table: "PhaseProducts",
                column: "phaseId1",
                principalTable: "Phases",
                principalColumn: "phaseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
