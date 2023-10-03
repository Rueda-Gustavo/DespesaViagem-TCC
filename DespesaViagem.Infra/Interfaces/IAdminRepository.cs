using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IAdminRepository
    {
        Task<AdminManutencao> ObterListaUsuarios();
    }
}
