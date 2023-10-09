using DespesaViagem.Client.Pages;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace DespesaViagem.Client.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _http;
        public string Mensagem { get; set; } = "Carregando gestor...";
        public event Action GestoresChanged;

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
                var response = await _http.GetFromJsonAsync< ServiceResponse<AdminManutencaoDTO>>("api/admin/ObterUsuarios") ?? new();
                Console.WriteLine("Sucesso - AdminService - Client");

                if (response.Conteudo is null)
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
    }
}
