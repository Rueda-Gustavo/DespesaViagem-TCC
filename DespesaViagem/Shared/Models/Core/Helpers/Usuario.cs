using DespesaViagem.Shared.Models.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class Usuario
    {        
        public int Id { get; set; } = 0;
        [Column(TypeName = "varchar(50)")]
        public string Nome { get; set; } = string.Empty;
        [Column(TypeName = "varchar(1000)")]
        public string Sobrenome { get; set; } = string.Empty;
        [Column(TypeName = "varchar(30)")]
        public string Username { get; set; } = string.Empty;
        [Column(TypeName = "varchar(15)")]
        public string CPF { get; set; } = string.Empty;
        [Column(TypeName = "varchar(20)")]
        public RolesUsuario TipoDeUsuario { get; protected set; }
        public string PasswordHash { get; set;} = string.Empty;
    }
}
