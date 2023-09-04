using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IUsuarioService
    {                
        Task<Result<string>> Login (string username, string password);
        Task<Result<bool>> TrocarSenha(int idUsuario, string newPassword);        
    }
}
