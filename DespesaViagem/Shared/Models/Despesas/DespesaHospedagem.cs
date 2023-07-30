using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaViagem.Shared.Models.Despesas
{
    public class DespesaHospedagem : Despesa
    {
        public int QuantidadeDias { get; private set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorDiaria { get; private set; }
        public int IdEndereco { get; private set; }
        public Endereco? Endereco { get; private set; }
        public DespesaHospedagem(string descricaoDespesa, Endereco endereco, int quantidadeDias, decimal valorDiaria, int idViagem)
            : base("Despesa com hospedagem", descricaoDespesa, quantidadeDias * valorDiaria, TiposDespesas.Hospedagem, idViagem)
        {
            Endereco = endereco;
            QuantidadeDias = quantidadeDias;
            ValorDiaria = valorDiaria;
        }
        public DespesaHospedagem() {}

        public void CalcularTotalDespesa()
        {
            if (QuantidadeDias >= 0 && ValorDiaria >= 0)
                TotalDespesa = QuantidadeDias * ValorDiaria;
        }
    }
}
