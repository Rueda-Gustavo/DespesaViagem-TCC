using DespesaViagem.Shared.Models.Core.Enums;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaDeslocamentoDTO : DespesaDTO
    {
        //public int Id { get; set; } = 0;
        //public string DescricaoDespesa { get; set; } = string.Empty;
        //public decimal TotalDespesa { get; set; }
        //public DateTime DataDespesa { get; set; }
        //public DateTime DataDeCadastro { get; } = DateTime.Now;
        //[JsonIgnore]
        //public TiposDespesas TipoDespesa { get; set; } = TiposDespesas.Deslocamento;
        //public int IdViagem { get; set; }
        public int Quilometragem { get; set; }
        public decimal ValorPorQuilometro { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
    }
}
