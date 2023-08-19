using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Infra.Interfaces
{
    public interface IViagemRepository
    {
        Task<List<Viagem>> ObterTodos();
        Task<Viagem> ObterPorId(int id);
        Task<List<Viagem>> ObterPorFiltro(string filtro);
        //Diferente do método para obter todas as despesas da interface de IDespesasRepository, esse método irá retornar
        //todas as despesas referentes a viagem em questão, independente de qual tipo ela seja, Hospedagem, Passagem etc.
        Task<List<Despesa>> ObterTodasDepesas(int viagemId);
        Task<List<Viagem?>> ObterViagemPorStatus(StatusViagem statusViagem);
        Task<List<DespesaPorCategoria>> ObterTotalDasDespesasPorCategoria(int viagemId);
        Task Insert(Viagem viagem);
        Task Update(Viagem viagem);
        Task Delete(Viagem viagem);
    }
}
