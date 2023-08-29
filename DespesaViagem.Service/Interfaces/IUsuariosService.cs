using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespesaViagem.Services.Interfaces
{
    public interface IUsuariosService<T> where T : class
    {
        Task<Result<IEnumerable<T>>> ObterTodos();
        Task<Result<T>> ObterPorId(int id);
        Task<Result<IEnumerable<T>>> ObterPorFiltro(string filtro);
        Task<Result<T>> Remover(int id);
    }
}
