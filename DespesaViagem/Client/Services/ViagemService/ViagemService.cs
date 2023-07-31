using DespesaViagem.Client.Pages;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.ViagemService
{
    public class ViagemService : IViagemService
    {
        public event Action ViagensChanged;

        private readonly HttpClient _httpClient;
        private readonly ILogger<ViagemService> _logger;

        public List<ViagemDTO> Viagens { get; set; } = new List<ViagemDTO>();
        public string Mensagem { get; set; } = "Carregando viagens...";

        public ViagemService(HttpClient httpClient, ILogger<ViagemService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task GetViagens()
        {
            var response = await _httpClient
               .GetFromJsonAsync<List<ViagemDTO>>("api/Viagem");

            if (response == null || !response.Any())
                Mensagem = "Nenhuma viagem encontrada!";
            else
            {
                Viagens = response;
            }
            _logger.LogInformation("Sucesso.");
            ViagensChanged.Invoke();
        }

        public async Task<ViagemDTO> GetViagem(int idViagem)
        {
            var response = await _httpClient
                .GetFromJsonAsync<ViagemDTO>($"api/Viagem/{idViagem}");

            if (response == null)
            {
                Mensagem = "Nenhuma viagem encontrada!";
                return new ViagemDTO();
            }
            else
            {
                _logger.LogInformation("Sucesso.");
                return response;
            }           
        }

        public async Task<Funcionario> GetFuncionario(string CPF)
        {
            Funcionario funcionario = await _httpClient
                .GetFromJsonAsync<Funcionario>($"api/funcionario/{CPF}/obterfuncionarioporfiltro")
                ?? new Funcionario();

            if (funcionario.CPF != CPF)
            {
                _logger.LogError("Funcionário não encontrado.");
                return funcionario;
            }
            
            _logger.LogInformation("Sucesso.");
            return funcionario;                                                
        }

        public async Task<List<DespesaDTO>> ObterDespesas(int idViagem)
        {
            List<DespesaDTO> despesas = await _httpClient
                .GetFromJsonAsync<List<DespesaDTO>>($"api/Viagem/ObterDespesas/{idViagem}")
                ?? new List<DespesaDTO>();

            if (!despesas.Any())
            {
                _logger.LogError("Despesas não encontradas.");
                return despesas;
            }

            _logger.LogInformation("Sucesso.");
            return despesas;
        }
    }
}
