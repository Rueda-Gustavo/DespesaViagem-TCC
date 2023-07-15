using DespesaViagem.Shared.Models.Core.Enums;

namespace DespesaViagem.Shared.DTOs.Security
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now;
        public string Role { get; set; } = RolesUsuario.Funcionario.ToString();
    }
}
