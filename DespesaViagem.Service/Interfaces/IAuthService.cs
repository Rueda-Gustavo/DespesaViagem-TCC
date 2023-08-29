using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<int>> Register(Usuario usuario, string password);
        Task<bool> UsuarioJaExiste(string  username);
    }
}
