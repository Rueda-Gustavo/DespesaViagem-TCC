using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoCamposViagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeViagem",
                table: "Viagens",
                type: "varchar(1000)",
                unicode: false,
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoViagem",
                table: "Viagens",
                type: "varchar(3000)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "NomeDespesa",
                table: "Despesas",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoDespesa",
                table: "Despesas",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeViagem",
                table: "Viagens",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldUnicode: false,
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoViagem",
                table: "Viagens",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "NomeDespesa",
                table: "Despesas",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)");

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoDespesa",
                table: "Despesas",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");
        }
    }
}
