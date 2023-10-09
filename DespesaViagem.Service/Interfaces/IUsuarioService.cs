using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Result<UsuarioDTO>> ObterUsuario(int idUsuario);
        Task<Result<string>> Login (string username, string password);
        Task<Result<bool>> TrocarSenha(int idUsuario, string newPassword);   
        Task<Result<RolesUsuario>> ObterTipoUsuario(int idUsuario);
    }
}
