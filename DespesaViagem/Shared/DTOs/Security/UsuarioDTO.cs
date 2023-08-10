using DespesaViagem.Shared.Models.Core.Enums;

namespace DespesaViagem.Shared.DTOs.Security
{
    public class UsuarioDTO
    {                
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
