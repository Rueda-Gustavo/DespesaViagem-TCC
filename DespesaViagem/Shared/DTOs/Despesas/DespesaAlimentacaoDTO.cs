using DespesaViagem.Shared.Models.Core.Enums;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaAlimentacaoDTO
    {
        public int Id { get; set; } = 0;
        public string DescricaoDespesa { get; set; } = string.Empty;
        //public decimal TotalDespesa { get; set; }
        public DateTime DataDespesa { get; set; }
        public DateTime DataDeCadastro { get; } = DateTime.Now;
        [JsonIgnore]
        public TiposDespesas TipoDespesa { get; set; } = TiposDespesas.Alimentação;
        public int IdViagem { get; set; }
        public string NomeEstabelecimento { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public decimal ValorRefeicao { get; set; }
    }
}
