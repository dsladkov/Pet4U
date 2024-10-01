using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet4U.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "social_network_volunteer",
                schema: "core");

            migrationBuilder.DropTable(
                name: "social_network",
                schema: "core");

            migrationBuilder.AddColumn<string>(
                name: "SocialNetworks",
                schema: "core",
                table: "volunteers",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialNetworks",
                schema: "core",
                table: "volunteers");

            migrationBuilder.CreateTable(
                name: "social_network",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_network", x => x.id);
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

            migrationBuilder.CreateIndex(
                name: "ix_social_network_volunteer_volunteer_id",
                schema: "core",
                table: "social_network_volunteer",
                column: "volunteer_id");
        }
    }
}
