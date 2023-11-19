using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class DespesaDeslocamentoRepository : IDespesasRepository<DespesaDeslocamento>
    {
        private readonly DespesaViagemContext _context;
        public DespesaDeslocamentoRepository(DespesaViagemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DespesaDeslocamento>> ObterTodos(List<int> idsViagens)
        {
            return await _context.DespesasDeslocamento
                .Where(despesa => idsViagens.Contains(despesa.IdViagem))
                .ToListAsync();
        }

        public async Task<IEnumerable<DespesaDeslocamento>> ObterTodos(int idViagem)
        {
            return await _context.DespesasDeslocamento
                .Where(despesa => despesa.IdViagem == idViagem)                
                .ToListAsync();
        }

        public async Task<DespesaDeslocamento> ObterPorId(int id)
            => await _context.DespesasDeslocamento                
                .FirstOrDefaultAsync(despesa => despesa.Id == id);

        public async Task<IEnumerable<DespesaDeslocamento>> ObterPorFiltro(string filtro, int idViagem)
        {
            return await _context.DespesasDeslocamento
                .Where(despesa =>
                 (despesa.NomeDespesa.Contains(filtro) ||
                 despesa.DescricaoDespesa.Contains(filtro) ||
                 despesa.Placa.Contains(filtro) || 
                 despesa.Modelo.Contains(filtro) ||
                 despesa.Quilometragem.ToString().Contains(filtro) ||
                 despesa.ValorPorQuilometro.ToString().Contains(filtro)) &&
                 despesa.Viagem.Id == idViagem)
                .ToListAsync();
        }
        public async Task Insert(DespesaDeslocamento despesa)
        {
            _context.Add(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DespesaDeslocamento despesa)
        {
            var despesaNaMemoria = _context.Set<DespesaDeslocamento>().Find(despesa.Id);

            if (despesaNaMemoria != null)
                _context.Entry(despesaNaMemoria).State = EntityState.Detached;

            _context.Update(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(DespesaDeslocamento despesa)
        {
            _context.Remove(despesa);
            await _context.SaveChangesAsync();
        }
    }
}
