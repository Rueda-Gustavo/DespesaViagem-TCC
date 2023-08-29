using DespesaViagem.Shared.DTOs.Despesas;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class DespesasPorPagina
    {
        public List<DespesaDTO> Despesas { get; set; } = new();
        public int PaginaAtual { get; set; }
        public int QuantidadeDePaginas { get; set; }
    }
}
