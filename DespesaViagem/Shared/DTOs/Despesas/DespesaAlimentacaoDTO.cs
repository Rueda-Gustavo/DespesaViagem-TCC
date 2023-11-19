using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaAlimentacaoDTO : DespesaDTO
    {
        [Required(ErrorMessage = "Obrigatório!"), StringLength(30, MinimumLength = 2, ErrorMessage = "Obrigatório de 2 a 30 caracteres")]
        public string NomeEstabelecimento { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!"), StringLength(20, MinimumLength = 6, ErrorMessage = "Obrigatório de 6 a 20 caracteres")]
        public string CNPJ { get; set; } = string.Empty;
        public decimal ValorRefeicao { get; set; }
    }
}
