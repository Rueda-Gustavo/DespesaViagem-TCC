using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaAlimentacaoService : IDespesasService<DespesaAlimentacaoDTO>
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; } = "Carregando despesa com alimentação ...";
        public DespesaAlimentacaoDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaAlimentacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<DespesaAlimentacaoDTO>> AdicionarDespesa(DespesaAlimentacaoDTO despesa)
        {
            var result = await _httpClient
                          .PostAsJsonAsync("api/DespesaAlimentacao", despesa);

            DespesaAlimentacaoDTO response = await result.Content.ReadFromJsonAsync<DespesaAlimentacaoDTO>();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaAlimentacaoDTO>("Falha para adicionar despesa!");


            Console.WriteLine("Sucesso - DespesaAlimentacaoService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }

        public async Task<Result<DespesaAlimentacaoDTO>> AtualizarDespesa(DespesaAlimentacaoDTO despesa)
        {
            var result = await _httpClient
                          .PutAsJsonAsync("api/DespesaAlimentacao", despesa);

            DespesaAlimentacaoDTO response = await result.Content.ReadFromJsonAsync<DespesaAlimentacaoDTO>();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaAlimentacaoDTO>("Despesa com alimentação não encontrada!");


            Console.WriteLine("Sucesso - DespesaAlimentacaoService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }

        public async Task<Result<DespesaAlimentacaoDTO>> GetDespesa(int IdDespesa)
        {
            DespesaAlimentacaoDTO response = await _httpClient
                          .GetFromJsonAsync<DespesaAlimentacaoDTO>($"api/DespesaAlimentacao/{IdDespesa}") ?? new();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaAlimentacaoDTO>("Nenhuma viagem encontrada!");


            Console.WriteLine("Sucesso - DespesaAlimentacaoService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }
    }
}
