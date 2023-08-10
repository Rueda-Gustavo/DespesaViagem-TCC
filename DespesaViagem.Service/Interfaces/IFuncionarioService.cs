using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IFuncionarioService : IUsuariosService<Funcionario>
    {        
        Task<Result<Funcionario>> ObterPorCPF(string CPF);        
        Task<Result<Funcionario>> Adicionar(Funcionario funcionario);
        Task<Result<Funcionario>> Alterar(Funcionario funcionario);
    }
}
