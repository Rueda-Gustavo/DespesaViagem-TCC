using DespesaViagem.Shared.Models.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaViagem.Shared.Models.Despesas
{
    public class DespesaPassagem : Despesa
    {
        public string Companhia { get; private set; } = string.Empty;
        public string Origem { get; private set; } = string.Empty;
        public string Destino { get; private set; } = string.Empty;
        public DateTime DataHoraEmbarque { get; private set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Preco { get; private set; }

        public DespesaPassagem(string descricaoDespesa, string companhia, string origem, 
            string destino, DateTime dataHoraEmbarque, decimal preco, int idViagem) : 
            base("Despesa com passagem", descricaoDespesa, preco, TiposDespesas.Passagem, idViagem)
        {
            Companhia = companhia;
            Origem = origem;
            Destino = destino;
            DataHoraEmbarque = dataHoraEmbarque;
            Preco = preco;
        }

        public DespesaPassagem() { }

        public void CalcularTotalDespesa()
        {
            if (Preco >= 0m)
                TotalDespesa = Preco;
        }
    }
}
