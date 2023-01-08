using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "teams");

            migrationBuilder.CreateTable(
                name: "players",
                schema: "teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    teamid = table.Column<Guid>(name: "team_id", type: "uuid", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    gameid = table.Column<Guid>(name: "game_id", type: "uuid", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams_players",
                schema: "teams",
                columns: table => new
                {
                    teamid = table.Column<Guid>(name: "team_id", type: "uuid", nullable: false),
                    playerid = table.Column<Guid>(name: "player_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams_players", x => new { x.teamid, x.playerid });
                    table.ForeignKey(
                        name: "fk_teams_players_players_player_id",
                        column: x => x.playerid,
                        principalSchema: "teams",
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teams_players_teams_team_id",
                        column: x => x.teamid,
                        principalSchema: "teams",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_players_nickname",
                schema: "teams",
                table: "players",
                column: "nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_teams_players_player_id",
                schema: "teams",
                table: "teams_players",
                column: "player_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teams_players",
                schema: "teams");

            migrationBuilder.DropTable(
                name: "players",
                schema: "teams");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "teams");
        }
    }
}
