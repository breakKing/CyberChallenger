using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "team_role_id",
                schema: "teams",
                table: "teams_players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "team_roles",
                schema: "teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team_roles", x => x.id);
                });

            var now = DateTimeOffset.UtcNow;
            
            migrationBuilder.InsertData(
                schema: "teams",
                table: "team_roles",
                columns: new[] { "id", "created_at", "deleted_at", "is_deleted", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, now, null, false, "Игрок", now },
                    { 2, now, null, false, "Капитан", now },
                    { 3, now, null, false, "Тренер", now },
                    { 4, now, null, false, "Менеджер", now }
                });

            migrationBuilder.CreateIndex(
                name: "ix_teams_players_team_role_id",
                schema: "teams",
                table: "teams_players",
                column: "team_role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_teams_players_teams_roles_team_role_id",
                schema: "teams",
                table: "teams_players",
                column: "team_role_id",
                principalSchema: "teams",
                principalTable: "team_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_teams_players_teams_roles_team_role_id",
                schema: "teams",
                table: "teams_players");

            migrationBuilder.DropTable(
                name: "team_roles",
                schema: "teams");

            migrationBuilder.DropIndex(
                name: "ix_teams_players_team_role_id",
                schema: "teams",
                table: "teams_players");

            migrationBuilder.DropColumn(
                name: "team_role_id",
                schema: "teams",
                table: "teams_players");
        }
    }
}
