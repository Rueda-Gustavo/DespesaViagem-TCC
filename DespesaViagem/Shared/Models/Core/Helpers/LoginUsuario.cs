using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class LoginUsuario
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
