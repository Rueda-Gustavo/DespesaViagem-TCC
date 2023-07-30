using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IViagemService
    {
        event Action ViagensChanged;
        List<ViagemDTO> Viagens { get; set; }
        ViagemDTO Viagem { get; set; }
        string Mensagem { get; set; }
        Task GetViagens();
        Task GetViagem(int id);
        Task<ServiceResponse<Funcionario>> GetFuncionario(string CPF);
    }
}
