using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Corrigindo_Nomenclatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesaAlimentacao_Despesas_Id",
                table: "DespesaAlimentacao");

            migrationBuilder.DropForeignKey(
                name: "FK_DespesaDeslocamento_Despesas_Id",
                table: "DespesaDeslocamento");

            migrationBuilder.DropForeignKey(
                name: "FK_DespesaPassagem_Despesas_Id",
                table: "DespesaPassagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DespesaPassagem",
                table: "DespesaPassagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DespesaDeslocamento",
                table: "DespesaDeslocamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DespesaAlimentacao",
                table: "DespesaAlimentacao");

            migrationBuilder.RenameTable(
                name: "DespesaPassagem",
                newName: "DespesasPassagem");

            migrationBuilder.RenameTable(
                name: "DespesaDeslocamento",
                newName: "DespesasDeslocamento");

            migrationBuilder.RenameTable(
                name: "DespesaAlimentacao",
                newName: "DespesasAlimentacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DespesasPassagem",
                table: "DespesasPassagem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DespesasDeslocamento",
                table: "DespesasDeslocamento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DespesasAlimentacao",
                table: "DespesasAlimentacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasAlimentacao_Despesas_Id",
                table: "DespesasAlimentacao",
                column: "Id",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasDeslocamento_Despesas_Id",
                table: "DespesasDeslocamento",
                column: "Id",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasPassagem_Despesas_Id",
                table: "DespesasPassagem",
                column: "Id",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesasAlimentacao_Despesas_Id",
                table: "DespesasAlimentacao");

            migrationBuilder.DropForeignKey(
                name: "FK_DespesasDeslocamento_Despesas_Id",
                table: "DespesasDeslocamento");

            migrationBuilder.DropForeignKey(
                name: "FK_DespesasPassagem_Despesas_Id",
                table: "DespesasPassagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DespesasPassagem",
                table: "DespesasPassagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DespesasDeslocamento",
                table: "DespesasDeslocamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DespesasAlimentacao",
                table: "DespesasAlimentacao");

            migrationBuilder.RenameTable(
                name: "DespesasPassagem",
                newName: "DespesaPassagem");

            migrationBuilder.RenameTable(
                name: "DespesasDeslocamento",
                newName: "DespesaDeslocamento");

            migrationBuilder.RenameTable(
                name: "DespesasAlimentacao",
                newName: "DespesaAlimentacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DespesaPassagem",
                table: "DespesaPassagem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DespesaDeslocamento",
                table: "DespesaDeslocamento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DespesaAlimentacao",
                table: "DespesaAlimentacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaAlimentacao_Despesas_Id",
                table: "DespesaAlimentacao",
                column: "Id",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaDeslocamento_Despesas_Id",
                table: "DespesaDeslocamento",
                column: "Id",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaPassagem_Despesas_Id",
                table: "DespesaPassagem",
                column: "Id",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
