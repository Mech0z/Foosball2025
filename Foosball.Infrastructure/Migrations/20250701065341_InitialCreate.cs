using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foosball.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FinishedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Team1DefenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team1AttackerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team2DefenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team2AttackerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoringPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOwnGoal = table.Column<bool>(type: "bit", nullable: false),
                    ScoredAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    MatchEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Matches_MatchEntityId",
                        column: x => x.MatchEntityId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MatchEntityId",
                table: "Goals",
                column: "MatchEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
