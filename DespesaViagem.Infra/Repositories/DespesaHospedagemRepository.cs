using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class DespesaHospedagemRepository : IDespesasRepository<DespesaHospedagem>
    {
        private readonly DespesaViagemContext _context;
        public DespesaHospedagemRepository(DespesaViagemContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DespesaHospedagem>> ObterTodos(int idViagem)
        {
            return await _context.DespesasHospedagem
                .Where(despesa => despesa.IdViagem == idViagem)
                //.Include(endereco => endereco.Endereco)
                .ToListAsync();
        }

        public async Task<DespesaHospedagem> ObterPorId(int id)
        {
            return await _context.DespesasHospedagem
                 //.Include(endereco => endereco.Endereco)
                 .FirstOrDefaultAsync(despesa => despesa.Id == id);
        }

        public async Task<IEnumerable<DespesaHospedagem>> ObterPorFiltro(string filtro, int idViagem)
        {
            return await _context.DespesasHospedagem
                .Where(despesa =>
                 (despesa.NomeDespesa.Contains(filtro) ||
                 despesa.DescricaoDespesa.Contains(filtro)) &&
                 despesa.Viagem.Id == idViagem)
                //.Include(endereco => endereco.Endereco)
                .ToListAsync();
        }
        public async Task Insert(DespesaHospedagem despesa)
        {
            _context.Add(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DespesaHospedagem despesa)
        {
            var despesaNaMemoria = _context.Set<DespesaHospedagem>().Find(despesa.Id);

            if (despesaNaMemoria != null)
                _context.Entry(despesaNaMemoria).State = EntityState.Detached;

            /*var enderecoNaMemoria = _context.Set<Endereco>().Find(despesa.IdEndereco);

            if (enderecoNaMemoria != null)
                _context.Entry(enderecoNaMemoria).State = EntityState.Detached;
            */
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
