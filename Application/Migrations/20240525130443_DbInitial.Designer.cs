﻿// <auto-generated />
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Application.Migrations
{
    [DbContext(typeof(AdminDbContext))]
    [Migration("20240525130443_DbInitial")]
    partial class DbInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Application.Models.TaskDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("ApprovalWith")
                        .HasColumnType("integer");

                    b.Property<int>("EscalatedTo")
                        .HasColumnType("integer");

                    b.HasKey("ID")
                        .HasName("task_pkey");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Application.Models.TaskAssigned", b =>
                {
                    b.Property<int>("TaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TaskID"));

                    b.Property<int>("AssignedTo")
                        .HasColumnType("integer");

                    b.HasKey("TaskID")
                        .HasName("taskassigned_pkey");

                    b.ToTable("TaskAssigneds");
                });

            modelBuilder.Entity("Application.Models.TaskOnlineAssigned", b =>
                {
                    b.Property<int>("TenantID")
                        .HasColumnType("integer");

                    b.Property<int>("TaskID")
                        .HasColumnType("integer");

                    b.Property<int>("AssignedTo")
                        .HasColumnType("integer");

                    b.HasKey("TenantID", "TaskID");

                    b.ToTable("TasksOnlineAssigned");
                });

            modelBuilder.Entity("Application.Models.TaskResponsibleUserDTO", b =>
                {
                    b.Property<int>("TaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TaskID"));

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("TaskID")
                        .HasName("taskresponcibleuser_pkey");

                    b.ToTable("TaskResponsibleUsers");
                });

            modelBuilder.Entity("Application.Models.TaskUserCacheDTO", b =>
                {
                    b.Property<int>("TaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TaskID"));

                    b.Property<byte>("TaskListCategoryID")
                        .HasColumnType("smallint");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("TaskID")
                        .HasName("taskusercache_pkey");

                    b.ToTable("TaskUserCacheDTO");
                });

            modelBuilder.Entity("Application.Models.UserDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.HasKey("ID")
                        .HasName("user_pkey");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Application.Models.UserTaskListCategoryDTO", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserID"));

                    b.Property<byte>("TaskListCategoryID")
                        .HasColumnType("smallint");

                    b.HasKey("UserID")
                        .HasName("usertasklistcategory_pkey");

                    b.ToTable("UserTasksListCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
