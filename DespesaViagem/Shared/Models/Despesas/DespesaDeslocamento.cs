using DespesaViagem.Shared.Models.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaViagem.Shared.Models.Despesas
{
    public class DespesaDeslocamento : Despesa
    {
        public long Quilometragem { get; private set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorPorQuilometro { get; private set; }
        public string Placa { get; private set; } = string.Empty;
        public string Modelo { get; private set; } = string.Empty;
        public DespesaDeslocamento(string nomeDespesa, string descricaoDespesa, int quilometragem, decimal valorPorQuilometro, string modelo, string placa, int idViagem)
            : base(nomeDespesa, descricaoDespesa, quilometragem * valorPorQuilometro, TiposDespesas.Deslocamento, idViagem)
        {
            Quilometragem = quilometragem;
            ValorPorQuilometro = valorPorQuilometro;
            Modelo = modelo;
            Placa = placa;
        }

        public DespesaDeslocamento() { }

        public void CalcularTotalDespesa()
        {
            if (Quilometragem >= 0 && ValorPorQuilometro >= 0m)
                TotalDespesa = Quilometragem * ValorPorQuilometro;
        }
    }
}
