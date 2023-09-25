using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<ServiceResponse<string>> Login(LoginUsuario usuario);
        Task<ServiceResponse<bool>> TrocarSenha(TrocarSenhaUsuario novaSenhaUsuario);
        Task<ServiceResponse<RolesUsuario>> ObterTipoUsuario();
    }
}
