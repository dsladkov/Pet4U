using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet4U.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                schema: "core",
                table: "pets");

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
