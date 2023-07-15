using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DespesaViagemContext _context;

        public EnderecoRepository(DespesaViagemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Endereco>> ObterTodos()
        {
            return await _context.Enderecos.ToListAsync();
        }

        public async Task<Endereco> ObterPorId(int id)
        {
            return await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Endereco>> ObterPorFiltro(string filtro)
        {
            return await _context.Enderecos
            .Where(endereco => endereco.Logradouro.Contains(filtro) || endereco.CEP.Contains(filtro) || endereco.Cidade.Contains(filtro) || endereco.Estado.Contains(filtro))
            .ToListAsync();
        }

        public async Task Insert(Endereco endereco)
        {
            _context.Add(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Endereco endereco)
        {
            var enderecoNaMemoria = _context.Set<Endereco>().Find(endereco.Id);

            if (enderecoNaMemoria != null)
                _context.Entry(enderecoNaMemoria).State = EntityState.Detached;

            _context.Entry(endereco).State = EntityState.Modified;
            _context.Update(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Endereco endereco)
        {
            _context.Remove(endereco);
            await _context.SaveChangesAsync();
        }
    }
}
