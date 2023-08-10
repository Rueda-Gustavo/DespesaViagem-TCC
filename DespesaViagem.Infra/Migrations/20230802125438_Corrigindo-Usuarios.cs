using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesaViagem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viagens_Funcionarios_IdFuncionario",
                table: "Viagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios");

            migrationBuilder.RenameTable(
                name: "Funcionarios",
                newName: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "Usuarios",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Usuarios",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AddColumn<string>(
                name: "Funcionario_CPF",
                table: "Usuarios",
                type: "varchar(15)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdGestor",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdViagem",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoDeUsuario",
                table: "Usuarios",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Usuarios",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Funcionario_CPF", "IdGestor", "IdViagem", "PasswordHash", "TipoDeUsuario", "Username" },
                values: new object[] { "321.123.321-12", 0, 0, "", "Funcionario", "" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Viagens_Usuarios_IdFuncionario",
                table: "Viagens",
                column: "IdFuncionario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_IdGestor",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Viagens_Usuarios_IdFuncionario",
                table: "Viagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdGestor",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Funcionario_CPF",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdGestor",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdViagem",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TipoDeUsuario",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Funcionarios");

            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "Funcionarios",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Funcionarios",
                type: "varchar(15)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CPF",
                value: "321.123.321-12");

            migrationBuilder.AddForeignKey(
                name: "FK_Viagens_Funcionarios_IdFuncionario",
                table: "Viagens",
                column: "IdFuncionario",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
