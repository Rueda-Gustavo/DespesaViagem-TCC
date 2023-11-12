using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Interfaces
{
    public interface IViagemService
    {
        /*Consulta da viagem*/
        Task<Result<List<ViagemDTO>>> ObterTodasViagens(int idUsuario);
        Task<Result<ViagensPorPagina>> ObterViagensPaginadas(int idUsuario, int pagina);
        Task<Result<ViagemDTO>> ObterViagemPorId(int id);
        Task<Result<List<ViagemDTO>>> ObterViagensPorFuncionario(int idFuncionario); //Antigo
        Task<Result<ViagensPorPagina>> ObterViagensPorFuncionario(int idFuncionario, int pagina);
        Task<Result<List<ViagemDTO>>> ObterViagensPorDepartamento(int idDepartamento); //Antigo
        Task<Result<ViagensPorPagina>> ObterViagensPorDepartamento(int idDepartamento, int pagina);
        Task<Result<List<ViagemDTO>>> ObterViagensPorFiltro(string filtro); //Antigo
        Task<Result<ViagensPorPagina>> ObterViagensPorFiltro(string filtro, int pagina);
        Task<Result<List<ViagemDTO>>> ObterViagemPorStatus(StatusViagem statusViagem, int idFuncionario); //Antigo
        Task<Result<ViagensPorPagina>> ObterViagemPorStatus(StatusViagem statusViagem, int idFuncionario, int pagina);
        Task<Result<List<ViagemDTO>>> ObterTodasViagensStatus(StatusViagem statusViagem, int idUsuario);
        Task<Result<ViagemDTO?>> ObterViagemAbertaOuEmAndamento(int idFuncionario);
        
        /*Consulta de despesas relacionadas a viagem*/
        Task<Result<List<DespesaDTO>>> ObterTodasDespesas(int idViagem);
        Task<Result<DespesasPorPagina>> ObterDespesasPorPagina(int idViagem, int pagina);
        Task<Result<DespesasPorPagina>> ObterTodasDespesasPaginadasPorTipo(int idViagem, int pagina, string stringTipoDespesa);
        Task<Result<List<DespesaPorCategoria>>> ObterTotalDasDespesasPorCategoria(int viagemId);                
        Task<Result<decimal>> ObterPrestacaoDeContas(int idViagem);
        
        /*Manipulação das viagens*/
        Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagemDTO, int idFuncionario);
        Task<Result<ViagemDTO>> AlterarViagem(ViagemDTO viagemDTO, int idFuncionario);
        Task<Result<ViagemDTO>> RemoverViagem(int id);
        
        /*Manipulação dos estados da viagem*/
        Task<Result<ViagemDTO>> IniciarViagem(int idFuncionario);
        Task<Result<ViagemDTO>> EncerrarViagem(int idFuncionario);
        Task<Result<ViagemDTO>> CancelarViagem(int idFuncionario);
    }
}
