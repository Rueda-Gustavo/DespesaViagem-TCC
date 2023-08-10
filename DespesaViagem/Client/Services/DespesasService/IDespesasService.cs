using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Despesas;

namespace DespesaViagem.Client.Services.DespesasService
{
    public interface IDespesasService<T> where T : class
    {
        event Action DespesasChanged;
        T Despesa { get; set; }
        string Mensagem { get; set; }
        Task GetDespesa(int IdDespesa);
        Task AtualizarDespesa(T Despesa);

    }
}
