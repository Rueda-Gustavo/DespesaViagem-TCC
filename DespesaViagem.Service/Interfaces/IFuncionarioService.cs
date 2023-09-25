using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IFuncionarioService : IUsuariosService<Funcionario>
    {        
        Task<Result<Funcionario>> ObterPorCPF(string CPF);
        Task<Result<Funcionario>> Adicionar(Funcionario funcionario, string password);
        Task<Result<Funcionario>> Alterar(FuncionarioDTO funcionario);
    }
}
