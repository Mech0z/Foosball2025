using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foosball.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchGoalRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Matches_MatchEntityId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_MatchEntityId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "MatchEntityId",
                table: "Goals");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MatchId",
                table: "Goals",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Matches_MatchId",
                table: "Goals",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Matches_MatchId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_MatchId",
                table: "Goals");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchEntityId",
                table: "Goals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MatchEntityId",
                table: "Goals",
                column: "MatchEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Matches_MatchEntityId",
                table: "Goals",
                column: "MatchEntityId",
                principalTable: "Matches",
                principalColumn: "Id");
        }
    }
}
