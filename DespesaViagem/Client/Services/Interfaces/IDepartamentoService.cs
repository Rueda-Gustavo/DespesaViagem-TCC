using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IDepartamentoService
    {
        event Action DepartamentosChanged;
        Task<Result<IEnumerable<Departamento>>> ObterDepartamentos();
        Task<Result<Departamento>> ObterDepartamento(int idDepartamento);
        Task<Result<Departamento>> ObterDepartamento(string descricao);
        Task<Result<Departamento>> VincularDepartamento(int idFuncionario, int idDepartamento);
        Task<Result<Departamento>> AtualizarDepartamento(Departamento departamento);
        Task<Result<Departamento>> AdicionarDepartamento(string descricao);
        Task<Result<Departamento>> DesativarDepartamento(int idDepartamento);
        Task<Result<Departamento>> AtivarDepartamento(int idDepartamento);
    }
}
