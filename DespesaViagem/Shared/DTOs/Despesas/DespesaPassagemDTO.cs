using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaPassagemDTO : DespesaDTO
    {
        [Required(ErrorMessage = "Obrigatório!"), StringLength(40, MinimumLength = 2, ErrorMessage = "Obrigatório de 2 a 40 caracteres")]
        public string Companhia { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!"), StringLength(40, MinimumLength = 2, ErrorMessage = "Obrigatório de 2 a 40 caracteres")]
        public string Origem { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!"), StringLength(40, MinimumLength = 2, ErrorMessage = "Obrigatório de 2 a 40 caracteres")]
        public string Destino { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!")]
        public DateTime DataHoraEmbarque { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        public decimal Preco { get; set; }
    }
}
