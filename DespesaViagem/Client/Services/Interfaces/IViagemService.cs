using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IViagemService
    {
        event Action ViagensChanged;
        List<ViagemDTO> Viagens { get; set; }
        string Mensagem { get; set; }
        Task GetViagens();
        Task GetViagens(int idFuncionario);
        Task GetViagensPorDepartamento(int idDepartamento);
        Task<ViagemDTO> GetViagem(int idViagem);
        //Task<FuncionarioDTO> GetFuncionario(string CPF);
        //Task<FuncionarioDTO> GetFuncionario(int idFuncionario);
        Task<List<DespesaDTO>> ObterDespesas(int idViagem);
        Task<DespesasPorPagina> ObterDespesasPorPagina(int idViagem, int page);
        Task<DespesasPorPagina> ObterTodasDespesasPaginadasPorTipo(int idViagem, int pagina, string stringTipoDespesa);
        Task<List<DespesaPorCategoria>> ObterTotalDespesasPorCategoria(int idViagem);
        Task<ViagemDTO> ObterViagemAbertaOuEmAndamento();
        Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagem);
        Task<Result<ViagemDTO>> AtualizarViagem(ViagemDTO viagem);
        Task<Result<ViagemDTO>> IniciarViagem();
        Task<Result<ViagemDTO>> CancelarViagem();
        Task<Result<ViagemDTO>> EncerrarViagem();
    }
}
