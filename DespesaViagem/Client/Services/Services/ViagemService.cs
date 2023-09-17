using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class ViagemService : IViagemService
    {
        public event Action ViagensChanged;

        private readonly HttpClient _http;
        //private readonly ILogger<ViagemService> _logger;

        public List<ViagemDTO> Viagens { get; set; } = new List<ViagemDTO>();

        public string Mensagem { get; set; } = "Carregando viagens...";
        
        public ViagemService(HttpClient http/*, ILogger<ViagemService> logger*/)
        {
            _http = http;
            //_logger = logger;
        }

        public async Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagem)
        {
            var result = await _http.PostAsJsonAsync("api/viagem", viagem);

            ViagemDTO response = await result.Content.ReadFromJsonAsync<ViagemDTO>();

            if (response is null || response.Id == 0)
                return Result.Failure<ViagemDTO>("Erro para adicionar a viagem.");

            Console.WriteLine("Sucesso - ViagemService - Client");
            ViagensChanged.Invoke();
            return Result.Success(response); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<Result<ViagemDTO>> AtualizarViagem(ViagemDTO viagem)
        {
            var result = await _http.PutAsJsonAsync("api/viagem", viagem);

            ViagemDTO response = await result.Content.ReadFromJsonAsync<ViagemDTO>();

            if(response is null || response.Id == 0)
                return Result.Failure<ViagemDTO>("Erro para editar a viagem.");

            Console.WriteLine("Sucesso - ViagemService - Client");
            ViagensChanged.Invoke();
            return Result.Success(response); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task GetViagens()
        {
            var response = await _http
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
            var response = await _http
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
            var response = await _http
                .GetFromJsonAsync<ViagemDTO>($"api/Viagem/{idViagem}");

            if (response is null)
            {
                Mensagem = "Nenhuma viagem encontrada!";
                return new ViagemDTO();
            }
            else
            {
                Console.WriteLine("Sucesso - ViagemService - Client");
                ViagensChanged.Invoke();
                return response;
            }
        }

        public async Task<FuncionarioDTO> GetFuncionario(string CPF)
        {
            Funcionario funcionario = await _http
                .GetFromJsonAsync<Funcionario>($"api/funcionario/{CPF}/obterfuncionarioporfiltro")
                ?? new Funcionario();

            if (funcionario.CPF != CPF)
            {
                Console.WriteLine("Funcionário não encontrado.");
                return new();
            }

            FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(funcionario);

            Console.WriteLine("Sucesso - ViagemService - Client");
            return funcionarioDTO;
        }

        public async Task<FuncionarioDTO> GetFuncionario(int idFuncionario)
        {
            Funcionario funcionario = await _http
                .GetFromJsonAsync<Funcionario>($"api/funcionario/{idFuncionario}")
                ?? new Funcionario();

            if (funcionario.Id == 0 || funcionario.CPF == string.Empty)
            {
                Console.WriteLine("Funcionário não encontrado.");
                return new();
            }

            FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(funcionario);

            Console.WriteLine("Sucesso - ViagemService - Client");
            return funcionarioDTO;
        }

        public async Task<List<DespesaDTO>> ObterDespesas(int idViagem)
        {
            List<DespesaDTO> despesas = await _http
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
            DespesasPorPagina despesas = await _http
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
            DespesasPorPagina despesas = await _http
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
            List<DespesaPorCategoria> despesas = await _http
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


