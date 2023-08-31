using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task<ServiceResponse<int>> Cadastrar(CadastroUsuario request);
    }
}
