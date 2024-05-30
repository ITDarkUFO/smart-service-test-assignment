using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class EditUserRolepkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "userrole_pkey",
                schema: "adm",
                table: "userrole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskOnlineAssigneds",
                table: "TasksOnlineAssigned");

            migrationBuilder.AlterColumn<short>(
                name: "TenantID",
                schema: "adm",
                table: "userrole",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "userrole_pkey",
                schema: "adm",
                table: "userrole",
                columns: new[] { "TenantID", "UserID" });

            migrationBuilder.AddPrimaryKey(
                name: "taskonlineassigned_pkey",
                table: "TasksOnlineAssigned",
                columns: new[] { "TenantID", "TaskID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "userrole_pkey",
                schema: "adm",
                table: "userrole");

            migrationBuilder.DropPrimaryKey(
                name: "taskonlineassigned_pkey",
                table: "TasksOnlineAssigned");

            migrationBuilder.AlterColumn<short>(
                name: "TenantID",
                schema: "adm",
                table: "userrole",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "userrole_pkey",
                schema: "adm",
                table: "userrole",
                column: "TenantID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskOnlineAssigneds",
                table: "TasksOnlineAssigned",
                columns: new[] { "TenantID", "TaskID" });
        }
    }
}
