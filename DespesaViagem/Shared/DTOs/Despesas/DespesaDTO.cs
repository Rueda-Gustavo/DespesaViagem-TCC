using DespesaViagem.Shared.Models.Core.Enums;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaDTO
    {
        public int Id { get; set; }        
        public string NomeDespesa { get; set; } = string.Empty;        
        public string DescricaoDespesa { get; set; } = string.Empty;        
        public decimal TotalDespesa { get; set; }
        public DateTime DataDespesa { get; set; } = DateTime.Now;
        public DateTime DataDeCadastro { get; set; } = DateTime.Now;
        //[JsonIgnore]
        public TiposDespesas TipoDespesa { get; set; }
        public int IdViagem { get; set; }
    }
}
