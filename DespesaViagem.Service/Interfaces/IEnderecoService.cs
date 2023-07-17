using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<Result<IEnumerable<Endereco>>> ObterTodosEnderecos();
        Task<Result<Endereco>> ObterEnderecoPorId(int idEndereco);
        Task<Result<IEnumerable<Endereco>>> ObterEnderecoPorFiltro(string filtro);
        Task<Result<Endereco>> ObterEnderecoPorFiltro(Endereco endereco);
        Task<Result<Endereco>> AdicionarEndereco(Endereco endereco);
        Task<Result<Endereco>> AlterarEndereco(Endereco endereco);
        Task<Result<Endereco>> RemoverEndereco(int id);
    }
}