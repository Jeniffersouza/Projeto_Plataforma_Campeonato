using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaJiujitsu.Migrations
{
    /// <inheritdoc />
    public partial class CreateAcademiaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Academias",
                newName: "CNPJ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Academias",
                newName: "Telefone");
        }
    }
}
