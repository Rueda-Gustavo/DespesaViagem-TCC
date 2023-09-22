using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaPassagemService : IDespesasService<DespesaPassagemDTO>
    {
        private readonly HttpClient _httpClient;
        //public string Mensagem { get; set; } = "Carregando despesa com passagem ...";
        public DespesaPassagemDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaPassagemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<DespesaPassagemDTO>> AdicionarDespesa(DespesaPassagemDTO despesa)
        {
            var result = await _httpClient
                          .PostAsJsonAsync("api/DespesaPassagem", despesa);

            DespesaPassagemDTO response = await result.Content.ReadFromJsonAsync<DespesaPassagemDTO>() ?? new();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaPassagemDTO>("Falha para adicionar despesa!");


            Console.WriteLine("Sucesso - DespesaPassagemService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }

        public async Task<Result<DespesaPassagemDTO>> AtualizarDespesa(DespesaPassagemDTO despesa)
        {
            var result = await _httpClient
                          .PutAsJsonAsync("api/DespesaPassagem", despesa);

            DespesaPassagemDTO response = await result.Content.ReadFromJsonAsync<DespesaPassagemDTO>() ?? new();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaPassagemDTO>("Despesa com passagem não encontrada!");


            Console.WriteLine("Sucesso - DespesaPassagemService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }

        public async Task<Result<DespesaPassagemDTO>> GetDespesa(int IdDespesa)
        {
            try
            {
                var response = await _httpClient
              .GetFromJsonAsync<ServiceResponse<DespesaPassagemDTO>>($"api/DespesaPassagem/{IdDespesa}") ?? new();

                if (response.Conteudo is null || response.Conteudo.Id == 0)
                    return Result.Failure<DespesaPassagemDTO>("Despesa com passagem não encontrada!");


                Console.WriteLine("Sucesso - DespesaPassagemService - Client");
                DespesasChanged.Invoke();

                return Result.Success(response.Conteudo);
            }
            catch
            {
                Console.WriteLine("Falha - DespesaPassagemService - Client");
                return new();
            }

        }
    }
}
