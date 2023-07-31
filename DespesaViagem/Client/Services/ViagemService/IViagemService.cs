using DespesaViagem.Shared.DTOs.Despesas;
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
        Task<ViagemDTO> GetViagem(int idViagem);
        Task<Funcionario> GetFuncionario(string CPF);
        Task<List<DespesaDTO>> ObterDespesas(int idViagem);
    }
}
