using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class DespesaHospedagemRepository : IDespesasRepository<DespesaHospedagem, int>
    {
        private readonly DespesaViagemContext _context;
        public DespesaHospedagemRepository(DespesaViagemContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DespesaHospedagem>> ObterTodos(int idViagem)
        {
            return await _context.DespesasHospedagens
                .Where(despesa => despesa.IdViagem == idViagem)
                .Include(endereco => endereco.Endereco)
                .ToListAsync();
        }

        public async Task<DespesaHospedagem> ObterPorId(int id)
            => await _context.DespesasHospedagens
                .Include(endereco => endereco.Endereco)
                .FirstOrDefaultAsync(despesa => despesa.Id == id);

        public async Task<IEnumerable<DespesaHospedagem>> ObterPorFiltro(string filtro, int idViagem)
        {
            return await _context.DespesasHospedagens
                .Where(despesa =>
                 (despesa.NomeDespesa.Contains(filtro) ||
                 despesa.DescricaoDespesa.Contains(filtro)) &&
                 despesa.Viagem.Id == idViagem)
                 .Include(endereco => endereco.Endereco)
                .ToListAsync();
        }
        public async Task Insert(DespesaHospedagem despesa, Viagem viagem)
        {
            despesa.Viagem = viagem;
            _context.Add(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DespesaHospedagem despesa)
        {
            _context.Update(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(DespesaHospedagem despesa)
        {
            _context.Remove(despesa);
            await _context.SaveChangesAsync();
        }
    }
}
