using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DespesaViagemContext _context;

        public UsuarioRepository(DespesaViagemContext context)
        {
            _context = context;
        }
        public async Task<bool> UsuarioJaExiste(string filtro)
        {
            return await _context.Usuarios
                .AnyAsync(usuario => usuario.Username.ToLower().Equals(filtro.ToLower()) ||
                                     usuario.CPF.ToLower().Equals(filtro.ToLower()));
        }

        public async Task<Usuario?> ObterUsuario(string username)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
        }

        public async Task<Usuario?> ObterUsuario(int idUsuario)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id.Equals(idUsuario));
        }

        public async Task Update(Usuario usuario)
        {
            var usuarioNaMemoria = _context.Set<Usuario>().Find(usuario.Id);

            if (usuarioNaMemoria != null)
                _context.Entry(usuarioNaMemoria).State = EntityState.Detached;

            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
