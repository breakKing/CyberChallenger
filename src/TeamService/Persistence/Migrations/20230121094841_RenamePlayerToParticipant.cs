using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamePlayerToParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teams_players",
                schema: "teams");

            migrationBuilder.DropTable(
                name: "players",
                schema: "teams");

            migrationBuilder.CreateTable(
                name: "participants",
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
                    table.PrimaryKey("pk_participants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams_participants",
                schema: "teams",
                columns: table => new
                {
                    teamid = table.Column<Guid>(name: "team_id", type: "uuid", nullable: false),
                    participantid = table.Column<Guid>(name: "participant_id", type: "uuid", nullable: false),
                    teamroleid = table.Column<int>(name: "team_role_id", type: "integer", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams_participants", x => new { x.teamid, x.participantid });
                    table.ForeignKey(
                        name: "fk_teams_participants_participants_participant_id",
                        column: x => x.participantid,
                        principalSchema: "teams",
                        principalTable: "participants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teams_participants_teams_roles_team_role_id",
                        column: x => x.teamroleid,
                        principalSchema: "teams",
                        principalTable: "team_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_teams_participants_teams_team_id",
                        column: x => x.teamid,
                        principalSchema: "teams",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_participants_nickname",
                schema: "teams",
                table: "participants",
                column: "nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_teams_participants_participant_id",
                schema: "teams",
                table: "teams_participants",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "ix_teams_participants_team_role_id",
                schema: "teams",
                table: "teams_participants",
                column: "team_role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teams_participants",
                schema: "teams");

            migrationBuilder.DropTable(
                name: "participants",
                schema: "teams");

            migrationBuilder.CreateTable(
                name: "players",
                schema: "teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    teamid = table.Column<Guid>(name: "team_id", type: "uuid", nullable: true),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams_players",
                schema: "teams",
                columns: table => new
                {
                    teamid = table.Column<Guid>(name: "team_id", type: "uuid", nullable: false),
                    playerid = table.Column<Guid>(name: "player_id", type: "uuid", nullable: false),
                    teamroleid = table.Column<int>(name: "team_role_id", type: "integer", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
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
                        name: "fk_teams_players_teams_roles_team_role_id",
                        column: x => x.teamroleid,
                        principalSchema: "teams",
                        principalTable: "team_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "ix_teams_players_team_role_id",
                schema: "teams",
                table: "teams_players",
                column: "team_role_id");
        }
    }
}
