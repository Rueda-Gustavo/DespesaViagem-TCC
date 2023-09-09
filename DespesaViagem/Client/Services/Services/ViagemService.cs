using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Viagens;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class ViagemService : IViagemService
    {
        public event Action ViagensChanged;

        private readonly HttpClient _httpClient;
        //private readonly ILogger<ViagemService> _logger;

        public List<ViagemDTO> Viagens { get; set; } = new List<ViagemDTO>();

        public string Mensagem { get; set; } = "Carregando viagens...";

        public ViagemService(HttpClient httpClient/*, ILogger<ViagemService> logger*/)
        {
            _httpClient = httpClient;
            //_logger = logger;
        }

        public async Task GetViagens()
        {
            var response = await _httpClient
               .GetFromJsonAsync<List<ViagemDTO>>("api/Viagem");

            if (response is null || !response.Any())
                Mensagem = "Nenhuma viagem encontrada!";
            else
            {
                Viagens = response;
                Console.WriteLine("Sucesso - ViagemService - Client");
                ViagensChanged.Invoke();
            }
        }

        public async Task GetViagens(int idFuncionario)
        {
            var response = await _httpClient
                .GetFromJsonAsync<List<ViagemDTO>>($"api/Viagem");

            if (response is null || !response.Any())            
                Mensagem = "Nenhuma viagem encontrada!";                            
            else
            {                
                Viagens = response
                    .Where(v => v.IdFuncionario == idFuncionario).ToList();

                Console.WriteLine("Sucesso - ViagemService - Client");
                ViagensChanged.Invoke();
            }
        }

        public async Task<ViagemDTO> GetViagem(int idViagem)
        {
            var response = await _httpClient
                .GetFromJsonAsync<ViagemDTO>($"api/Viagem/{idViagem}");

            if (response is null)
            {
                Mensagem = "Nenhuma viagem encontrada!";
                return new ViagemDTO();
            }
            else
            {
                Console.WriteLine("Sucesso - ViagemService - Client");
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
                Console.WriteLine("Funcionário não encontrado.");
                return funcionario;
            }

            Console.WriteLine("Sucesso - ViagemService - Client");
            return funcionario;
        }

        public async Task<Funcionario> GetFuncionario(int idFuncionario)
        {
            Funcionario funcionario = await _httpClient
                .GetFromJsonAsync<Funcionario>($"api/funcionario/{idFuncionario}")
                ?? new Funcionario();

            if (funcionario.Id == 0 || funcionario.CPF == string.Empty)
            {
                Console.WriteLine("Funcionário não encontrado.");
                return funcionario;
            }

            Console.WriteLine("Sucesso - ViagemService - Client");
            return funcionario;
        }

        public async Task<List<DespesaDTO>> ObterDespesas(int idViagem)
        {
            List<DespesaDTO> despesas = await _httpClient
                .GetFromJsonAsync<List<DespesaDTO>>($"api/Viagem/ObterDespesas/{idViagem}")
                ?? new List<DespesaDTO>();

            if (!despesas.Any())
            {
                Console.WriteLine("Despesas não encontradas.");
                return despesas;
            }

            Console.WriteLine("Sucesso - ViagemService - Client");
            return despesas;
        }

        public async Task<DespesasPorPagina> ObterDespesasPorPagina(int idViagem, int pagina)
        {
            DespesasPorPagina despesas = await _httpClient
                .GetFromJsonAsync<DespesasPorPagina>($"api/Viagem/ObterDespesasPorPagina/{idViagem}/{pagina}")
                ?? new DespesasPorPagina();

            if (despesas is null)
            {
                Console.WriteLine("Despesas não encontradas.");
                return despesas;
            }

            Console.WriteLine("Sucesso - ViagemService - Client");
            ViagensChanged.Invoke();

            return despesas;
        }

        public async Task<DespesasPorPagina> ObterTodasDespesasPaginadasPorTipo(int idViagem, int pagina, string tipoDespesa)
        {
            DespesasPorPagina despesas = await _httpClient
                .GetFromJsonAsync<DespesasPorPagina>($"api/Viagem/ObterTodasDespesasPaginadasPorTipo/{idViagem}/{pagina}/{tipoDespesa}")
                ?? new DespesasPorPagina();

            if (despesas is null)
            {
                Console.WriteLine("Despesas não encontradas.");
                return despesas;
            }

            Console.WriteLine("Sucesso - ViagemService - Client");
            ViagensChanged.Invoke();

            return despesas;
        }


        public async Task<List<DespesaPorCategoria>> ObterTotalDespesasPorCategoria(int idViagem)
        {
            List<DespesaPorCategoria> despesas = await _httpClient
                .GetFromJsonAsync<List<DespesaPorCategoria>>($"api/Viagem/ObterDespesasPorCategoria/{idViagem}")
                ?? new List<DespesaPorCategoria>();

            if (!despesas.Any())
            {
                Console.WriteLine("Despesas não encontradas.");
                return despesas;
            }

            Console.WriteLine("Sucesso - ViagemService - Client");
            return despesas;
        }
    }
}


