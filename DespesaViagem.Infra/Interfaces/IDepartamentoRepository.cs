using DespesaViagem.Shared.Models.Core.Helpers;
using System.ComponentModel;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> ObterDepartamentos();
        Task<Departamento> ObterDepartamento(int id);
        Task<Departamento> ObterDepartamento(string descricao);
        Task Insert(Departamento departamento);
        Task Update(Departamento departamento);
        Task Delete(Departamento departamento);

    }
}
