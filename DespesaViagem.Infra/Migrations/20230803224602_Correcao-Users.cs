using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_IdGestor",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdGestor",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IdGestor",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "IdViagem",
                table: "Usuarios",
                newName: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GestorId",
                table: "Usuarios",
                column: "GestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_GestorId",
                table: "Usuarios",
                column: "GestorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_GestorId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_GestorId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "GestorId",
                table: "Usuarios",
                newName: "IdViagem");

            migrationBuilder.AddColumn<int>(
                name: "IdGestor",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Funcionario_CPF", "IdGestor", "IdViagem", "Matricula", "Nome", "PasswordHash", "Sobrenome", "TipoDeUsuario", "Username" },
                values: new object[] { 1, "321.123.321-12", 0, 0, "A65SD1ASD", "Gustavo", "", "Rueda dos Reis", "Funcionario", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdGestor",
                table: "Usuarios",
                column: "IdGestor");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_IdGestor",
                table: "Usuarios",
                column: "IdGestor",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
