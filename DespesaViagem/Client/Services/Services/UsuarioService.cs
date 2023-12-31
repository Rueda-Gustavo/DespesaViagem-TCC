﻿using DespesaViagem.Client.Pages;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
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

        public async Task<UsuarioDTO> GetUsuario(int idUsuario)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<UsuarioDTO>>($"api/usuario/{idUsuario}")
                    ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso || response.Conteudo.CPF == string.Empty)
                {
                    Console.WriteLine("Usuário não encontrado.");
                    return new();
                }

                //FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(response.Conteudo);

                Console.WriteLine("Sucesso - UsuarioService - Client");
                return response.Conteudo;
            }
            catch
            {
                Console.WriteLine("Falha - UsuarioService - Client");
                //Mensagem = "Usuário não encontrado!";
                return new();
            }
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

        public async Task<ServiceResponse<RolesUsuario>> ObterTipoUsuario()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<RolesUsuario>>("api/usuario/tipo-usuario") ?? new() { Sucesso = false };
                Console.WriteLine("Sucesso - UsuarioService - Client");

                return response;
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                return new() { Sucesso = false, Mensagem = "Falha para carregar o usuário." };
            }
        }
    }
}
