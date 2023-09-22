using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaHospedagemDTO : DespesaDTO
    {
        //public int Id { get; set; } = 0;
        //public string DescricaoDespesa { get; set; } = string.Empty;
        //public decimal TotalDespesa { get; set; }
        //public DateTime DataDespesa { get; set; }
        //public DateTime DataDeCadastro { get; } = DateTime.Now;
        //[JsonIgnore]
        //public TiposDespesas TipoDespesa { get; set; } = TiposDespesas.Hospedagem;
        //public int IdViagem { get; set; }
        public int QuantidadeDias { get; set; }
        public decimal ValorDiaria { get; set; }
        //public Endereco Endereco { get; set; } = new();
        public string Logradouro { get; set; } = string.Empty;
        public int NumeroCasa { get; set; }        
        public string CEP { get; set; } = string.Empty;        
        public string Cidade { get; set; } = string.Empty;        
        public string Estado { get; set; } = string.Empty;
        public int IdEndereco { get; set; }
    }
}
