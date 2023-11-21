using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly HttpClient _http;

        public DespesaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<DespesaDTO>> GetDespesas()
        {
            try
            {
                var response = await _http
                   .GetFromJsonAsync<ServiceResponse<IEnumerable<DespesaDTO>>>("api/despesa") ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Any())
                {                    
                    return new();
                }
                else
                {
                    Console.WriteLine("Sucesso - DespesaService - Client");
                    return response.Conteudo.ToList();                    
                }                
            }
            catch
            {
                Console.WriteLine("Falha - DespesaService - Client");
                return new();
            }
        }

        public async Task<List<DespesaDTO>> GetDespesasDaViagem(int idViagem)
        {
            try
            {
                var response = await _http
                   .GetFromJsonAsync<ServiceResponse<IEnumerable<DespesaDTO>>>($"api/despesa/viagem/{idViagem}") ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Any())
                {
                    return new();
                }
                else
                {
                    Console.WriteLine("Sucesso - DespesaService - Client");
                    return response.Conteudo.ToList();
                }
            }
            catch
            {
                Console.WriteLine("Falha - DespesaService - Client");
                return new();
            }
        }

        public async Task<DespesaDTO> GetDespesa(int idDespesa)
        {
            try
            {
                var response = await _http
                   .GetFromJsonAsync<ServiceResponse<DespesaDTO>>($"api/despesa/{idDespesa}") ?? new() { Sucesso = false };

                if (response.Conteudo is null)
                {
                    return new();
                }
                else
                {
                    Console.WriteLine("Sucesso - DespesaService - Client");
                    return response.Conteudo;
                }
            }
            catch
            {
                Console.WriteLine("Falha - DespesaService - Client");
                return new();
            }
        }

    }
}
