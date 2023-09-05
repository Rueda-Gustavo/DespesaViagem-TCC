using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponse<string>> Login(LoginUsuario request)
        {
            var result = await _http.PostAsJsonAsync("api/usuario/login", request);
            Console.WriteLine("Sucesso - UsuarioService - Client");

            ServiceResponse<string> response = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>() ?? new() { Sucesso = false };

            return response;//await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<bool>> TrocarSenha(TrocarSenhaUsuario novaSenhaUsuario)
        {
            var result = await _http.PostAsJsonAsync("api/usuario/trocar-senha", novaSenhaUsuario.Password);
            Console.WriteLine("Sucesso - UsuarioService - Client");

            ServiceResponse<bool> response = await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>() ?? new() { Sucesso = false };

            return response;//await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
