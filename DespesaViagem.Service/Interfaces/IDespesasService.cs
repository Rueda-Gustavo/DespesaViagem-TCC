using CSharpFunctionalExtensions;

namespace DespesaViagem.Services.Interfaces
{
    public interface IDespesasService<T> where T : class
    {
        Task<Result<IEnumerable<T>>> ObterTodasDespesas(int idUsuario);
        Task<Result<IEnumerable<T>>> ObterTodasDespesasViagem(int idViagem);
        Task<Result<IEnumerable<T>>> ObterDespesasPorFiltro(string filtro, string idViagem);
        Task<Result<T>> ObterDespesaPorId(string id);
        Task<Result<T>> AdicionarDespesa(T despesa);
        Task<Result<T>> AlterarDespesa(T despesa);
        Task<Result<T>> RemoverViagem(int id);
    }
}
