using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true)
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
                name: "hires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    period_price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    period_price_currency_type = table.Column<string>(type: "text", nullable: true),
                    maintenance_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    maintenance_currency_type = table.Column<string>(type: "text", nullable: true),
                    appliances_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    appliances_currency_type = table.Column<string>(type: "text", nullable: true),
                    total_price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    total_price_currency_type = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    duration_start = table.Column<DateOnly>(type: "date", nullable: false),
                    duration_end = table.Column<DateOnly>(type: "date", nullable: false),
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
                        name: "fk_hires_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_hires_vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hire_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_hires_hire_id",
                        column: x => x.hire_id,
                        principalTable: "hires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "hires");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vehicles");
        }
    }
}
