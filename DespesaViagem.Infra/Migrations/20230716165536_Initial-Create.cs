using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "varchar(1000)", nullable: false),
                    NumeroCasa = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(1000)", nullable: false),
                    CPF = table.Column<string>(type: "varchar(15)", nullable: false),
                    Matricula = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Viagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeViagem = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DescricaoViagem = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    Adiantamento = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDespesas = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    StatusViagem = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    IdFuncionario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viagens_Funcionarios_IdFuncionario",
                        column: x => x.IdFuncionario,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeDespesa = table.Column<string>(type: "varchar(30)", nullable: false),
                    DescricaoDespesa = table.Column<string>(type: "varchar(200)", nullable: false),
                    TotalDespesa = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataDespesa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoDespesa = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    IdViagem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_Viagens_IdViagem",
                        column: x => x.IdViagem,
                        principalTable: "Viagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "CPF", "Matricula", "Nome", "Sobrenome" },
                values: new object[] { 1, "321.123.321-12", "A65SD1ASD", "Gustavo", "Rueda dos Reis" });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_IdViagem",
                table: "Despesas",
                column: "IdViagem");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasHospedagem_IdEndereco",
                table: "DespesasHospedagem",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Viagens_IdFuncionario",
                table: "Viagens",
                column: "IdFuncionario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespesasHospedagem");

            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Viagens");

            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
