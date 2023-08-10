using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class Gestor : Usuario
    {
        [Column(TypeName = "varchar(15)")]        
        [JsonIgnore]
        public List<Funcionario> Funcionarios { get; set; } = new();

        public Gestor()
        {
            TipoDeUsuario = Enums.RolesUsuario.Gestor;
        }
    }
}
