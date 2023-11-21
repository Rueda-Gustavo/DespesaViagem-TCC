using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Despesas;

namespace DespesaViagem.Services.Interfaces
{
    public interface IDespesaService
    {
        Task<Result<IEnumerable<DespesaDTO>>> ObterTodasDespesas(int idUsuario);
        Task<Result<IEnumerable<DespesaDTO>>> ObterTodasDespesasDaViagem(int idViagem);
        Task<Result<IEnumerable<DespesaDTO>>> ObterDespesasPorFiltro(string filtro, int idViagem);
        Task<Result<DespesaDTO>> ObterDespesaPorId(int idDespesa);
    }
}
