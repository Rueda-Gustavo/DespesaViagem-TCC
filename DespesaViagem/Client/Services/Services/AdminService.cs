using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _http;
        public string Mensagem { get; set; } = "Carregando...";
        public event Action AdminChanged;

        public AdminService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<FuncionarioDTO>> ObterListaDeFuncionarios()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<FuncionarioDTO>>>("api/admin/lista-funcionarios") ?? new();
                Console.WriteLine("Sucesso - AdminService - Client");

                if (response.Conteudo is null || !response.Conteudo.Any())
                {
                    Mensagem = response.Mensagem;
                    return new();
                }
                else
                {
                    Console.WriteLine("Sucesso - AdminService - Client");
                    return response.Conteudo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - AdminService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<AdminManutencaoDTO> ObterListaDeUsuarios()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<AdminManutencaoDTO>>("api/admin/ObterUsuarios") ?? new();
                Console.WriteLine("Sucesso - AdminService - Client");

                if (response.Conteudo is null)
                {
                    Mensagem = response.Mensagem;
                    return new();
                }
                else
                {
                    Console.WriteLine("Sucesso - AdminService - Client");
                    AdminChanged.Invoke();
                    return response.Conteudo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - AdminService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<Result<FuncionarioDTO>> VincularGestor(int idFuncionario, int idGestor)
        {
            try
            {
                VinculoFuncionario vinculo = new VinculoFuncionario { IdFuncionario = idFuncionario, IdGestor = idGestor };

                var result = await _http.PatchAsJsonAsync("api/funcionario/vincular", vinculo);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<FuncionarioDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<FuncionarioDTO>("Erro para vincular.");

                Mensagem = "Vínculo feito com sucesso.";
                
                Console.WriteLine("Sucesso - AdminService - Client");
                AdminChanged.Invoke();

                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - AdminService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<Result<FuncionarioDTO>> DesvincularGestor(int idFuncionario)
        {
            try
            {                
                var result = await _http.PatchAsJsonAsync($"api/funcionario/desvincular/{idFuncionario}", idFuncionario);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<FuncionarioDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<FuncionarioDTO>("Erro para desvincular.");

                Mensagem = "Desvinculação feita com sucesso.";

                Console.WriteLine("Sucesso - AdminService - Client");
                AdminChanged.Invoke();

                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - AdminService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }
    }
}
