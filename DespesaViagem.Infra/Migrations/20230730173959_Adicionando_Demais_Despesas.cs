using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Adicionando_Demais_Despesas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DespesaAlimentacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NomeEstabelecimento = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ValorRefeicao = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaAlimentacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesaAlimentacao_Despesas_Id",
                        column: x => x.Id,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesaDeslocamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Quilometragem = table.Column<long>(type: "bigint", nullable: false),
                    ValorPorQuilometro = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Placa = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Modelo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaDeslocamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesaDeslocamento_Despesas_Id",
                        column: x => x.Id,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesaPassagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Companhia = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Origem = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Destino = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    DataHoraEmbarque = table.Column<DateTime>(type: "datetime", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaPassagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesaPassagem_Despesas_Id",
                        column: x => x.Id,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespesaAlimentacao");

            migrationBuilder.DropTable(
                name: "DespesaDeslocamento");

            migrationBuilder.DropTable(
                name: "DespesaPassagem");
        }
    }
}
