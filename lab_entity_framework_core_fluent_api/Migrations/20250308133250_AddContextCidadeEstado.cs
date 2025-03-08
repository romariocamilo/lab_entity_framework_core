using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab_entity_framework_core_fluent_api.Migrations
{
    /// <inheritdoc />
    public partial class AddContextCidadeEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_endereco_id",
                table: "Endereco");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estado",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Cidade",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estado",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Cidade",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");

            migrationBuilder.CreateIndex(
                name: "idx_endereco_id",
                table: "Endereco",
                column: "Id");
        }
    }
}
