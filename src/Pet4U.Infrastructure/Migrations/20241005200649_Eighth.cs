using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet4U.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Eighth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "pet_data_breed_id",
                schema: "core",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "pet_data_species_id",
                schema: "core",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "species",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "breed",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breed", x => x.id);
                    table.ForeignKey(
                        name: "fk_breed_species_breed_id",
                        column: x => x.breed_id,
                        principalSchema: "core",
                        principalTable: "species",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_breed_breed_id",
                schema: "core",
                table: "breed",
                column: "breed_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breed",
                schema: "core");

            migrationBuilder.DropTable(
                name: "species",
                schema: "core");

            migrationBuilder.DropColumn(
                name: "pet_data_breed_id",
                schema: "core",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "pet_data_species_id",
                schema: "core",
                table: "pets");
        }
    }
}
