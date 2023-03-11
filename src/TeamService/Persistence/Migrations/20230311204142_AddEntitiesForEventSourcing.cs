using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitiesForEventSourcing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "event_sourcing");

            migrationBuilder.CreateTable(
                name: "message_statuses",
                schema: "event_sourcing",
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
                    table.PrimaryKey("pk_message_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inbox",
                schema: "event_sourcing",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    consumername = table.Column<string>(name: "consumer_name", type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    topicname = table.Column<string>(name: "topic_name", type: "text", nullable: false),
                    statusid = table.Column<int>(name: "status_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox", x => x.id);
                    table.ForeignKey(
                        name: "fk_inbox_message_status_status_id",
                        column: x => x.statusid,
                        principalSchema: "event_sourcing",
                        principalTable: "message_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "outbox",
                schema: "event_sourcing",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    producername = table.Column<string>(name: "producer_name", type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    topicname = table.Column<string>(name: "topic_name", type: "text", nullable: false),
                    statusid = table.Column<int>(name: "status_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox", x => x.id);
                    table.ForeignKey(
                        name: "fk_outbox_message_statuses_status_id",
                        column: x => x.statusid,
                        principalSchema: "event_sourcing",
                        principalTable: "message_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "inbox_message_headers",
                schema: "event_sourcing",
                columns: table => new
                {
                    messageid = table.Column<Guid>(name: "message_id", type: "uuid", nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_message_headers", x => new { x.messageid, x.key });
                    table.ForeignKey(
                        name: "fk_inbox_message_headers_inbox_message_id",
                        column: x => x.messageid,
                        principalSchema: "event_sourcing",
                        principalTable: "inbox",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outbox_message_headers",
                schema: "event_sourcing",
                columns: table => new
                {
                    messageid = table.Column<Guid>(name: "message_id", type: "uuid", nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTimeOffset>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    deletedat = table.Column<DateTimeOffset>(name: "deleted_at", type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_message_headers", x => new { x.messageid, x.key });
                    table.ForeignKey(
                        name: "fk_outbox_message_headers_outbox_message_id",
                        column: x => x.messageid,
                        principalSchema: "event_sourcing",
                        principalTable: "outbox",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "event_sourcing",
                table: "message_statuses",
                columns: new[] { "id", "created_at", "deleted_at", "is_deleted", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(8979), new TimeSpan(0, 0, 0, 0, 0)), null, false, "Не задан", new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(8979), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9749), new TimeSpan(0, 0, 0, 0, 0)), null, false, "Подготовлено к отправке в топик", new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9749), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9751), new TimeSpan(0, 0, 0, 0, 0)), null, false, "Успешно отправлено в топик", new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9751), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9752), new TimeSpan(0, 0, 0, 0, 0)), null, false, "Успешно обработано", new DateTimeOffset(new DateTime(2023, 3, 11, 20, 41, 42, 90, DateTimeKind.Unspecified).AddTicks(9752), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_inbox_consumer_name",
                schema: "event_sourcing",
                table: "inbox",
                column: "consumer_name");

            migrationBuilder.CreateIndex(
                name: "ix_inbox_status_id",
                schema: "event_sourcing",
                table: "inbox",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_outbox_producer_name",
                schema: "event_sourcing",
                table: "outbox",
                column: "producer_name");

            migrationBuilder.CreateIndex(
                name: "ix_outbox_status_id",
                schema: "event_sourcing",
                table: "outbox",
                column: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inbox_message_headers",
                schema: "event_sourcing");

            migrationBuilder.DropTable(
                name: "outbox_message_headers",
                schema: "event_sourcing");

            migrationBuilder.DropTable(
                name: "inbox",
                schema: "event_sourcing");

            migrationBuilder.DropTable(
                name: "outbox",
                schema: "event_sourcing");

            migrationBuilder.DropTable(
                name: "message_statuses",
                schema: "event_sourcing");
        }
    }
}
