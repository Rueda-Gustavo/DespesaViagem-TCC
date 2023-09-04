using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class LoginUsuario
    {
        [Required (ErrorMessage = "Por favor, digite um nome de usuário válido.")]
        public string Username { get; set; } = string.Empty;
        [Required (ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; } = string.Empty;
    }
}
