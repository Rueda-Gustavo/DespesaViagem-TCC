using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly HttpClient _http;
        public event Action FuncionariosChanged;

        public string Mensagem { get; set; } = "Carregando funcionário...";

        public FuncionarioService(HttpClient http) => _http = http;

        public async Task<ServiceResponse<int>> Cadastrar(CadastroUsuario request)
        {
            try
            {
                var result = await _http.PostAsJsonAsync("api/funcionario", request);
                Console.WriteLine("Sucesso - FuncionarioService - Client");

                ServiceResponse<int> response = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>() ?? new() { Sucesso = false };

                return response; //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - FuncionarioService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<Result<FuncionarioDTO>> AtualizarPerfil(FuncionarioDTO funcionario)
        {
            try
            {
                var result = await _http.PutAsJsonAsync("api/funcionario", funcionario);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<FuncionarioDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                {
                    Mensagem = response.Mensagem;
                    return Result.Failure<FuncionarioDTO>("Erro para atualizar as informações.");
                }
                else
                    Mensagem = "Informações atualizadas com sucesso.";

                Console.WriteLine("Sucesso - FuncionarioService - Client");
                FuncionariosChanged.Invoke();
                return Result.Success(response.Conteudo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - FuncionarioService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<FuncionarioDTO> GetFuncionario(string CPF)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<FuncionarioDTO>>($"api/funcionario/{CPF}/obterfuncionarioporfiltro")
                    ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso || response.Conteudo.CPF == string.Empty)
                {
                    Mensagem = response.Mensagem;
                    Console.WriteLine("Funcionário não encontrado.");
                    return new();
                }

                //FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(response.Conteudo);

                Console.WriteLine("Sucesso - FuncionarioService - Client");
                return response.Conteudo;
            }
            catch
            {
                Console.WriteLine("Falha - FuncionarioService - Client");
                Mensagem = "Funcionario não encontrado!";
                return new();
            }
        }

        public async Task<FuncionarioDTO> GetFuncionario(int idFuncionario)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<FuncionarioDTO>>($"api/funcionario/{idFuncionario}")
                    ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso || response.Conteudo.CPF == string.Empty)
                {
                    Console.WriteLine("Funcionário não encontrado.");
                    return new();
                }

                //FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(response.Conteudo);

                Console.WriteLine("Sucesso - FuncionarioService - Client");
                return response.Conteudo;
            }
            catch
            {
                Console.WriteLine("Falha - FuncionarioService - Client");
                Mensagem = "Funcionario não encontrado!";
                return new();
            }
        }
    }
}
