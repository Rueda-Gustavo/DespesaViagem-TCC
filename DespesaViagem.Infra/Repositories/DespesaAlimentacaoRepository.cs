using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class DespesaAlimentacaoRepository : IDespesasRepository<DespesaAlimentacao>
    {
        private readonly DespesaViagemContext _context;
        public DespesaAlimentacaoRepository(DespesaViagemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DespesaAlimentacao>> ObterTodos(int idViagem)
        {
            return await _context.DespesasAlimentacao
                .Where(despesa => despesa.IdViagem == idViagem)
                .ToListAsync();
        }

        public async Task<DespesaAlimentacao> ObterPorId(int id)
            => await _context.DespesasAlimentacao
                .FirstOrDefaultAsync(despesa => despesa.Id == id);

        public async Task<IEnumerable<DespesaAlimentacao>> ObterPorFiltro(string filtro, int idViagem)
        {
            return await _context.DespesasAlimentacao
                .Where(despesa =>
                 (despesa.NomeDespesa.Contains(filtro) ||
                 despesa.DescricaoDespesa.Contains(filtro) ||
                 despesa.NomeEstabelecimento.Contains(filtro) ||
                 despesa.CNPJ.Contains(filtro) ||
                 despesa.ValorRefeicao.ToString().Contains(filtro)) &&
                 despesa.Viagem.Id == idViagem)
                .ToListAsync();
        }
        public async Task Insert(DespesaAlimentacao despesa)
        {
            _context.Add(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DespesaAlimentacao despesa)
        {
            var despesaNaMemoria = _context.Set<DespesaAlimentacao>().Find(despesa.Id);

            if (despesaNaMemoria != null)
                _context.Entry(despesaNaMemoria).State = EntityState.Detached;

            _context.Update(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(DespesaAlimentacao despesa)
        {
            _context.Remove(despesa);
            await _context.SaveChangesAsync();
        }
    }
}
