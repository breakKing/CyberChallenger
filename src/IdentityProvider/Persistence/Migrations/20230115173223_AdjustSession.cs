using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProvider.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_sessions",
                schema: "identity",
                table: "sessions");

            migrationBuilder.DropIndex(
                name: "ix_sessions_user_id",
                schema: "identity",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "identity",
                table: "sessions");

            migrationBuilder.AlterColumn<long>(
                name: "expires_in_minutes",
                schema: "identity",
                table: "sessions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sessions",
                schema: "identity",
                table: "sessions",
                columns: new[] { "user_id", "fingerprint" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_sessions",
                schema: "identity",
                table: "sessions");

            migrationBuilder.AlterColumn<int>(
                name: "expires_in_minutes",
                schema: "identity",
                table: "sessions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "identity",
                table: "sessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_sessions",
                schema: "identity",
                table: "sessions",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_sessions_user_id",
                schema: "identity",
                table: "sessions",
                column: "user_id");
        }
    }
}
