using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaDeslocamentoService : IDespesasService<DespesaDeslocamentoDTO>
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; } = "Carregando despesa com deslocamento ...";
        public DespesaDeslocamentoDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaDeslocamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task AtualizarDespesa(DespesaDeslocamentoDTO Despesa)
        {
            throw new NotImplementedException();
        }

        public async Task GetDespesa(int IdDespesa)
        {
            var response = await _httpClient
                          .GetFromJsonAsync<DespesaDeslocamentoDTO>($"api/DespesaDeslocamento/{IdDespesa}");

            if (response == null)
                Mensagem = "Nenhuma viagem encontrada!";
            else
            {
                Despesa = response;
            }
            Console.WriteLine("Sucesso - DespesaDeslocamentoService - Client");
            DespesasChanged.Invoke();
        }
    }
}
