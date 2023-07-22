﻿using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaViagem.Infra.Database.EntityConfiguration.DespesasEntityTypeConfiguration
{
    public class DespesaHospedagemEntityTypeConfiguration : IEntityTypeConfiguration<DespesaHospedagem>
    {
        public void Configure(EntityTypeBuilder<DespesaHospedagem> builder)
        {
            builder.ToTable($"DespesasHospedagem");

            builder.Property(p => p.QuantidadeDias)
                   .HasColumnType("integer")
                   .IsRequired(true);

            builder.Property(p => p.ValorDiaria)
                   .HasColumnType("decimal")
                   .IsRequired(true);

            builder
                   .Property(p => p.TipoDespesa)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder
                    .HasOne(e => e.Endereco)
                    .WithMany(d => d.DespesasHospedagem)
                    .HasForeignKey(d => d.IdEndereco);
        }
    }
}