using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_DespesasHospedagem_Enderecos_IdEndereco",
                table: "DespesasHospedagem");

            migrationBuilder.DropIndex(
                name: "IX_DespesasHospedagem_IdEndereco",
                table: "DespesasHospedagem");*/
            /*
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Enderecos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
            */
            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "DespesasHospedagem",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "DespesasHospedagem",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "DespesasHospedagem",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "DespesasHospedagem",
                type: "varchar(1000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumeroCasa",
                table: "DespesasHospedagem",
                type: "int",
                nullable: false,
                defaultValue: 0);
            /*
            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_DespesasHospedagem_Id",
                table: "Enderecos",
                column: "Id",
                principalTable: "DespesasHospedagem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_DespesasHospedagem_Id",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "DespesasHospedagem");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "DespesasHospedagem");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "DespesasHospedagem");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "DespesasHospedagem");

            migrationBuilder.DropColumn(
                name: "NumeroCasa",
                table: "DespesasHospedagem");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Enderecos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasHospedagem_IdEndereco",
                table: "DespesasHospedagem",
                column: "IdEndereco");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasHospedagem_Enderecos_IdEndereco",
                table: "DespesasHospedagem",
                column: "IdEndereco",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
