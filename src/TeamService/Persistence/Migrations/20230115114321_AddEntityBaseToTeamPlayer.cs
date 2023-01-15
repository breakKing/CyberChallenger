using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityBaseToTeamPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                schema: "teams",
                table: "teams_players",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "deleted_at",
                schema: "teams",
                table: "teams_players",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "teams",
                table: "teams_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                schema: "teams",
                table: "teams_players",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "teams",
                table: "teams_players",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "teams",
                table: "teams_players");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                schema: "teams",
                table: "teams_players");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "teams",
                table: "teams_players");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "teams",
                table: "teams_players");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "teams",
                table: "teams_players");
        }
    }
}
