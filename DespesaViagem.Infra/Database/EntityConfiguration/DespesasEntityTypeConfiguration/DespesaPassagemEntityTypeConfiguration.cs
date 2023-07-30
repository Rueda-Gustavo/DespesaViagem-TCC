using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaViagem.Infra.Database.EntityConfiguration.DespesasEntityTypeConfiguration
{
    public class DespesaPassagemEntityTypeConfiguration : IEntityTypeConfiguration<DespesaPassagem>
    {
        public void Configure(EntityTypeBuilder<DespesaPassagem> builder)
        {
            builder.ToTable($"DespesasPassagem");

            builder.Property(p => p.Companhia)
                   .HasMaxLength(40)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.Origem)
                   .HasMaxLength(40)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.Destino)
                   .HasMaxLength(40)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.DataHoraEmbarque)
                   .HasColumnType("datetime")
                   .IsRequired(true);

            builder.Property(p => p.Preco)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired(true);

            builder.Property(p => p.TipoDespesa)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired(true);
        }
    }
}
