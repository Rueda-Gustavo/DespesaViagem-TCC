﻿using CSharpFunctionalExtensions;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class ViagemService : IViagemService
    {
        public event Action ViagensChanged;

        private readonly HttpClient _http;
        //private readonly ILogger<ViagemService> _logger;

        public List<ViagemDTO> Viagens { get; set; } = new List<ViagemDTO>();
        public ViagensPorPagina ViagensPorPagina { get; set; } = new();

        public string Mensagem { get; set; } = "Carregando viagens...";

        public ViagemService(HttpClient http/*, ILogger<ViagemService> logger*/)
        {
            _http = http;
            //_logger = logger;
        }

        public async Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagem)
        {
            try
            {
                var result = await _http.PostAsJsonAsync("api/viagem", viagem);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<ViagemDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                {
                    Mensagem = response.Mensagem;
                    //Console.WriteLine(response.Mensagem);
                    return Result.Failure<ViagemDTO>(response.Mensagem);
                }

                Mensagem = "Viagem adicionada com sucesso.";

                Console.WriteLine("Sucesso - ViagemService - Client");
                ViagensChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = ex.Message;
                return new();
            }

        }

        public async Task<Result<ViagemDTO>> AtualizarViagem(ViagemDTO viagem)
        {
            try
            {
                var result = await _http.PutAsJsonAsync("api/viagem", viagem);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<ViagemDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<ViagemDTO>("Erro para editar a viagem.");

                Mensagem = "Viagem atualizada com sucesso.";

                Console.WriteLine("Sucesso - ViagemService - Client");
                ViagensChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = ex.Message;
                return Result.Failure<ViagemDTO>(ex.Message);
            }
        }

        public async Task GetViagens()
        {
            try
            {
                var response = await _http
                   .GetFromJsonAsync<ServiceResponse<List<ViagemDTO>>>("api/viagem") ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Any())
                {
                    //Mensagem = "Nenhuma viagem encontrada!";             
                    Viagens = new List<ViagemDTO>();
                    Mensagem = response.Mensagem;
                }
                else
                {
                    Viagens = response.Conteudo;
                    Console.WriteLine("Sucesso - ViagemService - Client");
                }
                ViagensChanged.Invoke();
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Nenhuma viagem encontrada!";
                Viagens = new();
                ViagensChanged.Invoke();
            }
        }

        public async Task GetViagensPorStatus(StatusViagem statusViagem, int pagina)
        {
            try
            {
                var response = await _http
                   .GetFromJsonAsync<ServiceResponse<ViagensPorPagina>>($"api/viagem/status/{statusViagem}/TodasViagens/{pagina}") ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Viagens.Any())
                {
                    //Mensagem = "Nenhuma viagem encontrada!";             
                    Viagens = new List<ViagemDTO>();
                    Mensagem = response.Mensagem;
                }
                else
                {
                    ViagensPorPagina = response.Conteudo;
                    Console.WriteLine("Sucesso - ViagemService - Client");
                }
                ViagensChanged.Invoke();
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Nenhuma viagem encontrada!";
                ViagensPorPagina = new();
                ViagensChanged.Invoke();
            }
        }

        public async Task GetViagens(List<StatusViagem> statusViagem)
        {
            try
            {
                var result = await _http.PostAsJsonAsync($"api/viagem/listaStatus/TodasViagens", statusViagem);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<List<ViagemDTO>>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Any())
                {
                    //Mensagem = "Nenhuma viagem encontrada!";
                    Viagens = new List<ViagemDTO>();
                    Mensagem = response.Mensagem;
                }
                else
                {
                    Viagens = response.Conteudo;
                    Console.WriteLine("Sucesso - ViagemService - Client");
                }
                ViagensChanged.Invoke();
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Nenhuma viagem encontrada!";
                Viagens = new();
                ViagensChanged.Invoke();
            }
        }

        public async Task GetViagens(int pagina)
        {
            try
            {
                var response = await _http
                   .GetFromJsonAsync<ServiceResponse<ViagensPorPagina>>($"api/viagem/ObterViagensPorPagina/{pagina}") ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Viagens.Any())
                {
                    //Mensagem = "Nenhuma viagem encontrada!";             
                    ViagensPorPagina = new();
                    Mensagem = response.Mensagem;
                }
                else
                {
                    ViagensPorPagina = response.Conteudo;
                    Console.WriteLine("Sucesso - ViagemService - Client");
                }
                ViagensChanged.Invoke();
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Nenhuma viagem encontrada!";
                ViagensPorPagina = new();
                ViagensChanged.Invoke();
            }
        }

        public async Task GetViagensPorFuncionario(int idFuncionario, int pagina)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<ViagensPorPagina>>($"api/viagem/PorFuncionario/{idFuncionario}/{pagina}") ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Viagens.Any())
                {
                    //Mensagem = "Nenhuma viagem encontrada!";
                    Mensagem = response.Mensagem;
                    ViagensPorPagina = new();
                }
                else
                {
                    ViagensPorPagina = response.Conteudo;

                    Console.WriteLine("Sucesso - ViagemService - Client");
                }

                ViagensChanged.Invoke();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Falha - ViagemService - Client\n" + ex.Message);
                Mensagem = "Viagens não encontradas!";
                ViagensPorPagina = new();
                ViagensChanged.Invoke();
            }
        }
        
        public async Task GetViagensPorDepartamento(int idDepartamento, int pagina)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<ViagensPorPagina>>($"api/Viagem/PorDepartamento/{idDepartamento}/{pagina}") ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Viagens.Any())
                {
                    //Mensagem = "Nenhuma viagem encontrada!";
                    Mensagem = response.Mensagem;
                    Viagens = new();
                }
                else
                {
                    /*response.Conteudo.Viagens = response.Conteudo.Viagens
                        .Where(v => v.Funcionario.Departamento is not null && v.Funcionario.Departamento.Id == idDepartamento).ToList();*/

                    ViagensPorPagina = response.Conteudo;

                    Console.WriteLine("Sucesso - ViagemService - Client");
                }
                ViagensChanged.Invoke();
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Viagens não encontradas!";
                ViagensPorPagina = new();
                ViagensChanged.Invoke();
            }
        }
        public async Task<ViagemDTO> GetViagem(int idViagem)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<ViagemDTO>>($"api/Viagem/{idViagem}") ?? new() { Sucesso = false };

                if (response.Conteudo is null)
                {
                    //Mensagem = "Nenhuma viagem encontrada!";
                    Mensagem = response.Mensagem;
                    return new ViagemDTO();
                }
                else
                {
                    Console.WriteLine("Sucesso - ViagemService - Client");
                    ViagensChanged.Invoke();
                    return response.Conteudo;
                }
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Nenhuma viagem encontrada!";
                ViagensChanged.Invoke();
                return new ViagemDTO();
            }
        }
        public async Task<ViagemDTO> ObterViagemAbertaOuEmAndamento()
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<ViagemDTO>>($"api/Viagem/ViagemAbertaOuEmAndamento") ?? new() { Sucesso = false };

                if (response.Conteudo is null)
                {
                    //Mensagem = "Nenhuma viagem encontrada!";
                    Mensagem = response.Mensagem;
                    return new ViagemDTO();
                }
                else
                {
                    Console.WriteLine("Sucesso - ViagemService - Client");
                    ViagensChanged.Invoke();
                    return response.Conteudo;
                }
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Nenhuma viagem Aberta ou em Andamento foi encontrada!";
                ViagensChanged.Invoke();
                return new ViagemDTO();
            }
        }


        public async Task<DespesasPorPagina> ObterDespesasPorPagina(int idViagem, int pagina)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<DespesasPorPagina>>($"api/Viagem/ObterDespesasPorPagina/{idViagem}/{pagina}")
                    ?? new() { Sucesso = false };

                if (response.Conteudo is null)
                {
                    Console.WriteLine("Despesas não encontradas.");
                    return new();
                }

                Console.WriteLine("Sucesso - ViagemService - Client");
                ViagensChanged.Invoke();

                return response.Conteudo;
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Despesas não encontradas.";
                return new();
            }

        }

        public async Task<DespesasPorPagina> ObterTodasDespesasPaginadasPorTipo(int idViagem, int pagina, string tipoDespesa)
        {
            try
            {
                var response = await _http
                .GetFromJsonAsync<ServiceResponse<DespesasPorPagina>>($"api/Viagem/ObterTodasDespesasPaginadasPorTipo/{idViagem}/{pagina}/{tipoDespesa}")
                ?? new() { Sucesso = false };

                if (response.Conteudo is null)
                {
                    Console.WriteLine("Despesas não encontradas.");
                    return new();
                }

                Console.WriteLine("Sucesso - ViagemService - Client");
                ViagensChanged.Invoke();

                return response.Conteudo;
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Despesas não encontradas.";
                return new();
            }

        }


        public async Task<List<DespesaPorCategoria>> ObterTotalDespesasPorCategoria(int idViagem)
        {
            try
            {
                var response = await _http
                    .GetFromJsonAsync<ServiceResponse<List<DespesaPorCategoria>>>($"api/Viagem/ObterDespesasPorCategoria/{idViagem}")
                    ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Conteudo.Any())
                {
                    Console.WriteLine("Despesas não encontradas.");
                    return new();
                }

                Console.WriteLine("Sucesso - ViagemService - Client");
                return response.Conteudo;
            }
            catch
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = "Despesas não encontradas.";
                return new();
            }

        }

        public async Task<Result<ViagemDTO>> IniciarViagem()
        {
            try
            {
                var result = await _http.PatchAsync("api/viagem/Iniciar", null);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<ViagemDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<ViagemDTO>("Erro para iniciar a viagem.");

                Mensagem = "Viagem iniciada com sucesso.";

                //ViagemDTO viagemAntiga = Viagens.FirstOrDefault(v => v.Id == response.Conteudo.Id) ?? new();
                //Viagens.Remove(viagemAntiga);
                //Viagens.Add(response.Conteudo);

                ViagemDTO viagemAntiga = ViagensPorPagina.Viagens.FirstOrDefault(v => v.Id == response.Conteudo.Id) ?? new();
                ViagensPorPagina.Viagens.Remove(viagemAntiga);
                ViagensPorPagina.Viagens.Add(response.Conteudo);

                Console.WriteLine("Sucesso - ViagemService - Client");
                //ViagensChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = ex.Message;
                return Result.Failure<ViagemDTO>(ex.Message);
            }
        }

        public async Task<Result<ViagemDTO>> CancelarViagem()
        {
            try
            {
                var result = await _http.PatchAsync("api/viagem/Cancelar", null);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<ViagemDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<ViagemDTO>("Erro para cancelar a viagem.");

                Mensagem = "Viagem cancelada com sucesso.";

                //ViagemDTO viagemAntiga = Viagens.FirstOrDefault(v => v.Id == response.Conteudo.Id) ?? new();
                //Viagens.Remove(viagemAntiga);
                //Viagens.Add(response.Conteudo);

                ViagemDTO viagemAntiga = ViagensPorPagina.Viagens.FirstOrDefault(v => v.Id == response.Conteudo.Id) ?? new();
                ViagensPorPagina.Viagens.Remove(viagemAntiga);
                ViagensPorPagina.Viagens.Add(response.Conteudo);

                Console.WriteLine("Sucesso - ViagemService - Client");
                //ViagensChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }

        public async Task<Result<ViagemDTO>> EncerrarViagem()
        {
            try
            {
                var result = await _http.PatchAsync("api/viagem/Encerrar", null);

                var response = await result.Content.ReadFromJsonAsync<ServiceResponse<ViagemDTO>>() ?? new() { Sucesso = false };

                if (response.Conteudo is null || !response.Sucesso)
                    return Result.Failure<ViagemDTO>("Erro para encerrar a viagem.");

                Mensagem = "Viagem encerrada com sucesso.";

                //ViagemDTO viagemAntiga = Viagens.FirstOrDefault(v => v.Id == response.Conteudo.Id) ?? new();
                //Viagens.Remove(viagemAntiga);
                //Viagens.Add(response.Conteudo);

                ViagemDTO viagemAntiga = ViagensPorPagina.Viagens.FirstOrDefault(v => v.Id == response.Conteudo.Id) ?? new();
                ViagensPorPagina.Viagens.Remove(viagemAntiga);
                ViagensPorPagina.Viagens.Add(response.Conteudo);

                Console.WriteLine("Sucesso - ViagemService - Client");
                //ViagensChanged.Invoke();
                return Result.Success(response.Conteudo); //await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha - ViagemService - Client");
                Mensagem = ex.Message;
                return new();
            }
        }
    }
}


