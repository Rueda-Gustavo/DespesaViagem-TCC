using DespesaViagem.Shared.Models.Core.Enums;

namespace DespesaViagem.Shared.DTOs.Helpers
{
    public class UsuarioDTO
    {
        public int Id { get; set; } = 0;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }        
    }
}
