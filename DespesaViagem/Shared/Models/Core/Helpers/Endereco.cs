using DespesaViagem.Shared.Models.Despesas;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class Endereco
    {
        [ForeignKey("DespesaHospedagem")]
        public int Id { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string Logradouro { get; set; } = string.Empty;
        public int NumeroCasa { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string CEP { get; set; } = string.Empty;
        [Column(TypeName = "varchar(100)")]
        public string Cidade { get; set; } = string.Empty;
        [Column(TypeName = "varchar(100)")]
        public string Estado { get; set; } = string.Empty;
        [JsonIgnore]
        //public ICollection<DespesaHospedagem> DespesasHospedagem { get; set; } = new Collection<DespesaHospedagem>();
        public DespesaHospedagem DespesaHospedagem { get; set; } = new();
    }
}
