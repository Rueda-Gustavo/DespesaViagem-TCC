using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly HttpClient _http;

        public FuncionarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<int>> Cadastrar(CadastroUsuario request)
        {
            var result = await _http.PostAsJsonAsync("api/funcionario", request);
            Console.WriteLine("Sucesso - FuncionarioService - Client");

            ServiceResponse<int> response = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>() ?? new() { Sucesso = false };

            return response; //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
