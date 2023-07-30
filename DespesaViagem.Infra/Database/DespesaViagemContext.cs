using DespesaViagem.Infra.Database.EntityConfiguration.DespesasEntityTypeConfiguration;
using DespesaViagem.Infra.Database.EntityConfiguration.ViagensEntityTypeConfiguration;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;
using Microsoft.EntityFrameworkCore;

namespace DespesaViagem.Infra.Database
{
    public class DespesaViagemContext : DbContext
    {
        public DbSet<Viagem> Viagens { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<DespesaHospedagem> DespesasHospedagem { get; set; }
        public DbSet<DespesaDeslocamento> DespesasDeslocamento { get; set; }
        public DbSet<DespesaAlimentacao> DespesasAlimentacao { get; set; }
        public DbSet<DespesaPassagem> DespesasPassagem { get; set; }


        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DespesaViagemContext(DbContextOptions<DespesaViagemContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despesa>().HasKey(k => k.Id);

            modelBuilder.Entity<DespesaHospedagem>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<DespesaAlimentacao>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<DespesaDeslocamento>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<DespesaPassagem>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<Endereco>().HasKey(k => k.Id);

            modelBuilder.Entity<Funcionario>().HasKey(k => k.Id);

            modelBuilder.Entity<Funcionario>().HasData(new Funcionario
            {
                Id = 1,
                Nome = "Gustavo",
                Sobrenome = "Rueda dos Reis",
                CPF = "321.123.321-12",
                Matricula = "A65SD1ASD"
            });

            modelBuilder.ApplyConfiguration(new DespesaHospedagemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DespesaAlimentacaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DespesaDeslocamentoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DespesaPassagemEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new ViagemEntityTypeConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
