using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaDeslocamentoService : IDespesasService<DespesaDeslocamentoDTO>
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; } = "Carregando despesa com deslocamento ...";
        public DespesaDeslocamentoDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaDeslocamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<DespesaDeslocamentoDTO>> AdicionarDespesa(DespesaDeslocamentoDTO despesa)
        {
            var result = await _httpClient
                          .PostAsJsonAsync("api/DespesaDeslocamento", despesa);

            DespesaDeslocamentoDTO response = await result.Content.ReadFromJsonAsync<DespesaDeslocamentoDTO>();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaDeslocamentoDTO>("Falha para adicionar despesa!");


            Console.WriteLine("Sucesso - DespesaDeslocamentoService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }

        public async Task<Result<DespesaDeslocamentoDTO>> AtualizarDespesa(DespesaDeslocamentoDTO despesa)
        {
            var result = await _httpClient
                          .PutAsJsonAsync("api/DespesaDeslocamento", despesa);

            DespesaDeslocamentoDTO response = await result.Content.ReadFromJsonAsync<DespesaDeslocamentoDTO>();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaDeslocamentoDTO>("Despesa com deslocamento não encontrada!");


            Console.WriteLine("Sucesso - DespesaDeslocamentoService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }
        public async Task<Result<DespesaDeslocamentoDTO>> GetDespesa(int IdDespesa)
        {
            DespesaDeslocamentoDTO response = await _httpClient
                          .GetFromJsonAsync<DespesaDeslocamentoDTO>($"api/DespesaDeslocamento/{IdDespesa}") ?? new();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaDeslocamentoDTO>("Nenhuma viagem encontrada!");


            Console.WriteLine("Sucesso - DespesaDeslocamentoService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }
    }
}
