using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class GestorService : IGestorService
    {
        private readonly HttpClient _http;

        public GestorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Funcionario>> ObterListaDeFuncionarios()
        {
            var result = await _http.GetFromJsonAsync<List<Funcionario>>("api/gestor/lista-funcionarios") ?? new();
            Console.WriteLine("Sucesso - GestorService - Client");
            
            return result;
        }
    }
}
