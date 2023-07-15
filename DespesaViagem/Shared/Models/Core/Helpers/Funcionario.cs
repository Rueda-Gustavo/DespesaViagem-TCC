using DespesaViagem.Shared.Models.Viagens;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public record Funcionario
    {
        [ForeignKey("Viagem")]
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Nome { get; set; } = string.Empty;
        [Column(TypeName = "varchar(1000)")]
        public string Sobrenome { get; set; } = string.Empty;
        [Column(TypeName = "varchar(15)")]
        public string CPF { get; set; } = string.Empty;
        [Column(TypeName = "varchar(30)")]
        public string Matricula { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Viagem> Viagens { get; set; } = new Collection<Viagem>();
    }
}
