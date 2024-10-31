using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet4U.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breed_species_breed_id",
                schema: "core",
                table: "breed");

            migrationBuilder.RenameColumn(
                name: "breed_id",
                schema: "core",
                table: "breed",
                newName: "species_id");

            migrationBuilder.RenameIndex(
                name: "ix_breed_breed_id",
                schema: "core",
                table: "breed",
                newName: "ix_breed_species_id");

            migrationBuilder.AlterColumn<string>(
                name: "social_networks",
                schema: "core",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "payment_infos",
                schema: "core",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "core",
                table: "species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                schema: "core",
                table: "breed",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "core",
                table: "breed",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "core",
                table: "breed",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_breed_species_species_id",
                schema: "core",
                table: "breed",
                column: "species_id",
                principalSchema: "core",
                principalTable: "species",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breed_species_species_id",
                schema: "core",
                table: "breed");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "core",
                table: "species");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "core",
                table: "breed");

            migrationBuilder.RenameColumn(
                name: "species_id",
                schema: "core",
                table: "breed",
                newName: "breed_id");

            migrationBuilder.RenameIndex(
                name: "ix_breed_species_id",
                schema: "core",
                table: "breed",
                newName: "ix_breed_breed_id");

            migrationBuilder.AlterColumn<string>(
                name: "social_networks",
                schema: "core",
                table: "volunteers",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "payment_infos",
                schema: "core",
                table: "volunteers",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                schema: "core",
                table: "breed",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "core",
                table: "breed",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddForeignKey(
                name: "fk_breed_species_breed_id",
                schema: "core",
                table: "breed",
                column: "breed_id",
                principalSchema: "core",
                principalTable: "species",
                principalColumn: "id");
        }
    }
}
