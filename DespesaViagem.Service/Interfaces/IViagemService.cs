using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Interfaces
{
    public interface IViagemService
    {
        Task<Result<List<ViagemDTO>>> ObterTodasViagens();
        Task<Result<ViagemDTO>> ObterViagemPorId(int id);
        Task<Result<List<ViagemDTO>>> ObterViagemPorFiltro(string filtro);        
        Task<Result<List<Despesa>>> ObterTodasDespesas(int id);
        Task<Result<List<ViagemDTO>>> ObterViagemPorStatus(StatusViagem statusViagem);      
        Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagemDTO);
        Task<Result<ViagemDTO>> AlterarViagem(ViagemDTO viagemDTO);
        Task<Result<ViagemDTO>> RemoverViagem(int id);
        Result<decimal> ObterPrestacaoDeContas(Viagem viagem);
        Task<Result<ViagemDTO>> IniciarViagem();
        Task<Result<ViagemDTO>> EncerrarViagem();
        Task<Result<ViagemDTO>> CancelarViagem();
    }
}
