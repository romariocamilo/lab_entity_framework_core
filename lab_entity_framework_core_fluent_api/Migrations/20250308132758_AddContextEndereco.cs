using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab_entity_framework_core_fluent_api.Migrations
{
    /// <inheritdoc />
    public partial class AddContextEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Endereco",
                type: "VARCHAR(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Endereco",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Endereco",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Endereco",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "idx_endereco_id",
                table: "Endereco",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_endereco_id",
                table: "Endereco");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Endereco",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Endereco",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Endereco",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Endereco",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");
        }
    }
}
