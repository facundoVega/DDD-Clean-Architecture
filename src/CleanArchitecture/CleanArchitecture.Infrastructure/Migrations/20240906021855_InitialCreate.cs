using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    password_hash = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    model = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    vin = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    direction_country = table.Column<string>(type: "text", nullable: true),
                    direction_department = table.Column<string>(type: "text", nullable: true),
                    direction_province = table.Column<string>(type: "text", nullable: true),
                    direction_city = table.Column<string>(type: "text", nullable: true),
                    direction_street = table.Column<string>(type: "text", nullable: true),
                    price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    price_currency_type = table.Column<string>(type: "text", nullable: true),
                    maintenance_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    maintenance_currency_type = table.Column<string>(type: "text", nullable: true),
                    last_hire_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    appliances = table.Column<int[]>(type: "integer[]", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles_permissions",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_roles_permissions_permissions_permissions_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_roles_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_roles", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_users_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_users_roles_users_user_id1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    period_price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    period_price_currency_type = table.Column<string>(type: "text", nullable: true),
                    maintenance_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    maintenance_currency_type = table.Column<string>(type: "text", nullable: true),
                    appliances_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    appliances_currency_type = table.Column<string>(type: "text", nullable: true),
                    total_price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    total_price_currency_type = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    duration_start = table.Column<DateOnly>(type: "date", nullable: true),
                    duration_end = table.Column<DateOnly>(type: "date", nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    confirmation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    negationtion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    completed_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cancellation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hires", x => x.id);
                    table.ForeignKey(
                        name: "fk_hires_user_user_temp_id1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_hires_vehicle_vehicle_temp_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: true),
                    hire_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_hires_hire_id1",
                        column: x => x.hire_id,
                        principalTable: "hires",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_reviews_user_user_temp_id1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_reviews_vehicle_vehicle_temp_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "ReadUser" },
                    { 2, "WriteUser" },
                    { 3, "UpdateUser" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Client" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "roles_permissions",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_hires_user_id",
                table: "hires",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_hires_vehicle_id",
                table: "hires",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_hire_id",
                table: "reviews",
                column: "hire_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_vehicle_id",
                table: "reviews",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_permissions_permission_id",
                table: "roles_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_roles_user_id",
                table: "users_roles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "roles_permissions");

            migrationBuilder.DropTable(
                name: "users_roles");

            migrationBuilder.DropTable(
                name: "hires");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vehicles");
        }
    }
}
