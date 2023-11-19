using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class DespesaRepository : IDespesaRepository
    {
        private readonly DespesaViagemContext _context;

        public DespesaRepository(DespesaViagemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Despesa>> ObterTodos(List<int> idsViagens)
        {
            return await _context.Despesas
                .Where(d => idsViagens.Contains(d.IdViagem)) 
                .OrderBy(d => d.IdViagem)
                .ToListAsync();
        }

        public async Task<IEnumerable<Despesa>> ObterTodos(int idViagem)
        {
            return await _context.Despesas
                .Where(despesa => despesa.IdViagem == idViagem)
                .ToListAsync();
        }        

        public async Task<Despesa> ObterPorId(int id)
        {
            return await _context.Despesas
                .FirstOrDefaultAsync(despesa => despesa.Id == id);
        }

        public async Task<IEnumerable<Despesa>> ObterPorFiltro(string filtro, int idViagem)
        {
            return await _context.Despesas
                .Where(despesa =>
                (despesa.NomeDespesa.Contains(filtro) ||
                 despesa.DescricaoDespesa.Contains(filtro)) &&
                 despesa.Viagem.Id == idViagem)
                .ToListAsync();
        }
    }
}
