﻿using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly DespesaViagemContext _context;

        public FuncionarioRepository(DespesaViagemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Funcionario>> ObterTodos()
        {
            return await _context.Funcionarios
                .Include(f => f.Gestor)
                .ToListAsync();
        }

        public async Task<Funcionario> ObterPorId(int id)
        {
            return await _context.Funcionarios
                .Include(f => f.Gestor)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Funcionario>> ObterFuncionariosPorGestor(int gestorId)
        {
            return await _context.Funcionarios
                .Include(f => f.Gestor)
                .Where(f => f.Gestor.Id == gestorId).ToListAsync();
        }

        public async Task<Funcionario> ObterPorCPF(string CPF)
        {
            return await _context.Funcionarios
                .Include(f => f.Gestor)
                .FirstOrDefaultAsync(f => f.CPF == CPF);
        }

        public async Task<IEnumerable<Funcionario>> ObterPorFiltro(string filtro)
        {
            return await _context.Funcionarios
            .Include(f => f.Gestor)
            .Where(funcionario => funcionario.CPF.ToLower().Contains(filtro.ToLower()) || 
                                  funcionario.NomeCompleto.ToLower().Contains(filtro.ToLower()) || 
                                  funcionario.Matricula.ToLower().Contains(filtro.ToLower()) ||
                                  funcionario.Username.ToLower().Contains(filtro.ToLower()))
            .ToListAsync();
        }

        public async Task<bool> FuncionarioJaExiste(string filtro)
        {
            return await _context.Funcionarios
                .AnyAsync(funcionario => funcionario.Matricula.ToLower().Equals(filtro.ToLower()));
        }

        public async Task Insert(Funcionario funcionario)
        {
            _context.Add(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Funcionario funcionario)
        {
            //Como é feita uma verificação antes, o Entity acaba rastreando a entidade, por isso é necessário faze-lo esse comando para desanexa-lo e realizar o update do novo objeto.
            var funcionarioNaMemoria = _context.Set<Funcionario>().Find(funcionario.Id);

            if (funcionarioNaMemoria != null)
                _context.Entry(funcionarioNaMemoria).State = EntityState.Detached;

            _context.Update(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Funcionario funcionario)
        {
            _context.Remove(funcionario);
            await _context.SaveChangesAsync();
        }
    }
}
