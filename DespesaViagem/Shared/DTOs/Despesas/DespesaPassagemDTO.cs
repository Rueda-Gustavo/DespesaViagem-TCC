using DespesaViagem.Shared.Models.Core.Enums;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaPassagemDTO
    {
        public int Id { get; set; } = 0;        
        public string DescricaoDespesa { get; set; } = string.Empty;
        //public decimal TotalDespesa { get; set; }
        public DateTime DataDespesa { get; set; }
        public DateTime DataDeCadastro { get; } = DateTime.Now;
        [JsonIgnore]
        public TiposDespesas TipoDespesa { get; set; } = TiposDespesas.Passagem;
        public int IdViagem { get; set; }
        public string Companhia { get; set; } = string.Empty;
        public string Origem { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public DateTime DataHoraEmbarque { get; set; }
        public decimal Preco { get; set; }
    }
}
