using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly HttpClient _http;

        public event Action DepartamentosChanged;

        public DepartamentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Result<IEnumerable<Departamento>>> ObterDepartamentos()
        {
            try
            {
                var response = await _http
                   .GetFromJsonAsync<ServiceResponse<IEnumerable<Departamento>>>("api/departamento") ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Any())
                {
                    //Mensagem = "Nenhuma viagem encontrada!";             
                    return Result.Failure<IEnumerable<Departamento>>("Nenhum departamento encontrado.");
                }
                
                IEnumerable<Departamento> result = response.Conteudo;
                Console.WriteLine("Sucesso - DepartamentoService - Client");

                DepartamentosChanged.Invoke();
                return Result.Success(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DepartamentoService - Client");
                DepartamentosChanged.Invoke();
                return Result.Failure<IEnumerable<Departamento>>("Nenhum departamento encontrado.");
            }
        }

        public async Task<Result<Departamento>> ObterDepartamento(int idDepartamento)
        {
            try
            {
                var response = await _http
                   .GetFromJsonAsync<ServiceResponse<Departamento>>($"api/departamento/{idDepartamento}") ?? new() { Sucesso = false };

                if (response.Conteudo is null || response.Conteudo.Id == 0)
                {
                    //Mensagem = "Nenhuma viagem encontrada!";             
                    return Result.Failure<Departamento>("Nenhum departamento encontrado.");
                }

                Departamento result = response.Conteudo;
                Console.WriteLine("Sucesso - DepartamentoService - Client");

                DepartamentosChanged.Invoke();
                return Result.Success(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DepartamentoService - Client");
                DepartamentosChanged.Invoke();
                return Result.Failure<Departamento>("Nenhum departamento encontrado.");
            }
        }

        public Task<Result<Departamento>> ObterDepartamento(string descricao)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Departamento>> AdicionarDepartamento(string descricao)
        {
            try
            {
                var result = await _http.PostAsJsonAsync("api/departamento", descricao);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<Departamento>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                {
                    //Mensagem = response.Mensagem;
                    return Result.Failure<Departamento>("Erro para adicionar o departamento.");
                }

                //Mensagem = "Viagem adicionada com sucesso.";

                Console.WriteLine("Sucesso - DepartamentoService - Client");
                DepartamentosChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DepartamentoService - Client");
                //Mensagem = ex.Message;
                return Result.Failure<Departamento>("Erro para adicionar o departamento.\n" + ex.Message);
            }
        }

        public async Task<Result<Departamento>> AtualizarDepartamento(Departamento departamento)
        {
            try
            {
                var result = await _http.PutAsJsonAsync("api/departamento", departamento);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<Departamento>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                {
                    //Mensagem = response.Mensagem;
                    return Result.Failure<Departamento>("Erro para alterar o departamento.");
                }

                //Mensagem = "Viagem adicionada com sucesso.";

                Console.WriteLine("Sucesso - DepartamentoService - Client");
                DepartamentosChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DepartamentoService - Client");
                //Mensagem = ex.Message;
                return Result.Failure<Departamento>("Erro para alterar o departamento.\n" + ex.Message);
            }
        }

        public async Task<Result<Departamento>> AtivarDepartamento(int idDepartamento)
        {
            try
            {
                var result = await _http.PatchAsJsonAsync("api/departamento/ativar", idDepartamento);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<Departamento>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                {
                    //Mensagem = response.Mensagem;
                    return Result.Failure<Departamento>("Erro para ativar o departamento.");
                }

                Console.WriteLine("Sucesso - DepartamentoService - Client");
                DepartamentosChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DepartamentoService - Client");
                //Mensagem = ex.Message;
                return Result.Failure<Departamento>("Erro para ativar o departamento.\n" + ex.Message);
            }
        }

        public async Task<Result<Departamento>> DesativarDepartamento(int idDepartamento)
        {
            try
            {
                var result = await _http.PatchAsJsonAsync("api/departamento/desativar", idDepartamento);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<Departamento>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                {
                    //Mensagem = response.Mensagem;
                    return Result.Failure<Departamento>("Erro para desativar o departamento.");
                }

                Console.WriteLine("Sucesso - DepartamentoService - Client");
                DepartamentosChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - DepartamentoService - Client");
                //Mensagem = ex.Message;
                return Result.Failure<Departamento>("Erro para desativar o departamento.\n" + ex.Message);
            }
        }

        public Task<Result<Departamento>> VincularDepartamento(int idFuncionario, int idDepartamento)
        {
            throw new NotImplementedException();
        }
    }
}
