using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaPassagemService : IDespesasService<DespesaPassagemDTO>
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; } = "Carregando despesa com passagem ...";
        public DespesaPassagemDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaPassagemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<DespesaPassagemDTO>> AdicionarDespesa(DespesaPassagemDTO despesa)
        {
            try
            {
                var result = await _httpClient
                              .PostAsJsonAsync("api/DespesaPassagem", despesa);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<DespesaPassagem>>() ?? new();

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<DespesaPassagemDTO>(response.Mensagem);


                Console.WriteLine("Sucesso - DespesaPassagemService - Client");
                DespesasChanged.Invoke();

                despesa = MappingDTOs.ConverterDTO(response.Conteudo);

                return Result.Success(despesa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DespesaPassagemService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<Result<DespesaPassagemDTO>> AtualizarDespesa(DespesaPassagemDTO despesa)
        {
            try
            {
                var result = await _httpClient
                              .PutAsJsonAsync("api/DespesaPassagem", despesa);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<DespesaPassagem>>() ?? new();

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<DespesaPassagemDTO>(response.Mensagem);


                Console.WriteLine("Sucesso - DespesaPassagemService - Client");
                DespesasChanged.Invoke();

                despesa = MappingDTOs.ConverterDTO(response.Conteudo);

                return Result.Success(despesa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DespesaHospedagemService - Client");
                Mensagem = ex.Message;
                return new();
            }
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

        public async Task<Result<List<DespesaPassagemDTO>>> GetDespesas()
        {
            try
            {
                var response = await _httpClient
                    .GetFromJsonAsync<ServiceResponse<List<DespesaPassagemDTO>>>($"api/DespesaPassagem") ?? new();

                if (response.Conteudo is null || !response.Conteudo.Any())
                    return Result.Failure<List<DespesaPassagemDTO>>("Despesas com passagem não encontradas!");


                Console.WriteLine("Sucesso - DespesaPassagemService - Client");
                //DespesasChanged.Invoke();

                return Result.Success(response.Conteudo);

            }
            catch
            {
                Console.WriteLine("Falha - DespesaPassagemService - Client");
                Mensagem = "Despesas com passagem não encontradas!";
                return new();
            }
        }
    }
}
