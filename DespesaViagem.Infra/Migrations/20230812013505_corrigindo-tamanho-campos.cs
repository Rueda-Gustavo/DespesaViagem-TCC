using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class corrigindotamanhocampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeViagem",
                table: "Viagens",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoViagem",
                table: "Viagens",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldUnicode: false,
                oldMaxLength: 60);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeViagem",
                table: "Viagens",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoViagem",
                table: "Viagens",
                type: "varchar(60)",
                unicode: false,
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);
        }
    }
}
