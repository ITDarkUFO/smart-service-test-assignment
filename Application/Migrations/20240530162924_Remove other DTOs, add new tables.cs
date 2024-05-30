using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class RemoveotherDTOsaddnewtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListCategories");

            migrationBuilder.DropTable(
                name: "TaskResponsibleUsers");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskUserCacheDTO");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTasksListCategories");

            migrationBuilder.EnsureSchema(
                name: "work");

            migrationBuilder.EnsureSchema(
                name: "pa");

            migrationBuilder.RenameTable(
                name: "TasksOnlineAssigned",
                newName: "taskonlineassigned",
                newSchema: "work");

            migrationBuilder.CreateTable(
                name: "tasklistcategory",
                schema: "work",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "smallint", nullable: false),
                    Permissionextid = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tasklistcategory_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tenantmember",
                schema: "adm",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    TenantID = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tenantmember_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "userworktype",
                schema: "pa",
                columns: table => new
                {
                    WorkTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantID = table.Column<short>(type: "smallint", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userworktype_pkey", x => x.WorkTypeID);
                });

            migrationBuilder.CreateTable(
                name: "worktype",
                schema: "work",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantID = table.Column<short>(type: "smallint", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("worktype_pkey", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasklistcategory",
                schema: "work");

            migrationBuilder.DropTable(
                name: "tenantmember",
                schema: "adm");

            migrationBuilder.DropTable(
                name: "userworktype",
                schema: "pa");

            migrationBuilder.DropTable(
                name: "worktype",
                schema: "work");

            migrationBuilder.RenameTable(
                name: "taskonlineassigned",
                schema: "work",
                newName: "TasksOnlineAssigned");

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
                name: "TaskResponsibleUsers",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("taskresponcibleuser_pkey", x => x.TaskID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApprovalWith = table.Column<int>(type: "integer", nullable: false),
                    EscalatedTo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("task_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaskUserCacheDTO",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskListCategoryID = table.Column<byte>(type: "smallint", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("taskusercache_pkey", x => x.TaskID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserTasksListCategories",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskListCategoryID = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usertasklistcategory_pkey", x => x.UserID);
                });
        }
    }
}
