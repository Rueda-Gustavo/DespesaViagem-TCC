﻿using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Diagnostics.Tracing;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class GestorService : IGestorService
    {
        private readonly HttpClient _http;
        public string Mensagem { get; set; } = "Carregando gestor...";
        public event Action GestoresChanged;

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

        public async Task<Result<GestorDTO>> AtualizarPerfil(GestorDTO gestor)
        {
            try
            {
                var result = await _http.PutAsJsonAsync("api/gestor", gestor);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<GestorDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<GestorDTO>("Erro para atualizar as informações.");

                Mensagem = "Informações atualizadas com sucesso.";

                Console.WriteLine("Sucesso - GestorService - Client");
                //GestoresChanged.Invoke();
                return Result.Success(response.Conteudo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - GestorService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<GestorDTO> GetGestor(string CPF)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<Gestor>>($"api/gestor/{CPF}/obterfuncionarioporfiltro")
                    ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso || response.Conteudo.CPF == string.Empty)
                {
                    Mensagem = response.Mensagem;
                    Console.WriteLine("Gestor não encontrado.");
                    return new();
                }

                GestorDTO gestorDTO = MappingDTOs.ConverterDTO(response.Conteudo);

                Console.WriteLine("Sucesso - GestorService - Client");
                return gestorDTO;
            }
            catch
            {
                Console.WriteLine("Falha - GestorService - Client");
                Mensagem = "Gestor não encontrado!";
                return new();
            }
        }

        public async Task<GestorDTO> GetGestor(int idGestor)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<Gestor>>($"api/gestor/{idGestor}")
                    ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso || response.Conteudo.CPF == string.Empty)
                {
                    Console.WriteLine("Gestor não encontrado.");
                    return new();
                }

                GestorDTO gestorDTO = MappingDTOs.ConverterDTO(response.Conteudo);

                Console.WriteLine("Sucesso - GestorService - Client");
                return gestorDTO;
            }
            catch
            {
                Console.WriteLine("Falha - GestorService - Client");
                Mensagem = "Gestor não encontrado!";
                return new();
            }
        }
    }
}
