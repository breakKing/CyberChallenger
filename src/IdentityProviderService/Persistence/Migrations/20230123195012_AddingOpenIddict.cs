using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProviderService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingOpenIddict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sessions",
                schema: "identity");

            migrationBuilder.EnsureSchema(
                name: "openid");

            migrationBuilder.CreateTable(
                name: "applications",
                schema: "openid",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    clientid = table.Column<string>(name: "client_id", type: "character varying(100)", maxLength: 100, nullable: true),
                    clientsecret = table.Column<string>(name: "client_secret", type: "text", nullable: true),
                    concurrencytoken = table.Column<string>(name: "concurrency_token", type: "character varying(50)", maxLength: 50, nullable: true),
                    consenttype = table.Column<string>(name: "consent_type", type: "character varying(50)", maxLength: 50, nullable: true),
                    displayname = table.Column<string>(name: "display_name", type: "text", nullable: true),
                    displaynames = table.Column<string>(name: "display_names", type: "text", nullable: true),
                    permissions = table.Column<string>(type: "text", nullable: true),
                    postlogoutredirecturis = table.Column<string>(name: "post_logout_redirect_uris", type: "text", nullable: true),
                    properties = table.Column<string>(type: "text", nullable: true),
                    redirecturis = table.Column<string>(name: "redirect_uris", type: "text", nullable: true),
                    requirements = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scopes",
                schema: "openid",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    concurrencytoken = table.Column<string>(name: "concurrency_token", type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    descriptions = table.Column<string>(type: "text", nullable: true),
                    displayname = table.Column<string>(name: "display_name", type: "text", nullable: true),
                    displaynames = table.Column<string>(name: "display_names", type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    properties = table.Column<string>(type: "text", nullable: true),
                    resources = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scopes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "authorizations",
                schema: "openid",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    applicationid = table.Column<Guid>(name: "application_id", type: "uuid", nullable: true),
                    concurrencytoken = table.Column<string>(name: "concurrency_token", type: "character varying(50)", maxLength: 50, nullable: true),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: true),
                    properties = table.Column<string>(type: "text", nullable: true),
                    scopes = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authorizations", x => x.id);
                    table.ForeignKey(
                        name: "fk_authorizations_applications_application_id",
                        column: x => x.applicationid,
                        principalSchema: "openid",
                        principalTable: "applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tokens",
                schema: "openid",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    applicationid = table.Column<Guid>(name: "application_id", type: "uuid", nullable: true),
                    authorizationid = table.Column<Guid>(name: "authorization_id", type: "uuid", nullable: true),
                    concurrencytoken = table.Column<string>(name: "concurrency_token", type: "character varying(50)", maxLength: 50, nullable: true),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: true),
                    expirationdate = table.Column<DateTime>(name: "expiration_date", type: "timestamp with time zone", nullable: true),
                    payload = table.Column<string>(type: "text", nullable: true),
                    properties = table.Column<string>(type: "text", nullable: true),
                    redemptiondate = table.Column<DateTime>(name: "redemption_date", type: "timestamp with time zone", nullable: true),
                    referenceid = table.Column<string>(name: "reference_id", type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_tokens_applications_application_id",
                        column: x => x.applicationid,
                        principalSchema: "openid",
                        principalTable: "applications",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tokens_authorizations_authorization_id",
                        column: x => x.authorizationid,
                        principalSchema: "openid",
                        principalTable: "authorizations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_applications_client_id",
                schema: "openid",
                table: "applications",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_authorizations_application_id_status_subject_type",
                schema: "openid",
                table: "authorizations",
                columns: new[] { "application_id", "status", "subject", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_scopes_name",
                schema: "openid",
                table: "scopes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tokens_application_id_status_subject_type",
                schema: "openid",
                table: "tokens",
                columns: new[] { "application_id", "status", "subject", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_tokens_authorization_id",
                schema: "openid",
                table: "tokens",
                column: "authorization_id");

            migrationBuilder.CreateIndex(
                name: "ix_tokens_reference_id",
                schema: "openid",
                table: "tokens",
                column: "reference_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scopes",
                schema: "openid");

            migrationBuilder.DropTable(
                name: "tokens",
                schema: "openid");

            migrationBuilder.DropTable(
                name: "authorizations",
                schema: "openid");

            migrationBuilder.DropTable(
                name: "applications",
                schema: "openid");

            migrationBuilder.CreateTable(
                name: "sessions",
                schema: "identity",
                columns: table => new
                {
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    fingerprint = table.Column<string>(type: "text", nullable: false),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    expiresinminutes = table.Column<long>(name: "expires_in_minutes", type: "bigint", nullable: false),
                    refreshtoken = table.Column<string>(name: "refresh_token", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sessions", x => new { x.userid, x.fingerprint });
                    table.ForeignKey(
                        name: "fk_sessions_users_user_id",
                        column: x => x.userid,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
