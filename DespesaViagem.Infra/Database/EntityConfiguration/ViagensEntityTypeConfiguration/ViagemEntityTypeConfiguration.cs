using DespesaViagem.Shared.Models.Viagens;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespesaViagem.Infra.Database.EntityConfiguration.ViagensEntityTypeConfiguration
{
    public class ViagemEntityTypeConfiguration : IEntityTypeConfiguration<Viagem>
    {
        public void Configure(EntityTypeBuilder<Viagem> builder)
        {
            builder.ToTable($"Viagens");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.NomeViagem)
                   .HasMaxLength(40)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.DescricaoViagem)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.Adiantamento)
                   .HasColumnType("decimal")
                   .HasPrecision(10, 2)
                   .IsRequired();

            builder.Property(p => p.TotalDespesas)
                   .HasColumnType("decimal")
                   .HasPrecision(10, 2)
                   .IsRequired();

            builder.Property(p => p.StatusViagem)
                   .HasColumnType("varchar")
                   .HasMaxLength(15)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Ignore(p => p.Despesas);

            builder
                .HasMany(d => d.Despesas)
                .WithOne(v => v.Viagem)
                .HasForeignKey(d => d.IdViagem);

            builder
                .HasOne(f => f.Funcionario)
                .WithMany(v => v.Viagens)
                .HasForeignKey(f => f.IdFuncionario);

        }
    }
}
