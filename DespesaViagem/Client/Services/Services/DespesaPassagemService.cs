using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaPassagemService : IDespesasService<DespesaPassagemDTO>
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; } = "Carregando despesa com passagem ...";
        public DespesaPassagemDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaPassagemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task AtualizarDespesa(DespesaPassagemDTO Despesa)
        {
            throw new NotImplementedException();
        }

        public async Task GetDespesa(int IdDespesa)
        {
            var response = await _httpClient
                          .GetFromJsonAsync<DespesaPassagemDTO>($"api/DespesaPassagem/{IdDespesa}");

            if (response == null)
                Mensagem = "Nenhuma viagem encontrada!";
            else
            {
                Despesa = response;
            }
            Console.WriteLine("Sucesso - DespesaPassagemService - Client");
            DespesasChanged.Invoke();
        }
    }
}
