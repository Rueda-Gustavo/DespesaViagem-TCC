using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.DTOs.Security
{
    public class UsuarioLogin
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
