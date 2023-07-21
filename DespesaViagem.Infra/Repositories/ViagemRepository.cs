using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class ViagemRepository : IViagemRepository
    {
        private readonly DespesaViagemContext _context;

        public ViagemRepository(DespesaViagemContext context)
        => _context = context;


        public async Task<IEnumerable<Viagem>> ObterTodos()
        {
            return await _context.Viagens
                .Include(f => f.Funcionario)
                .ToListAsync();
        }
        public async Task<Viagem> ObterPorId(int id)
        {
            return await _context.Viagens
                .FirstOrDefaultAsync(viagem => viagem.Id == id);

            //    .Where(viagem => viagem.Id == id)
            //    .ToListAsync();
        }

        public async Task<IEnumerable<Viagem?>> ObterViagemPorStatus(StatusViagem statusViagem)
        {            
            return await _context.Viagens
                .Include(f => f.Funcionario)
                .Where(viagem => viagem.StatusViagem == statusViagem)                
                .ToListAsync();
        }

        public async Task<IEnumerable<Viagem>> ObterPorFiltro(string filtro)
        {
            _ = int.TryParse(filtro, out int id);
            return await _context.Viagens
                .Where(viagem => viagem.Id == id || viagem.NomeViagem.Contains(filtro) || viagem.DescricaoViagem.Contains(filtro))
                .ToListAsync();
        }

        public async Task<IEnumerable<Despesa>> ObterTodasDepesas(int viagemId)
        {
            return await _context.Despesas
                .Where(despesa => despesa.Viagem.Id == viagemId)
                .ToListAsync();
        }

        public async Task Insert(Viagem viagem)
        {
            _context.Add(viagem);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Viagem viagem)
        {
            var viagemNaMemoria = _context.Set<Viagem>().Find(viagem.Id);

            if (viagemNaMemoria != null)
                _context.Entry(viagemNaMemoria).State = EntityState.Detached;

            _context.Update(viagem);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Viagem viagem)
        {
            _context.Remove(viagem);
            await _context.SaveChangesAsync();
        }
    }
}
