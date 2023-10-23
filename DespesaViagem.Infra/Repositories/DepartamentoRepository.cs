using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Viagens;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly DespesaViagemContext _context;

        public DepartamentoRepository(DespesaViagemContext context) => _context = context;

        public async Task<IEnumerable<Departamento>> ObterDepartamentos()
        {
            return await _context.Departamentos.ToListAsync();
        }

        public async Task<Departamento> ObterDepartamento(int id)
        {
            return await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Departamento> ObterDepartamento(string descricao)
        {
            return await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Descricao == descricao);
        }

        public async Task Insert(Departamento departamento)
        {
            _context.Add(departamento);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Departamento departamento)
        {
            var departamentoNaMemoria = _context.Set<Departamento>().Find(departamento.Id);

            if (departamentoNaMemoria != null)
                _context.Entry(departamentoNaMemoria).State = EntityState.Detached;
            _context.Update(departamento);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Departamento departamento)
        {
            _context.Remove(departamento);
            await _context.SaveChangesAsync();
        }
    }
}
