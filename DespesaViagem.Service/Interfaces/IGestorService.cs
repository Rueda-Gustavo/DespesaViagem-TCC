using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IGestorService : IUsuariosService<GestorDTO>
    {
        Task<Result<GestorDTO>> ObterPorCPF(string CPF);
        Task<Result<GestorDTO>> Adicionar(Gestor gestor, string password);
        Task<Result<GestorDTO>> Alterar(GestorDTO gestor);
        Task<Result<IEnumerable<FuncionarioDTO>>> ObterListaFuncionarios(int gestorId);
    }
}
