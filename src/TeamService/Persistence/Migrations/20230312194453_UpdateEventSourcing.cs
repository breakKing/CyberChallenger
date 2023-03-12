using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamService.Persistence.Migrations
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
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 12, 19, 44, 52, 900, DateTimeKind.Unspecified).AddTicks(5371), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 12, 19, 44, 52, 900, DateTimeKind.Unspecified).AddTicks(5371), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 12, 19, 44, 52, 900, DateTimeKind.Unspecified).AddTicks(6144), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 12, 19, 44, 52, 900, DateTimeKind.Unspecified).AddTicks(6144), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 12, 19, 44, 52, 900, DateTimeKind.Unspecified).AddTicks(6146), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 12, 19, 44, 52, 900, DateTimeKind.Unspecified).AddTicks(6146), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 12, 19, 44, 52, 900, DateTimeKind.Unspecified).AddTicks(6147), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 12, 19, 44, 52, 900, DateTimeKind.Unspecified).AddTicks(6147), new TimeSpan(0, 0, 0, 0, 0)) });
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
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(8979), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(8979), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9749), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9749), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9751), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9751), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "event_sourcing",
                table: "message_statuses",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9752), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9752), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
