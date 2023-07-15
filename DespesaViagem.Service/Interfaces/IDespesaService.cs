using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Despesas;

namespace DespesaViagem.Services.Interfaces
{
    public interface IDespesaService
    {
        Task<Result<IEnumerable<Despesa>>> ObterTodasDespesas(int idViagem);
        Task<Result<IEnumerable<Despesa>>> ObterDespesasPorFiltro(string filtro, int idViagem);
        Task<Result<Despesa>> ObterDespesaPorId(string id);
    }
}
