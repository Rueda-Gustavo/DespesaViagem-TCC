using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Result<AdminManutencaoDTO>> ObterListaUsuarios(int idAdmin);
    }
}
