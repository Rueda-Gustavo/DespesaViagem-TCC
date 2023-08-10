using DespesaViagem.Shared.Models.Core.Enums;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaPassagemDTO : DespesaDTO
    {        
        public string Companhia { get; set; } = string.Empty;
        public string Origem { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public DateTime DataHoraEmbarque { get; set; }
        public decimal Preco { get; set; }
    }
}
