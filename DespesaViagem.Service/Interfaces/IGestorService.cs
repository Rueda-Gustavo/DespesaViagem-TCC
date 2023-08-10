using CSharpFunctionalExtensions;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IGestorService : IUsuariosService<Gestor>
    {
        Task<Result<Gestor>> ObterPorCPF(string CPF);
        Task<Result<Gestor>> Adicionar(Gestor gestor);
        Task<Result<Gestor>> Alterar(Gestor gestor);
    }
}
