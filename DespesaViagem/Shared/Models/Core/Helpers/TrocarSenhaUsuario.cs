using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class TrocarSenhaUsuario
    {
        [Required, StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter de 6 a 100 caracteres.")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Senhas diferentes.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
