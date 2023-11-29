using System.ComponentModel.DataAnnotations;

namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesaHospedagemDTO : DespesaDTO
    {
        [Required(ErrorMessage = "Obrigatório!")]
        public int QuantidadeDias { get; set; }
        public decimal ValorDiaria { get; set; }
        [Required(ErrorMessage = "Obrigatório!"), StringLength(1000, MinimumLength = 6, ErrorMessage = "Obrigatório de 6 a 1000 caracteres")]
        public string Logradouro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!")]
        public int NumeroCasa { get; set; }
        [Required(ErrorMessage = "Obrigatório!"), StringLength(20, MinimumLength = 6, ErrorMessage = "Obrigatório de 6 a 20 caracteres")]
        public string CEP { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!"), StringLength(100, MinimumLength = 6, ErrorMessage = "Obrigatório de 6 a 100 caracteres")]
        public string Cidade { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório!"), StringLength(100, MinimumLength = 6, ErrorMessage = "Obrigatório de 6 a 100 caracteres")]
        public string Estado { get; set; } = string.Empty;
        //public int IdEndereco { get; set; }
    }
}
