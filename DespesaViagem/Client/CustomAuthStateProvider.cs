using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace DespesaViagem.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            _localStorageService = localStorageService;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string tokenJwt = await _localStorageService.GetItemAsStringAsync("tokenJwt");

            ClaimsIdentity identity = new();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(tokenJwt))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(tokenJwt), "jwt");
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", tokenJwt.Replace("\"", ""));
                }
                catch
                {
                    await _localStorageService.RemoveItemAsync(tokenJwt);
                    identity = new ClaimsIdentity();
                }

                if (TokenExpirado(identity))
                {
                    await _localStorageService.RemoveItemAsync(tokenJwt);
                    identity = new ClaimsIdentity();
                }
            }

            ClaimsPrincipal usuario = new(identity);
            AuthenticationState state = new(usuario);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string tokenJwt)
        {
            string payload = tokenJwt.Split('.')[1];
            byte[] jsonBytes = ParseBase64WithoutPadding(payload);
            Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }

        private bool TokenExpirado(ClaimsIdentity identity)
        {

            var validadeToken = identity.FindFirst("exp");
            if (validadeToken is null || !long.TryParse(validadeToken.Value, out long validade))
            {
                return true;
            }

            return DateTimeOffset.FromUnixTimeSeconds(validade) <= DateTime.UtcNow;
        }
    }
}
