using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DespesaViagemContext _context;

        public AdminRepository(DespesaViagemContext context)
        {
            _context = context;
        }

        public async Task<AdminManutencao> ObterListaUsuarios()
        {
            AdminManutencao manutencao = new()
            {
                Funcionarios = await _context.Funcionarios
                    .Include(f => f.Gestor)                    
                    .ToListAsync(),       

                Gestores = await _context.Gestores
                    .Include(g => g.Funcionarios)
                    .ToListAsync()
            };

            return manutencao;
        }
    }
}
