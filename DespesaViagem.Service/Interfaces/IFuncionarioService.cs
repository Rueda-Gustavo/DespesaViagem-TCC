using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task<Result<IEnumerable<Funcionario>>> ObterTodosFuncionarios();
        Task<Result<Funcionario>> ObterFuncionarioPorId(int id);
        Task<Result<Funcionario>> ObterFuncionarioPorCPF(string CPF);
        Task<Result<IEnumerable<Funcionario>>> ObterFuncionarioPorFiltro(string filtro);
        Task<Result<Funcionario>> AdicionarFuncionario(Funcionario funcionario);
        Task<Result<Funcionario>> AlterarFuncionario(Funcionario funcionario);
        Task<Result<Funcionario>> RemoverFuncionario(int id);
    }
}
