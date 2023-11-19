using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaDeslocamentoDTO : DespesaDTO
    {
        [Required(ErrorMessage = "Obrigatório!"), RegularExpression(@"^[1-9]\d+$", ErrorMessage = "Preenchimento incorreto!")]
        public int Quilometragem { get; set; }
        public decimal ValorPorQuilometro { get; set; }
        [Required(ErrorMessage = "Obrigatório!"), StringLength(20, MinimumLength = 5, ErrorMessage = "Obrigatório de 5 a 20 caracteres")]
        public string Placa { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!"), StringLength(20, MinimumLength = 2, ErrorMessage = "Obrigatório de 2 a 20 caracteres")]
        public string Modelo { get; set; } = string.Empty;
    }
}
