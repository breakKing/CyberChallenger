using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProviderService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteForUserAndRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "identity",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "identity",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "identity",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "identity",
                table: "roles");
        }
    }
}
