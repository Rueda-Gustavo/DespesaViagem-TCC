﻿using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class GestorRepository : IGestorRepository
    {
        private readonly DespesaViagemContext _context;

        public GestorRepository(DespesaViagemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gestor>> ObterTodos()
        {
            return await _context.Gestores.ToListAsync();
        }

        public async Task<Gestor> ObterPorId(int id)
        {
            return await _context.Gestores
                .Include(g => g.Funcionarios)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Gestor> ObterPorCPF(string CPF)
        {
            return await _context.Gestores
                .Include(g => g.Funcionarios)
                .FirstOrDefaultAsync(g => g.CPF == CPF);
        }

        public async Task<IEnumerable<Gestor>> ObterPorFiltro(string filtro)
        {
            return await _context.Gestores
                .Include(g => g.Funcionarios)
                .Where(Gestor => Gestor.CPF.Contains(filtro) ||
                                 Gestor.NomeCompleto.Contains(filtro))
                .ToListAsync();
        }

        public async Task<bool> UsuarioJaExiste(string filtro)
        {
            return await _context.Usuarios
                .AnyAsync(usuario => usuario.Username.ToLower().Equals(filtro.ToLower()) ||
                                     usuario.CPF.ToLower().Equals(filtro.ToLower()));
        }

        public async Task Insert(Gestor Gestor)
        {
            _context.Add(Gestor);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Gestor Gestor)
        {
            //Como é feita uma verificação antes, o Entity acaba rastreando a entidade, por isso é necessário faze-lo esse comando para desanexa-lo e realizar o update do novo objeto.
            var gestorNaMemoria = _context.Set<Gestor>().Find(Gestor.Id);

            if (gestorNaMemoria != null)
                _context.Entry(gestorNaMemoria).State = EntityState.Detached;

            _context.Update(Gestor);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Gestor Gestor)
        {
            _context.Remove(Gestor);
            await _context.SaveChangesAsync();
        }
    }
}
