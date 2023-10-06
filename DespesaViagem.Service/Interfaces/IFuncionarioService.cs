using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IFuncionarioService : IUsuariosService<FuncionarioDTO>
    {        
        Task<Result<FuncionarioDTO>> ObterPorCPF(string CPF);
        Task<Result<FuncionarioDTO>> Adicionar(Funcionario funcionario, string password);
        Task<Result<FuncionarioDTO>> Alterar(FuncionarioDTO funcionario);
    }
}
