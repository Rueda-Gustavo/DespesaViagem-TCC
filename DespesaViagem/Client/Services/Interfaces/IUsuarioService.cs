using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<ServiceResponse<string>> Login(LoginUsuario usuario);
    }
}
