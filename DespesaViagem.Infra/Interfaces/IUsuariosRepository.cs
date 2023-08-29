using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IUsuariosRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodos();
        Task<T> ObterPorId(int id);
        Task<IEnumerable<T>> ObterPorFiltro(string filtro);
        Task<bool> UsuarioJaExiste(string filtro);
        Task Insert(T usuario);
        Task Update(T usuario);
        Task Delete(T usuario);
    }
}
