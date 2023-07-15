using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> ObterTodos();
        Task<Endereco> ObterPorId(int id);
        Task<IEnumerable<Endereco>> ObterPorFiltro(string filtro);
        Task Insert(Endereco endereco);
        Task Update(Endereco endereco);
        Task Delete(Endereco endereco);
    }
}
