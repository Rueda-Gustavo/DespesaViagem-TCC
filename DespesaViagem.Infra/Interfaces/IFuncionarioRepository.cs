using DespesaViagem.Shared.Models.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> ObterTodos();
        Task<Funcionario> ObterPorId(int id);
        Task<Funcionario> ObterPorCPF(string CPF);
        Task<IEnumerable<Funcionario>> ObterPorFiltro(string filtro);
        Task Insert(Funcionario funcionario);
        Task Update(Funcionario funcionario);
        Task Delete(Funcionario funcionario);
    }
}
