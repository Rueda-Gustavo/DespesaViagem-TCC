using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaHospedagemService : IDespesasService<DespesaHospedagemDTO>
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; } = "Carregando despesa com hospedagem ...";
        //public DespesaHospedagemDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaHospedagemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<DespesaHospedagemDTO>> AdicionarDespesa(DespesaHospedagemDTO despesa)
        {
            try
            {
                var result = await _httpClient
                              .PostAsJsonAsync("api/DespesaHospedagem", despesa);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<DespesaHospedagem>>() ?? new();

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<DespesaHospedagemDTO>(response.Mensagem);


                Console.WriteLine("Sucesso - DespesaHospedagemService - Client");
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

        public async Task<Result<DespesaHospedagemDTO>> AtualizarDespesa(DespesaHospedagemDTO despesa)
        {
            try
            {
                var result = await _httpClient
                              .PutAsJsonAsync("api/DespesaHospedagem", despesa);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<DespesaHospedagem>>() ?? new();

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<DespesaHospedagemDTO>(response.Mensagem);


                Console.WriteLine("Sucesso - DespesaHospedagemService - Client");
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

        public async Task<Result<DespesaHospedagemDTO>> GetDespesa(int IdDespesa)
        {
            try
            {
                var response = await _httpClient
                              .GetFromJsonAsync<ServiceResponse<DespesaHospedagemDTO>>($"api/DespesaHospedagem/{IdDespesa}") ?? new();

                if (response.Conteudo is null || response.Conteudo.Id == 0)
                    return Result.Failure<DespesaHospedagemDTO>("Despesa com hospedagem não encontrada!");


                Console.WriteLine("Sucesso - DespesaHospedagemService - Client");
                DespesasChanged.Invoke();

                return Result.Success(response.Conteudo);
            }
            catch
            {
                Console.WriteLine("Falha - DespesaHospedagemService - Client");
                return new();
            }
        }

        public async Task<Result<List<DespesaHospedagemDTO>>> GetDespesas()
        {
            try
            {
                var response = await _httpClient
                    .GetFromJsonAsync<ServiceResponse<List<DespesaHospedagemDTO>>>($"api/DespesaHospedagem") ?? new();

                if (response.Conteudo is null || !response.Conteudo.Any())
                    return Result.Failure<List<DespesaHospedagemDTO>>("Despesas com hospedagem não encontradas!");


                Console.WriteLine("Sucesso - DespesaHospedagemService - Client");
                //DespesasChanged.Invoke();

                return Result.Success(response.Conteudo);

            }
            catch
            {
                Console.WriteLine("Falha - DespesaHospedagemService - Client");
                Mensagem = "Despesas com hospedagem não encontradas!";
                return new();
            }
        }
    }
}
