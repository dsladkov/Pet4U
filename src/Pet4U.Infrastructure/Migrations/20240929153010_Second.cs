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
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id1",
                schema: "core",
                table: "pets");

            migrationBuilder.DropIndex(
                name: "ix_pets_volunteer_id1",
                schema: "core",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "volunteer_id1",
                schema: "core",
                table: "pets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "volunteer_id1",
                schema: "core",
                table: "pets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id1",
                schema: "core",
                table: "pets",
                column: "volunteer_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id1",
                schema: "core",
                table: "pets",
                column: "volunteer_id1",
                principalSchema: "core",
                principalTable: "volunteers",
                principalColumn: "id");
        }
    }
}
