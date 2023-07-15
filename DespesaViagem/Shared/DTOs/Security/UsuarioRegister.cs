using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.DTOs.Security
{
    public class UsuarioRegister
    {
        [Required, StringLength(20, MinimumLength = 4)]
        public string UserName { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
