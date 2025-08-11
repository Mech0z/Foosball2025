using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foosball.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScoredAt",
                table: "Goals",
                newName: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team1AttackerId",
                table: "Matches",
                column: "Team1AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team1DefenderId",
                table: "Matches",
                column: "Team1DefenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team2AttackerId",
                table: "Matches",
                column: "Team2AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team2DefenderId",
                table: "Matches",
                column: "Team2DefenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Team1AttackerId",
                table: "Matches",
                column: "Team1AttackerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Team1DefenderId",
                table: "Matches",
                column: "Team1DefenderId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Team2AttackerId",
                table: "Matches",
                column: "Team2AttackerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Team2DefenderId",
                table: "Matches",
                column: "Team2DefenderId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Team1AttackerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Team1DefenderId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Team2AttackerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Team2DefenderId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Team1AttackerId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Team1DefenderId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Team2AttackerId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Team2DefenderId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Goals",
                newName: "ScoredAt");
        }
    }
}
