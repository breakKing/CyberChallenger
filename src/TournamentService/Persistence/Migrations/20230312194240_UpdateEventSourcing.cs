using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventSourcing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "event_sourcing",
                table: "outbox",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "event_sourcing",
                table: "inbox",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 12, 19, 42, 39, 763, DateTimeKind.Unspecified).AddTicks(2470), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 12, 19, 42, 39, 763, DateTimeKind.Unspecified).AddTicks(2470), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 12, 19, 42, 39, 763, DateTimeKind.Unspecified).AddTicks(3312), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 12, 19, 42, 39, 763, DateTimeKind.Unspecified).AddTicks(3312), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 12, 19, 42, 39, 763, DateTimeKind.Unspecified).AddTicks(3314), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 12, 19, 42, 39, 763, DateTimeKind.Unspecified).AddTicks(3314), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 12, 19, 42, 39, 763, DateTimeKind.Unspecified).AddTicks(3315), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 12, 19, 42, 39, 763, DateTimeKind.Unspecified).AddTicks(3315), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                schema: "event_sourcing",
                table: "outbox");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "event_sourcing",
                table: "inbox");

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 11, 21, 54, 46, 47, DateTimeKind.Unspecified).AddTicks(1163), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 11, 21, 54, 46, 47, DateTimeKind.Unspecified).AddTicks(1163), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 11, 21, 54, 46, 47, DateTimeKind.Unspecified).AddTicks(2011), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 11, 21, 54, 46, 47, DateTimeKind.Unspecified).AddTicks(2011), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 11, 21, 54, 46, 47, DateTimeKind.Unspecified).AddTicks(2013), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 11, 21, 54, 46, 47, DateTimeKind.Unspecified).AddTicks(2013), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 11, 21, 54, 46, 47, DateTimeKind.Unspecified).AddTicks(2014), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 11, 21, 54, 46, 47, DateTimeKind.Unspecified).AddTicks(2014), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
