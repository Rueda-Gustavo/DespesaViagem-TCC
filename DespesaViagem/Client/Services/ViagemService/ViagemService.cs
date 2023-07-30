using DespesaViagem.Client.Pages;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
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
        public ViagemDTO Viagem { get; set; } = new ViagemDTO();
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

            ViagensChanged.Invoke();
        }

        public async Task GetViagem(int id)
        {
            var response = await _httpClient
                .GetFromJsonAsync<ViagemDTO>($"api/Viagem/{id}");

            if (response == null)
            {
                Mensagem = "Nenhuma viagem encontrada!";                
            }
            else
            {
                Viagem = response;
            }

            ViagensChanged.Invoke();
        }

        public async Task<ServiceResponse<Funcionario>> GetFuncionario(string CPF)
        {
            Funcionario funcionario = await _httpClient
                .GetFromJsonAsync<Funcionario>($"api/funcionario/{CPF}/obterfuncionarioporfiltro")
                ?? new Funcionario();

            if (funcionario == null || funcionario.CPF != CPF)
                return new ServiceResponse<Funcionario> { Sucesso = false };

            return new ServiceResponse<Funcionario> { Dados = funcionario };
            /*
            _logger.LogError($"Erro ao obter Funcionario pelo CPF = {CPF} - {message}");
            throw new Exception($"Status Code : {response.StatusCode} - {message}");
            */

        }
    }
}
