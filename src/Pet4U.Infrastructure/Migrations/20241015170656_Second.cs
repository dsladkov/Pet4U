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
            migrationBuilder.RenameColumn(
                name: "SocialNetworks",
                schema: "core",
                table: "volunteers",
                newName: "social_networks");

            migrationBuilder.RenameColumn(
                name: "PaymentInfos",
                schema: "core",
                table: "volunteers",
                newName: "payment_infos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "social_networks",
                schema: "core",
                table: "volunteers",
                newName: "SocialNetworks");

            migrationBuilder.RenameColumn(
                name: "payment_infos",
                schema: "core",
                table: "volunteers",
                newName: "PaymentInfos");
        }
    }
}
