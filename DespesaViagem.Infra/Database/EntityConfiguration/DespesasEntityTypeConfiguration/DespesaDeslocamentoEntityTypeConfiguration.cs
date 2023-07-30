using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaViagem.Infra.Database.EntityConfiguration.DespesasEntityTypeConfiguration
{
    public class DespesaDeslocamentoEntityTypeConfiguration : IEntityTypeConfiguration<DespesaDeslocamento>
    {
        public void Configure(EntityTypeBuilder<DespesaDeslocamento> builder)
        {
            builder.ToTable($"DespesasDeslocamento");

            builder.Property(p => p.Modelo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired(true);

            builder.Property(p => p.Placa)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.Quilometragem)
                   .HasColumnType("bigint")
                   .IsRequired(true);

            builder.Property(p => p.ValorPorQuilometro)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired(true);

            builder.Property(p => p.TipoDespesa)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired(true);
        }
    }
}
