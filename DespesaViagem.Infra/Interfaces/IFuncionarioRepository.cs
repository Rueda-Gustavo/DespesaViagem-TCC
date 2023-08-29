using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IFuncionarioRepository : IUsuariosRepository<Funcionario>
    {
        Task<Funcionario> ObterPorCPF(string CPF);
        Task<bool> FuncionarioJaExiste(string filtro);
    }
}
