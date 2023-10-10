using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IAdminService
    {
        event Action AdminChanged;
        string Mensagem { get; set; }
        Task<List<FuncionarioDTO>> ObterListaDeFuncionarios();
        Task<AdminManutencaoDTO> ObterListaDeUsuarios();
        Task<Result<FuncionarioDTO>> VincularGestor(int idFuncionario, int idGestor);
        Task<Result<FuncionarioDTO>> DesvincularGestor(int idFuncionario);
    }
}
