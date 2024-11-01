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
                name: "fk_pets_volunteers_volunteer_id",
                schema: "core",
                table: "pets");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "core",
                table: "volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "core",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                schema: "core",
                table: "pets",
                column: "volunteer_id",
                principalSchema: "core",
                principalTable: "volunteers",
                principalColumn: "volunteer_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                schema: "core",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "core",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "core",
                table: "pets");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                schema: "core",
                table: "pets",
                column: "volunteer_id",
                principalSchema: "core",
                principalTable: "volunteers",
                principalColumn: "volunteer_id");
        }
    }
}
