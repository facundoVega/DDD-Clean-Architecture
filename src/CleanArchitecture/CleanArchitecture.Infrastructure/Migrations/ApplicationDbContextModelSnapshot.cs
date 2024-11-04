﻿// <auto-generated />
using System;
using CleanArchitecture.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitecture.Domain.Hires.Hire", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CancellationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("cancellation_date");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("completed_date");

                    b.Property<DateTime?>("ConfirmationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("confirmation_date");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<DateTime?>("NegationtionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("negationtion_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uuid")
                        .HasColumnName("vehicle_id");

                    b.HasKey("Id")
                        .HasName("pk_hires");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_hires_user_id");

                    b.HasIndex("VehicleId")
                        .HasDatabaseName("ix_hires_vehicle_id");

                    b.ToTable("hires", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Permissions.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_permissions");

                    b.ToTable("permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ReadUser"
                        },
                        new
                        {
                            Id = 2,
                            Name = "WriteUser"
                        },
                        new
                        {
                            Id = 3,
                            Name = "UpdateUser"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Reviews.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("comment");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<Guid?>("HireId")
                        .HasColumnType("uuid")
                        .HasColumnName("hire_id");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uuid")
                        .HasColumnName("vehicle_id");

                    b.HasKey("Id")
                        .HasName("pk_reviews");

                    b.HasIndex("HireId")
                        .HasDatabaseName("ix_reviews_hire_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_reviews_user_id");

                    b.HasIndex("VehicleId")
                        .HasDatabaseName("ix_reviews_vehicle_id");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Client"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Roles.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer")
                        .HasColumnName("permission_id");

                    b.HasKey("RoleId", "PermissionId")
                        .HasName("pk_roles_permissions");

                    b.HasIndex("PermissionId")
                        .HasDatabaseName("ix_roles_permissions_permission_id");

                    b.ToTable("roles_permissions", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 1
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("email");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("password_hash");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Users.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("RoleId", "UserId")
                        .HasName("pk_users_roles");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_users_roles_user_id");

                    b.ToTable("users_roles", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Vehicles.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int[]>("Appliances")
                        .IsRequired()
                        .HasColumnType("integer[]")
                        .HasColumnName("appliances");

                    b.Property<DateTime?>("LastHireDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_hire_date");

                    b.Property<string>("Model")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("model");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("Vin")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("vin");

                    b.HasKey("Id")
                        .HasName("pk_vehicles");

                    b.ToTable("vehicles", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Ingrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OcurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ocurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Hires.Hire", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_hires_user_user_temp_id1");

                    b.HasOne("CleanArchitecture.Domain.Vehicles.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .HasConstraintName("fk_hires_vehicle_vehicle_temp_id");

                    b.OwnsOne("CleanArchitecture.Domain.Hires.DateRange", "Duration", b1 =>
                        {
                            b1.Property<Guid>("HireId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateOnly>("End")
                                .HasColumnType("date")
                                .HasColumnName("duration_end");

                            b1.Property<DateOnly>("Start")
                                .HasColumnType("date")
                                .HasColumnName("duration_start");

                            b1.HasKey("HireId");

                            b1.ToTable("hires");

                            b1.WithOwner()
                                .HasForeignKey("HireId")
                                .HasConstraintName("fk_hires_hires_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Currency", "Appliances", b1 =>
                        {
                            b1.Property<Guid>("HireId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("appliances_amount");

                            b1.Property<string>("CurrencyType")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("appliances_currency_type");

                            b1.HasKey("HireId");

                            b1.ToTable("hires");

                            b1.WithOwner()
                                .HasForeignKey("HireId")
                                .HasConstraintName("fk_hires_hires_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Currency", "Maintenance", b1 =>
                        {
                            b1.Property<Guid>("HireId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("maintenance_amount");

                            b1.Property<string>("CurrencyType")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("maintenance_currency_type");

                            b1.HasKey("HireId");

                            b1.ToTable("hires");

                            b1.WithOwner()
                                .HasForeignKey("HireId")
                                .HasConstraintName("fk_hires_hires_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Currency", "PeriodPrice", b1 =>
                        {
                            b1.Property<Guid>("HireId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("period_price_amount");

                            b1.Property<string>("CurrencyType")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("period_price_currency_type");

                            b1.HasKey("HireId");

                            b1.ToTable("hires");

                            b1.WithOwner()
                                .HasForeignKey("HireId")
                                .HasConstraintName("fk_hires_hires_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Currency", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("HireId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("total_price_amount");

                            b1.Property<string>("CurrencyType")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("total_price_currency_type");

                            b1.HasKey("HireId");

                            b1.ToTable("hires");

                            b1.WithOwner()
                                .HasForeignKey("HireId")
                                .HasConstraintName("fk_hires_hires_id");
                        });

                    b.Navigation("Appliances");

                    b.Navigation("Duration");

                    b.Navigation("Maintenance");

                    b.Navigation("PeriodPrice");

                    b.Navigation("TotalPrice");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Reviews.Review", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Hires.Hire", null)
                        .WithMany()
                        .HasForeignKey("HireId")
                        .HasConstraintName("fk_reviews_hires_hire_id1");

                    b.HasOne("CleanArchitecture.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_reviews_user_user_temp_id1");

                    b.HasOne("CleanArchitecture.Domain.Vehicles.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .HasConstraintName("fk_reviews_vehicle_vehicle_temp_id");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Roles.RolePermission", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Permissions.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_roles_permissions_permissions_permissions_id");

                    b.HasOne("CleanArchitecture.Domain.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_roles_permissions_roles_role_id");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Users.UserRole", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_roles_roles_role_id");

                    b.HasOne("CleanArchitecture.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_roles_users_user_id1");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Vehicles.Vehicle", b =>
                {
                    b.OwnsOne("CleanArchitecture.Domain.Vehicles.Direction", "Direction", b1 =>
                        {
                            b1.Property<Guid>("VehicleId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direction_city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direction_country");

                            b1.Property<string>("Department")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direction_department");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direction_province");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direction_street");

                            b1.HasKey("VehicleId");

                            b1.ToTable("vehicles");

                            b1.WithOwner()
                                .HasForeignKey("VehicleId")
                                .HasConstraintName("fk_vehicles_vehicles_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Currency", "Maintenance", b1 =>
                        {
                            b1.Property<Guid>("VehicleId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("maintenance_amount");

                            b1.Property<string>("CurrencyType")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("maintenance_currency_type");

                            b1.HasKey("VehicleId");

                            b1.ToTable("vehicles");

                            b1.WithOwner()
                                .HasForeignKey("VehicleId")
                                .HasConstraintName("fk_vehicles_vehicles_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Currency", "Price", b1 =>
                        {
                            b1.Property<Guid>("VehicleId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("price_amount");

                            b1.Property<string>("CurrencyType")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("price_currency_type");

                            b1.HasKey("VehicleId");

                            b1.ToTable("vehicles");

                            b1.WithOwner()
                                .HasForeignKey("VehicleId")
                                .HasConstraintName("fk_vehicles_vehicles_id");
                        });

                    b.Navigation("Direction");

                    b.Navigation("Maintenance");

                    b.Navigation("Price");
                });
#pragma warning restore 612, 618
        }
    }
}
