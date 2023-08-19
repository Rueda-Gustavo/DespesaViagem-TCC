using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Despesas;
using System.Net.Http.Json;

namespace DespesaViagem.Client.Services.DespesasService
{
    public class DespesaHospedagemService : IDespesasService<DespesaHospedagemDTO>
    {
        private readonly HttpClient _httpClient;
        public string Mensagem { get; set; } = "Carregando despesa com hospedagem ...";
        public DespesaHospedagemDTO Despesa { get; set; } = new();

        public event Action DespesasChanged;


        public DespesaHospedagemService(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }

        public Task AtualizarDespesa(DespesaHospedagemDTO Despesa)
        {
            throw new NotImplementedException();
        }

        public async Task GetDespesa(int IdDespesa)
        {
            var response = await _httpClient
                          .GetFromJsonAsync<DespesaHospedagemDTO>($"api/DespesaHospedagem/{IdDespesa}");

            if (response == null)
                Mensagem = "Nenhuma viagem encontrada!";
            else
            {
                Despesa = response;
            }
            Console.WriteLine("Sucesso - DespesaHospedagemService - Client");
            DespesasChanged.Invoke();
        }
    }
}
