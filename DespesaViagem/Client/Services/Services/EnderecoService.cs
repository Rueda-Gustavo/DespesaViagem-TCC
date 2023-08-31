using DespesaViagem.Client.Pages;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; }
        public Endereco Endereco { get; set; }

        public EnderecoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task GetEnderecos()
        {
            throw new NotImplementedException();
        }

        public async Task GetEndereco(int idEndereco)
        {
            var response = await _httpClient
                           .GetFromJsonAsync<Endereco>($"api/Endereco/{idEndereco}");

            if (response == null)
                Mensagem = "Nenhum endereco encontrado!";
            else
            {
                Endereco = response;
            }
            Console.WriteLine("Sucesso - EnderecoService - Client");
        }


    }
}
