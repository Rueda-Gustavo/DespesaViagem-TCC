using DespesaViagem.Shared.DTOs.Despesas;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IDespesaService
    {
        Task<List<DespesaDTO>> GetDespesas();
    }
}
