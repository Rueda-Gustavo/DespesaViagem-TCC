using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IViagemService
    {
        Task<Result<List<ViagemDTO>>> ObterTodasViagens(int idFuncionario);        
        Task<Result<ViagemDTO>> ObterViagemPorId(int id);
        Task<Result<List<ViagemDTO>>> ObterViagemPorFiltro(string filtro);        
        Task<Result<List<DespesaDTO>>> ObterTodasDespesas(int idViagem);
        Task<DespesasPorPagina> ObterDespesasPorPagina(int idViagem, int pagina);
        Task<DespesasPorPagina> ObterTodasDespesasPaginadasPorTipo(int idViagem, int pagina, string stringTipoDespesa);
        Task<List<DespesaPorCategoria>> ObterTotalDasDespesasPorCategoria(int viagemId);
        Task<Result<List<ViagemDTO>>> ObterViagemPorStatus(StatusViagem statusViagem);      
        Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagemDTO, int idFuncionario);
        Task<Result<ViagemDTO>> AlterarViagem(ViagemDTO viagemDTO, int idFuncionario);
        Task<Result<ViagemDTO>> RemoverViagem(int id);
        Task<Result<decimal>> ObterPrestacaoDeContas(int idViagem);
        Task<Result<ViagemDTO>> IniciarViagem(int idFuncionario);
        Task<Result<ViagemDTO>> EncerrarViagem(int idFuncionario);
        Task<Result<ViagemDTO>> CancelarViagem(int idFuncionario);
    }
}
