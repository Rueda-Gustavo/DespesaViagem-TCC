using DespesaViagem.Infra.Database.EntityConfiguration.DespesasEntityTypeConfiguration;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DespesaViagem.Infra.Database
{
    public class DespesaViagemContext : DbContext
    {
        public DbSet<Viagem> Viagens { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<DespesaHospedagem> DespesasHospedagens { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DespesaViagemContext(DbContextOptions<DespesaViagemContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despesa>().HasKey(k => k.Id);

            modelBuilder.Entity<DespesaHospedagem>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<Endereco>().HasKey(k => k.Id);
            
            modelBuilder.Entity<Funcionario>().HasKey(k => k.Id);
            
            modelBuilder.Entity<Viagem>().HasKey(k => k.Id);

            modelBuilder.ApplyConfiguration(new DespesaHospedagemEntityTypeConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
