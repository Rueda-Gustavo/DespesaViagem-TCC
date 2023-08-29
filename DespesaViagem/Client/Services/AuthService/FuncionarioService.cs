using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.AuthService
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly HttpClient _http;

        public FuncionarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<Funcionario>> Cadastrar(CadastroUsuario request)
        {
            var result = await _http.PostAsJsonAsync("api/funcionario/cadastrar", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Funcionario>>();
        }
    }
}
