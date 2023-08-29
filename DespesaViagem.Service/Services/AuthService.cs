using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class AuthService : IAuthService
    {
        public Task<Result<int>> Register(Usuario usuario, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UsuarioJaExiste(string username)
        {
            throw new NotImplementedException();
        }
    }
}
