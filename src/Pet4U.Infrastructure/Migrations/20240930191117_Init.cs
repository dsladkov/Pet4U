using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet4U.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "social_network",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_network", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteers",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    experience = table.Column<int>(type: "integer", nullable: true),
                    phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    midle_name = table.Column<string>(type: "text", nullable: false),
                    PaymentInfos = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nickname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    species = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    breed = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    health = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    height = table.Column<double>(type: "double precision", nullable: false),
                    phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_neutered = table.Column<bool>(type: "boolean", nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateOnly>(type: "date", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalSchema: "core",
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "social_network_volunteer",
                schema: "core",
                columns: table => new
                {
                    social_networks_id = table.Column<Guid>(type: "uuid", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_network_volunteer", x => new { x.social_networks_id, x.volunteer_id });
                    table.ForeignKey(
                        name: "fk_social_network_volunteer_social_network_social_networks_id",
                        column: x => x.social_networks_id,
                        principalSchema: "core",
                        principalTable: "social_network",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_social_network_volunteer_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalSchema: "core",
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pet_photo",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_photo", x => x.id);
                    table.ForeignKey(
                        name: "fk_pet_photo_pets_pet_id",
                        column: x => x.pet_id,
                        principalSchema: "core",
                        principalTable: "pets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_pet_photo_pet_id",
                schema: "core",
                table: "pet_photo",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                schema: "core",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_social_network_volunteer_volunteer_id",
                schema: "core",
                table: "social_network_volunteer",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pet_photo",
                schema: "core");

            migrationBuilder.DropTable(
                name: "social_network_volunteer",
                schema: "core");

            migrationBuilder.DropTable(
                name: "pets",
                schema: "core");

            migrationBuilder.DropTable(
                name: "social_network",
                schema: "core");

            migrationBuilder.DropTable(
                name: "volunteers",
                schema: "core");
        }
    }
}
