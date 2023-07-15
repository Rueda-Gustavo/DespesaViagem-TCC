using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IViagemRepository
    {
        Task<IEnumerable<Viagem>> ObterTodos();
        Task<Viagem> ObterPorId(int id);
        Task<IEnumerable<Viagem>> ObterPorFiltro(string filtro);
        //Diferente do método para obter todas as despesas da interface de IDespesasRepository, esse método irá retornar
        //todas as despesas referentes a viagem em questão, independente de qual tipo ela seja, Hospedagem, Passagem etc.
        Task<IEnumerable<Despesa>> ObterTodasDepesas(int viagemId);
        Task<Viagem?> ObterViagemAbertaOuEmAndamento();
        Task Insert(Viagem viagem);
        Task Update(Viagem viagem);
        Task Delete(Viagem viagem);
    }
}
