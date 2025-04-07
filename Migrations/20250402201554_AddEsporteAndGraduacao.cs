using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaJiujitsu.Migrations
{
    /// <inheritdoc />
    public partial class AddEsporteAndGraduacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atletas_Esporte_EsporteId",
                table: "Atletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Atletas_Graduacao_GraduacaoId",
                table: "Atletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Graduacao_Esporte_EsporteId",
                table: "Graduacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Graduacao",
                table: "Graduacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Esporte",
                table: "Esporte");

            migrationBuilder.RenameTable(
                name: "Graduacao",
                newName: "Graduacoes");

            migrationBuilder.RenameTable(
                name: "Esporte",
                newName: "Esportes");

            migrationBuilder.RenameIndex(
                name: "IX_Graduacao_EsporteId",
                table: "Graduacoes",
                newName: "IX_Graduacoes_EsporteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Graduacoes",
                table: "Graduacoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Esportes",
                table: "Esportes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atletas_Esportes_EsporteId",
                table: "Atletas",
                column: "EsporteId",
                principalTable: "Esportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atletas_Graduacoes_GraduacaoId",
                table: "Atletas",
                column: "GraduacaoId",
                principalTable: "Graduacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Graduacoes_Esportes_EsporteId",
                table: "Graduacoes",
                column: "EsporteId",
                principalTable: "Esportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atletas_Esportes_EsporteId",
                table: "Atletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Atletas_Graduacoes_GraduacaoId",
                table: "Atletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Graduacoes_Esportes_EsporteId",
                table: "Graduacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Graduacoes",
                table: "Graduacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Esportes",
                table: "Esportes");

            migrationBuilder.RenameTable(
                name: "Graduacoes",
                newName: "Graduacao");

            migrationBuilder.RenameTable(
                name: "Esportes",
                newName: "Esporte");

            migrationBuilder.RenameIndex(
                name: "IX_Graduacoes_EsporteId",
                table: "Graduacao",
                newName: "IX_Graduacao_EsporteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Graduacao",
                table: "Graduacao",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Esporte",
                table: "Esporte",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atletas_Esporte_EsporteId",
                table: "Atletas",
                column: "EsporteId",
                principalTable: "Esporte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atletas_Graduacao_GraduacaoId",
                table: "Atletas",
                column: "GraduacaoId",
                principalTable: "Graduacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Graduacao_Esporte_EsporteId",
                table: "Graduacao",
                column: "EsporteId",
                principalTable: "Esporte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
