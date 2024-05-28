using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class Adduserdistrictmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userdistrict",
                schema: "adm",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    DistrictID = table.Column<int>(type: "integer", nullable: false),
                    TenantID = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userdistrict_pkey", x => new { x.DistrictID, x.UserID });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userdistrict",
                schema: "adm");
        }
    }
}
