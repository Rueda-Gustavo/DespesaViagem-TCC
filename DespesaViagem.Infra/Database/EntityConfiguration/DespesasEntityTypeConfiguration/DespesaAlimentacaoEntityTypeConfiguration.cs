using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaViagem.Infra.Database.EntityConfiguration.DespesasEntityTypeConfiguration
{
    public class DespesaAlimentacaoEntityTypeConfiguration : IEntityTypeConfiguration<DespesaAlimentacao>
    {
        public void Configure(EntityTypeBuilder<DespesaAlimentacao> builder)
        {
            builder.ToTable($"DespesasAlimentacao");

            builder.Property(p => p.CNPJ)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.NomeEstabelecimento)
                   .HasMaxLength(30)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.ValorRefeicao)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired(true);

            builder
                   .Property(p => p.TipoDespesa)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired(true);
        }
    }
}
