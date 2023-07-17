using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Interfaces
{
    public interface IViagemService
    {
        Task<Result<IEnumerable<Viagem>>> ObterTodasViagens();
        Task<Result<Viagem>> ObterViagemPorId(int id);
        Task<Result<IEnumerable<Viagem>>> ObterViagemPorFiltro(string filtro);        
        Task<Result<IEnumerable<Despesa>>> ObterTodasDespesas(int id);
        Task<Result<IEnumerable<Viagem>>> ObterViagemPorStatus(StatusViagem statusViagem);      
        Task<Result<Viagem>> AdicionarViagem(ViagemDTO viagemDTO);
        Task<Result<Viagem>> AlterarViagem(Viagem viagem);
        Task<Result<Viagem>> RemoverViagem(int id);
        Task<Result<decimal>> ObterPrestacaoDeContas(Viagem viagem);
        Task<Result<Viagem>> IniciarViagem();
        Task<Result<Viagem>> EncerrarViagem();
        Task<Result<Viagem>> CancelarViagem();
    }
}
