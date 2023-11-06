using DespesaViagem.Shared.DTOs.Viagens;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class ViagensPorPagina
    {
        public List<ViagemDTO> Viagens { get; set; } = new();
        public int PaginaAtual { get; set; }
        public int QuantidadeDePaginas { get; set; }
    }
}
