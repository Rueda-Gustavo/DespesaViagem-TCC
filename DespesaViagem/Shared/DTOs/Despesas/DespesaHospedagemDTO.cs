using DespesaViagem.Shared.Models.Core.Enums;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaHospedagemDTO
    {
        public int Id { get; set; } = 0;
        [JsonIgnore]
        public virtual string NomeDespesa { get; set; } = "Despesa com hospedagem";
        public virtual string DescricaoDespesa { get; set; } = string.Empty;
        public virtual decimal TotalDespesa { get; set; }
        public virtual DateTime DataDespesa { get; set; }
        public DateTime DataDeCadastro { get; } = DateTime.Now;
        [JsonIgnore]
        public TiposDespesas TipoDespesa { get; set; } = TiposDespesas.Hospedagem;
        public int IdViagem { get; set; }
        public int QuantidadeDias { get; set; }
        public decimal ValorDiaria { get; set; }
        public int IdEndereco { get; set; }
    }
}
