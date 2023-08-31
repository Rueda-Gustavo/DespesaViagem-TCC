using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> UsuarioJaExiste(string filtro);
        Task<Usuario?> ObterUsuario(string username);
    }
}
