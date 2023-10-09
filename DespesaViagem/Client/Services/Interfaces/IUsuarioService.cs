using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> GetFuncionario(int idUsuario);
        Task<ServiceResponse<string>> Login(LoginUsuario usuario);
        Task<ServiceResponse<bool>> TrocarSenha(TrocarSenhaUsuario novaSenhaUsuario);
        Task<ServiceResponse<RolesUsuario>> ObterTipoUsuario();
    }
}
