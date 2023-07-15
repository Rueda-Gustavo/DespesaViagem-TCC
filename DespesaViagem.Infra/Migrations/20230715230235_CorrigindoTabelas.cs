using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Enderecos_IdEndereco",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_IdEndereco",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "IdEndereco",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "QuantidadeDias",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "ValorDiaria",
                table: "Despesas");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDespesa",
                table: "Despesas",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "DespesasHospedagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    QuantidadeDias = table.Column<int>(type: "integer", nullable: false),
                    ValorDiaria = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    IdEndereco = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasHospedagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesasHospedagem_Despesas_Id",
                        column: x => x.Id,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DespesasHospedagem_Enderecos_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DespesasHospedagem_IdEndereco",
                table: "DespesasHospedagem",
                column: "IdEndereco");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespesasHospedagem");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDespesa",
                table: "Despesas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Despesas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdEndereco",
                table: "Despesas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeDias",
                table: "Despesas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorDiaria",
                table: "Despesas",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_IdEndereco",
                table: "Despesas",
                column: "IdEndereco");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Enderecos_IdEndereco",
                table: "Despesas",
                column: "IdEndereco",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
