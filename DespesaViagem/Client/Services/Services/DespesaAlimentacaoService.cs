using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
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

            DespesaAlimentacao response = await result.Content.ReadFromJsonAsync<DespesaAlimentacao>() ?? new();

            despesa = MappingDTOs.ConverterDTO(response);

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaAlimentacaoDTO>("Falha para adicionar despesa!");


            Console.WriteLine("Sucesso - DespesaAlimentacaoService - Client");
            DespesasChanged.Invoke();

            return Result.Success(despesa);
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
            try
            {
                var response = await _httpClient
                    .GetFromJsonAsync<ServiceResponse<DespesaAlimentacaoDTO>>($"api/DespesaAlimentacao/{IdDespesa}") ?? new();

                if (response.Conteudo is null || response.Conteudo.Id == 0)
                    return Result.Failure<DespesaAlimentacaoDTO>("Despesa com alimentação não encontrada!");


                Console.WriteLine("Sucesso - DespesaAlimentacaoService - Client");
                DespesasChanged.Invoke();

                return Result.Success(response.Conteudo);

            } catch
            {
                Console.WriteLine("Falha - DespesaAlimentacaoService - Client");
                Mensagem = "Despesa com alimentação não encontrada!";
                return new();
            }
        }
    }
}
