using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingGamesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "team_id",
                schema: "teams",
                table: "participants");

            migrationBuilder.EnsureSchema(
                name: "games");

            migrationBuilder.CreateTable(
                name: "games",
                schema: "games",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_games", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_teams_game_id",
                schema: "teams",
                table: "teams",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_games_name",
                schema: "games",
                table: "games",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_teams_game_game_id",
                schema: "teams",
                table: "teams",
                column: "game_id",
                principalSchema: "games",
                principalTable: "games",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_teams_game_game_id",
                schema: "teams",
                table: "teams");

            migrationBuilder.DropTable(
                name: "games",
                schema: "games");

            migrationBuilder.DropIndex(
                name: "ix_teams_game_id",
                schema: "teams",
                table: "teams");

            migrationBuilder.AddColumn<Guid>(
                name: "team_id",
                schema: "teams",
                table: "participants",
                type: "uuid",
                nullable: true);
        }
    }
}
