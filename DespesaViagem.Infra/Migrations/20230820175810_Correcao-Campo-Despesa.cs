using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoCampoDespesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeCadastro",
                table: "Despesas",
                type: "datetime",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeCadastro",
                table: "Despesas");
        }
    }
}
