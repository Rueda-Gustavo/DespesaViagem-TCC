using DespesaViagem.Shared.Models.Viagens;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class Funcionario : Usuario
    {
        [Column(TypeName = "varchar(30)")]
        public string Matricula { get; set; } = string.Empty;
        [Column(TypeName = "varchar(15)")]        
        //public int IdGestor { get; set; }
        //[JsonIgnore]
        public Gestor? Gestor { get; set; }        
        [JsonIgnore]
        public ICollection<Viagem> Viagens { get; set; } = new Collection<Viagem>();

        public Funcionario()
        {
            TipoDeUsuario = Enums.RolesUsuario.Funcionario;
        }
    }
}
