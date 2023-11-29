using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class criacaosqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "varchar(50)", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCompleto = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Username = table.Column<string>(type: "varchar(30)", nullable: false),
                    CPF = table.Column<string>(type: "varchar(15)", nullable: true),
                    TipoDeUsuario = table.Column<int>(type: "varchar(20)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Matricula = table.Column<string>(type: "varchar(30)", nullable: true),
                    GestorId = table.Column<int>(type: "INTEGER", nullable: true),
                    DepartamentoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Viagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeViagem = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 1000, nullable: false),
                    DescricaoViagem = table.Column<string>(type: "varchar(3000)", unicode: false, nullable: false),
                    Adiantamento = table.Column<decimal>(type: "decimal", precision: 10, scale: 2, nullable: false),
                    DataInicial = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalDespesas = table.Column<decimal>(type: "decimal", precision: 10, scale: 2, nullable: false),
                    StatusViagem = table.Column<int>(type: "varchar", unicode: false, maxLength: 15, nullable: false),
                    IdFuncionario = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viagens_Usuarios_IdFuncionario",
                        column: x => x.IdFuncionario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeDespesa = table.Column<string>(type: "varchar(1000)", nullable: false),
                    DescricaoDespesa = table.Column<string>(type: "varchar(3000)", nullable: false),
                    TotalDespesa = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataDespesa = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataDeCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    TipoDespesa = table.Column<int>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    IdViagem = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "DespesasAlimentacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeEstabelecimento = table.Column<string>(type: "TEXT", unicode: false, maxLength: 30, nullable: false),
                    CNPJ = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    ValorRefeicao = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasAlimentacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesasAlimentacao_Despesas_Id",
                        column: x => x.Id,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesasDeslocamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quilometragem = table.Column<long>(type: "bigint", nullable: false),
                    ValorPorQuilometro = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Placa = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasDeslocamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesasDeslocamento_Despesas_Id",
                        column: x => x.Id,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesasHospedagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuantidadeDias = table.Column<int>(type: "integer", nullable: false),
                    ValorDiaria = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(1000)", nullable: false),
                    NumeroCasa = table.Column<int>(type: "INTEGER", nullable: false),
                    CEP = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(100)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "DespesasPassagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Companhia = table.Column<string>(type: "TEXT", unicode: false, maxLength: 40, nullable: false),
                    Origem = table.Column<string>(type: "TEXT", unicode: false, maxLength: 40, nullable: false),
                    Destino = table.Column<string>(type: "TEXT", unicode: false, maxLength: 40, nullable: false),
                    DataHoraEmbarque = table.Column<DateTime>(type: "datetime", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasPassagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesasPassagem_Despesas_Id",
                        column: x => x.Id,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(1000)", nullable: false),
                    NumeroCasa = table.Column<int>(type: "INTEGER", nullable: false),
                    CEP = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_DespesasHospedagem_Id",
                        column: x => x.Id,
                        principalTable: "DespesasHospedagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_Descricao",
                table: "Departamentos",
                column: "Descricao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_IdViagem",
                table: "Despesas",
                column: "IdViagem");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DepartamentoId",
                table: "Usuarios",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GestorId",
                table: "Usuarios",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Viagens_IdFuncionario",
                table: "Viagens",
                column: "IdFuncionario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespesasAlimentacao");

            migrationBuilder.DropTable(
                name: "DespesasDeslocamento");

            migrationBuilder.DropTable(
                name: "DespesasPassagem");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "DespesasHospedagem");

            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "Viagens");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
