using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.Services
{
    public class DespesaAlimentacaoService : IDespesasService<DespesaAlimentacaoDTO>
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; } = "Carregando despesa com alimentação ...";
        public DespesaAlimentacaoDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaAlimentacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task AtualizarDespesa(DespesaAlimentacaoDTO Despesa)
        {
            throw new NotImplementedException();
        }

        public async Task GetDespesa(int IdDespesa)
        {
            var response = await _httpClient
                          .GetFromJsonAsync<DespesaAlimentacaoDTO>($"api/DespesaAlimentacao/{IdDespesa}");

            if (response == null)
                Mensagem = "Nenhuma viagem encontrada!";
            else
            {
                Despesa = response;
            }
            Console.WriteLine("Sucesso - DespesaAlimentacaoService - Client");
            DespesasChanged.Invoke();
        }
    }
}
