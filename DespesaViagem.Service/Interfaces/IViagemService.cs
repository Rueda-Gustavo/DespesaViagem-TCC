using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IViagemService
    {
        Task<Result<List<ViagemDTO>>> ObterTodasViagens();
        Task<Result<ViagemDTO>> ObterViagemPorId(int id);
        Task<Result<List<ViagemDTO>>> ObterViagemPorFiltro(string filtro);        
        Task<Result<List<DespesaDTO>>> ObterTodasDespesas(int id);
        Task<List<DespesaPorCategoria>> ObterTotalDasDespesasPorCategoria(int viagemId);
        Task<Result<List<ViagemDTO>>> ObterViagemPorStatus(StatusViagem statusViagem);      
        Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagemDTO);
        Task<Result<ViagemDTO>> AlterarViagem(ViagemDTO viagemDTO);
        Task<Result<ViagemDTO>> RemoverViagem(int id);
        Task<Result<decimal>> ObterPrestacaoDeContas(int idViagem);
        Task<Result<ViagemDTO>> IniciarViagem();
        Task<Result<ViagemDTO>> EncerrarViagem();
        Task<Result<ViagemDTO>> CancelarViagem();
    }
}
