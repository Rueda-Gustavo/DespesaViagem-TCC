using DespesaViagem.Infra.Database.EntityConfiguration.DespesasEntityTypeConfiguration;
using DespesaViagem.Infra.Database.EntityConfiguration.ViagensEntityTypeConfiguration;
using DespesaViagem.Shared.Models.Core.Enums;
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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DespesaHospedagem> DespesasHospedagem { get; set; }
        public DbSet<DespesaDeslocamento> DespesasDeslocamento { get; set; }
        public DbSet<DespesaAlimentacao> DespesasAlimentacao { get; set; }
        public DbSet<DespesaPassagem> DespesasPassagem { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Gestor> Gestores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DespesaViagemContext(DbContextOptions<DespesaViagemContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despesa>().HasKey(k => k.Id);

            modelBuilder.Entity<Despesa>()
                   .Property(p => p.DataDeCadastro)
                   .HasColumnType("datetime");

            modelBuilder.Entity<DespesaHospedagem>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<DespesaAlimentacao>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<DespesaDeslocamento>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<DespesaPassagem>()
                .HasBaseType<Despesa>();

            modelBuilder.Entity<Usuario>()
                .ToTable($"{nameof(Usuario)}s")
                .HasDiscriminator(u => u.TipoDeUsuario)
                .HasValue<Usuario>(RolesUsuario.Administrador)
                .HasValue<Funcionario>(RolesUsuario.Funcionario)
                .HasValue<Gestor>(RolesUsuario.Gestor);

            modelBuilder.Entity<Usuario>().HasKey(k => k.Id);

            modelBuilder.Entity<Usuario>()
                        .Property(u => u.CPF)
                        .IsRequired(false);

            modelBuilder.Entity<Usuario>()
                        .HasIndex(u => u.Username)
                        .IsUnique(true);                        

            modelBuilder.Entity<Endereco>().HasKey(k => k.Id);

            modelBuilder.Entity<Funcionario>()
                        .HasOne(f => f.Gestor)
                        .WithMany(g => g.Funcionarios)
                        //.HasForeignKey(f => f.IdGestor)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Departamento>()
                        .HasIndex(d => d.Descricao)
                        .IsUnique(true);

            modelBuilder.ApplyConfiguration(new DespesaHospedagemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DespesaAlimentacaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DespesaDeslocamentoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DespesaPassagemEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new ViagemEntityTypeConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
