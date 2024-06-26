﻿// <auto-generated />
using System;
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
    [Migration("20240530162924_Remove other DTOs, add new tables")]
    partial class RemoveotherDTOsaddnewtables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Application.Models.RolePermissionExt", b =>
                {
                    b.Property<short>("TenantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("TenantID"));

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<short?>("Permissionextid")
                        .HasColumnType("smallint");

                    b.Property<int>("RoleID")
                        .HasColumnType("integer");

                    b.HasKey("TenantID")
                        .HasName("rolepermissionext_pkey");

                    b.ToTable("rolepermissionext", "adm");
                });

            modelBuilder.Entity("Application.Models.TaskListCategory", b =>
                {
                    b.Property<byte>("ID")
                        .HasColumnType("smallint");

                    b.Property<short?>("Permissionextid")
                        .HasColumnType("smallint");

                    b.HasKey("ID")
                        .HasName("tasklistcategory_pkey");

                    b.ToTable("tasklistcategory", "work");
                });

            modelBuilder.Entity("Application.Models.TaskOnlineAssigned", b =>
                {
                    b.Property<int>("TenantID")
                        .HasColumnType("integer");

                    b.Property<int>("TaskID")
                        .HasColumnType("integer");

                    b.Property<int?>("AssignedTo")
                        .HasColumnType("integer");

                    b.HasKey("TenantID", "TaskID")
                        .HasName("taskonlineassigned_pkey");

                    b.ToTable("taskonlineassigned", "work");
                });

            modelBuilder.Entity("Application.Models.TenantMember", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<short>("TenantID")
                        .HasColumnType("smallint");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID")
                        .HasName("tenantmember_pkey");

                    b.ToTable("tenantmember", "adm");
                });

            modelBuilder.Entity("Application.Models.UserDistrict", b =>
                {
                    b.Property<int>("DistrictID")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("TenantID")
                        .HasColumnType("integer");

                    b.HasKey("DistrictID", "UserID")
                        .HasName("userdistrict_pkey");

                    b.ToTable("userdistrict", "adm");
                });

            modelBuilder.Entity("Application.Models.UserRole", b =>
                {
                    b.Property<short>("TenantID")
                        .HasColumnType("smallint");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("RoleID")
                        .HasColumnType("integer");

                    b.HasKey("TenantID", "UserID")
                        .HasName("userrole_pkey");

                    b.ToTable("userrole", "adm");
                });

            modelBuilder.Entity("Application.Models.UserWorkType", b =>
                {
                    b.Property<int>("WorkTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WorkTypeID"));

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<short>("TenantID")
                        .HasColumnType("smallint");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("WorkTypeID")
                        .HasName("userworktype_pkey");

                    b.ToTable("userworktype", "pa");
                });

            modelBuilder.Entity("Application.Models.WorkType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<short>("TenantID")
                        .HasColumnType("smallint");

                    b.HasKey("ID")
                        .HasName("worktype_pkey");

                    b.ToTable("worktype", "work");
                });
#pragma warning restore 612, 618
        }
    }
}
