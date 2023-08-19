using DespesaViagem.Shared.Models.Core.Enums;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class DespesaPorCategoria
    {
        public decimal TotalDespesa { get; set; }
        public TiposDespesas TipoDespesa { get; set; }
    }
}
