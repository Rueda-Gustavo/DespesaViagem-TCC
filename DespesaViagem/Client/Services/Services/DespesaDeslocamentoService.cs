using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
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
            try
            {
                var result = await _httpClient
                              .PostAsJsonAsync("api/DespesaDeslocamento", despesa);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<DespesaDeslocamento>>() ?? new();

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<DespesaDeslocamentoDTO>("Falha para adicionar despesa!");


                Console.WriteLine("Sucesso - DespesaDeslocamentoService - Client");
                DespesasChanged.Invoke();

                despesa = MappingDTOs.ConverterDTO(response.Conteudo);

                return Result.Success(despesa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DespesaDeslocamentoService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<Result<DespesaDeslocamentoDTO>> AtualizarDespesa(DespesaDeslocamentoDTO despesa)
        {
            try
            {
                var result = await _httpClient
                              .PutAsJsonAsync("api/DespesaDeslocamento", despesa);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<DespesaDeslocamento>>() ?? new();

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<DespesaDeslocamentoDTO>("Despesa com deslocamento não encontrada!");

                Console.WriteLine("Sucesso - DespesaDeslocamentoService - Client");
                DespesasChanged.Invoke();

                despesa = MappingDTOs.ConverterDTO(response.Conteudo);

                return Result.Success(despesa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DespesaDeslocamentoService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }
        public async Task<Result<DespesaDeslocamentoDTO>> GetDespesa(int IdDespesa)
        {
            try
            {
                var response = await _httpClient
                          .GetFromJsonAsync<ServiceResponse<DespesaDeslocamentoDTO>>($"api/DespesaDeslocamento/{IdDespesa}") ?? new();

                if (response.Conteudo is null || response.Conteudo.Id == 0)
                    return Result.Failure<DespesaDeslocamentoDTO>("Despesa com deslocamento não encontrada!");


                Console.WriteLine("Sucesso - DespesaDeslocamentoService - Client");
                DespesasChanged.Invoke();

                return Result.Success(response.Conteudo);
            }
            catch
            {
                Console.WriteLine("Falha - DespesaDeslocamentoService - Client");
                Mensagem = "Despesa com deslocamento não encontrada!";
                return new();
            }
        }
    }
}
