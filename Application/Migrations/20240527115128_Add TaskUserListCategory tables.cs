using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskUserListCategorytables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "adm");

            migrationBuilder.CreateTable(
                name: "ListCategories",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "smallint", nullable: false),
                    Permissionextid = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("listcategory_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "rolepermissionext",
                schema: "adm",
                columns: table => new
                {
                    TenantID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleID = table.Column<int>(type: "integer", nullable: false),
                    Permissionextid = table.Column<short>(type: "smallint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rolepermissionext_pkey", x => x.TenantID);
                });

            migrationBuilder.CreateTable(
                name: "userrole",
                schema: "adm",
                columns: table => new
                {
                    TenantID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    RoleID = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userrole_pkey", x => x.TenantID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListCategories");

            migrationBuilder.DropTable(
                name: "rolepermissionext",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "userrole",
                schema: "adm");
        }
    }
}
