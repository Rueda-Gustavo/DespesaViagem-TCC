using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IDepartamentoService
    {
        Task<Result<IEnumerable<Departamento>>> ObterDepartamentos(/*int idAdmin*/);
        Task<Result<Departamento>> ObterDepartamento(int id);
        Task<Result<Departamento>> ObterDepartamento(string descricao);
        Task<Result<Departamento>> AdicionarDepartamento(string descricao);
        Task<Result<Departamento>> AlterarDepartamento(Departamento departamento);
        Task<Result<Departamento>> AtivarDepartamento(int id);
        Task<Result<Departamento>> DesativarDepartamento(int id);
        Task<Result<Departamento>> RemoverDepartamento(int id);
    }
}
