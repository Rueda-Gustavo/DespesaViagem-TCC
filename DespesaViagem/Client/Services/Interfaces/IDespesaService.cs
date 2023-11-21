using DespesaViagem.Shared.DTOs.Despesas;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IDespesaService
    {
        Task<List<DespesaDTO>> GetDespesas();
        Task<DespesaDTO> GetDespesa(int idDespesa);
        Task<List<DespesaDTO>> GetDespesasDaViagem(int idViagem);

    }
}
