using DespesaViagem.Shared.Models.Despesas;

namespace DespesaViagem.Infra.Interfaces
{
    //Essa interface diz respeito a classe abstrata. A interface IDespesasRepository é relacionada as 
    //classes que herdam da classe abstrata.
    public interface IDespesaRepository
    {
        Task<IEnumerable<Despesa>> ObterTodos(List<int> idsViagens);
        Task<IEnumerable<Despesa>> ObterTodos(int idViagem);
        Task<Despesa> ObterPorId(int id);
        Task<IEnumerable<Despesa>> ObterPorFiltro(string filtro, int idViagem);
    }
}
