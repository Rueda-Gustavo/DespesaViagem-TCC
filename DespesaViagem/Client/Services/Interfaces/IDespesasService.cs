using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Despesas;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IDespesasService<T> where T : class
    {
        event Action DespesasChanged;
        //T Despesa { get; set; }
        //string Mensagem { get; set; }
        Task<Result<T>> GetDespesa(int IdDespesa);
        Task<Result<T>> AtualizarDespesa(T Despesa);
        Task<Result<T>> AdicionarDespesa(T Despesa);

    }
}
