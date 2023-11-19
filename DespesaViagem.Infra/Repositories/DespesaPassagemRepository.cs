using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    internal class DespesaPassagemRepository : IDespesasRepository<DespesaPassagem>
    {
        private readonly DespesaViagemContext _context;
        public DespesaPassagemRepository(DespesaViagemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DespesaPassagem>> ObterTodos(List<int> idsViagens)
        {
            return await _context.DespesasPassagem
                .Where(despesa => idsViagens.Contains(despesa.IdViagem))
                .ToListAsync();
        }

        public async Task<IEnumerable<DespesaPassagem>> ObterTodos(int idViagem)
        {
            return await _context.DespesasPassagem
                .Where(despesa => despesa.IdViagem == idViagem)
                .ToListAsync();
        }

        public async Task<DespesaPassagem> ObterPorId(int id)
            => await _context.DespesasPassagem
                .FirstOrDefaultAsync(despesa => despesa.Id == id);

        public async Task<IEnumerable<DespesaPassagem>> ObterPorFiltro(string filtro, int idViagem)
        {
            return await _context.DespesasPassagem
                .Where(despesa =>
                 (despesa.NomeDespesa.Contains(filtro) ||
                 despesa.DescricaoDespesa.Contains(filtro) ||
                 despesa.Destino.Contains(filtro) ||
                 despesa.Origem.Contains(filtro) || 
                 despesa.Companhia.Contains(filtro) || 
                 despesa.DataHoraEmbarque.ToString().Contains(filtro) ||
                 despesa.Preco.ToString().Contains(filtro)) &&
                 despesa.Viagem.Id == idViagem)
                .ToListAsync();
        }
        public async Task Insert(DespesaPassagem despesa)
        {
            _context.Add(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DespesaPassagem despesa)
        {
            var despesaNaMemoria = _context.Set<DespesaPassagem>().Find(despesa.Id);

            if (despesaNaMemoria != null)
                _context.Entry(despesaNaMemoria).State = EntityState.Detached;

            _context.Update(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(DespesaPassagem despesa)
        {
            _context.Remove(despesa);
            await _context.SaveChangesAsync();
        }
    }
}
