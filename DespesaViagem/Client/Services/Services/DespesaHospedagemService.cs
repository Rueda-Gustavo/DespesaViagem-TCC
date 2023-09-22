using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaHospedagemService : IDespesasService<DespesaHospedagemDTO>
    {
        private readonly HttpClient _httpClient;
        //public string Mensagem { get; set; } = "Carregando despesa com hospedagem ...";
        //public DespesaHospedagemDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaHospedagemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<DespesaHospedagemDTO>> AdicionarDespesa(DespesaHospedagemDTO despesa)
        {
            var result = await _httpClient
                          .PostAsJsonAsync("api/DespesaHospedagem", despesa);

            DespesaHospedagemDTO response = await result.Content.ReadFromJsonAsync<DespesaHospedagemDTO>();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaHospedagemDTO>("Falha para adicionar despesa!");


            Console.WriteLine("Sucesso - DespesaHospedagemService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
        }

        public async Task<Result<DespesaHospedagemDTO>> AtualizarDespesa(DespesaHospedagemDTO despesa)
        {
            var result = await _httpClient
                          .PutAsJsonAsync("api/DespesaHospedagem", despesa);

            DespesaHospedagemDTO response = await result.Content.ReadFromJsonAsync<DespesaHospedagemDTO>();

            if (response == null || response.Id == 0)
                return Result.Failure<DespesaHospedagemDTO>("Despesa com hospedagem não encontrada!");


            Console.WriteLine("Sucesso - DespesaHospedagemService - Client");
            DespesasChanged.Invoke();

            return Result.Success(response);
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
    }
}
