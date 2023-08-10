using DespesaViagem.Shared.Models.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaViagem.Shared.Models.Despesas
{
    public class DespesaAlimentacao : Despesa
    {
        public string NomeEstabelecimento { get; private set; } = string.Empty;
        public string CNPJ { get; private set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorRefeicao { get; private set; }

        public DespesaAlimentacao(string nomeDespesa, string descricaoDespesa, decimal valorRefeicao, string nomeEstabelecimento, string CNPJ, int idViagem) : 
            base(nomeDespesa, descricaoDespesa, valorRefeicao, TiposDespesas.Alimentação, idViagem)
        {
            NomeEstabelecimento = nomeEstabelecimento;
            this.CNPJ = CNPJ;
            ValorRefeicao = valorRefeicao;
        }

        public DespesaAlimentacao() { }

        public void CalcularTotalDespesa()
        {
            if (ValorRefeicao >= 0m)
                TotalDespesa = ValorRefeicao;
        }
    }
}