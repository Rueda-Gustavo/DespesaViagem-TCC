using DespesaViagem.Shared.Models.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório!"), StringLength(1000, MinimumLength = 3, ErrorMessage = "Tamanho máximo atingido! (1000 caracteres)")]
        public string NomeDespesa { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!")]
        public string DescricaoDespesa { get; set; } = string.Empty;        
        public decimal TotalDespesa { get; set; }
        [Required(ErrorMessage = "Obrigatório!")]
        public DateTime DataDespesa { get; set; } = DateTime.Now;
        public DateTime DataDeCadastro { get; set; } = DateTime.Now;
        //[JsonIgnore]
        public TiposDespesas TipoDespesa { get; set; }
        public int IdViagem { get; set; }
    }
}
