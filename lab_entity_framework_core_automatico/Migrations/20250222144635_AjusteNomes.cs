using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab_entity_framework_core_automatico.Migrations
{
    /// <inheritdoc />
    public partial class AjusteNomes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Estado",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Estado",
                newName: "Name");
        }
    }
}
