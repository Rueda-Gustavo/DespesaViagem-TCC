using DespesaViagem.Shared.Models.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class CadastroUsuario
    {
        [Required]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string CPF { get; set; } = string.Empty;
        public string? Matricula { get; set; } = string.Empty;
        public Departamento? Departamento { get; set; }
        public RolesUsuario TipoDeUsuario { get; set; } = RolesUsuario.Funcionario;
        [Required, StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter de 6 a 100 caracteres.")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Senhas diferentes.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
