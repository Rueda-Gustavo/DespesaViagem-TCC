using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class ViagemRepository : IViagemRepository 
    {
        private readonly DespesaViagemContext _context;

        public ViagemRepository(DespesaViagemContext context)
        => _context = context;


        public async Task<List<Viagem>> ObterTodos(int idFuncionario)
        {
            return await _context.Viagens
                .Where(v => v.IdFuncionario == idFuncionario)
                .Include(f => f.Funcionario)
                .Include(d => d.Despesas)
                .OrderByDescending(viagem => viagem.Id)
                .ToListAsync();
        }
        public async Task<Viagem> ObterPorId(int id)
        {
            return await _context.Viagens
                .Include(v => v.Despesas)
                .FirstOrDefaultAsync(viagem => viagem.Id == id);

            //    .Where(viagem => viagem.Id == id)
            //    .ToListAsync();
        }

        public async Task<List<Viagem?>> ObterViagemPorStatus(StatusViagem statusViagem)
        {
            return await _context.Viagens
                .Include(f => f.Funcionario)
                .Where(viagem => viagem.StatusViagem == statusViagem)
                .ToListAsync();
        }

        public async Task<List<Viagem>> ObterPorFiltro(string filtro)
        {
            _ = int.TryParse(filtro, out int id);
            return await _context.Viagens
                .Where(viagem => viagem.Id == id || viagem.NomeViagem.Contains(filtro) || viagem.DescricaoViagem.Contains(filtro))
                .ToListAsync();
        }

        public async Task<List<Despesa>> ObterTodasDepesas(int viagemId)
        {
            return await _context.Despesas
                .Where(despesa => despesa.Viagem.Id == viagemId)
                .OrderByDescending(despesa => despesa.Id)
                .ToListAsync();
        }

        public async Task<List<Despesa>> ObterDespesasPorTipo(int viagemId, TiposDespesas tipoDespesa)
        {
            List<Despesa> despesas = await _context.Despesas
               .Where(d => d.IdViagem == viagemId && d.TipoDespesa == tipoDespesa)
               .ToListAsync();

            return despesas;
        }
        /*
        public async Task<List<Despesa>> ObterDepesasPorPagina(int viagemId, int pagina, int despesasPorPagina)
        {
            List<Despesa> despesas = await _context.Despesas
                .Where(d => d.IdViagem == viagemId)
                .Skip((pagina - 1) * despesasPorPagina)
                .Take(despesasPorPagina)
                .ToListAsync();

            return despesas;
        }
        */

        public async Task<List<DespesaPorCategoria>> ObterTotalDasDespesasPorCategoria(int viagemId)
        {
            /*
            string sqlDespesasPorCategoria = @"
            SELECT TipoDespesa,SUM(Totaldespesa) 
            FROM Despesas 
            WHERE IdViagem = @viagemId";
            */ //Basicamente esse é o select que é feito na consulta LINQ abaixo

            var result = await _context.Despesas
            .Where(despesa => despesa.IdViagem == viagemId)
            .GroupBy(despesa => despesa.TipoDespesa)
            .Select(agrupamento => new DespesaPorCategoria
            {
                TipoDespesa = agrupamento.Key,
                TotalDespesa = agrupamento.Sum(despesa => despesa.TotalDespesa)
            })
            .ToListAsync();

            return result;
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
