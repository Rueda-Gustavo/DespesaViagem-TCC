using DespesaViagem.Shared.Models.Core.Enums;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaDTO
    {
        public int Id { get; set; }        
        public virtual string NomeDespesa { get; set; } = string.Empty;        
        public virtual string DescricaoDespesa { get; set; } = string.Empty;        
        public virtual decimal TotalDespesa { get; set; }
        public virtual DateTime DataDespesa { get; set; }
        public DateTime DataDeCadastro { get; set; }
        //[JsonIgnore]
        public TiposDespesas TipoDespesa { get; set; }
        public int IdViagem { get; set; }
    }
}
