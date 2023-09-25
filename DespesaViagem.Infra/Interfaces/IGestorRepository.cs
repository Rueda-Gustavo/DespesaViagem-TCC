using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IGestorRepository : IUsuariosRepository<Gestor>
    {
        Task<Gestor> ObterPorCPF(string CPF);
        Task<bool> UsuarioJaExiste(string filtro);
    }
}
