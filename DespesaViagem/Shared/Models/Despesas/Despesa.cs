using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Viagens;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.Models.Despesas
{
    public abstract class Despesa
    {
        [ForeignKey("Viagem")]
        public int Id { get; protected set; }
        [Column(TypeName = "varchar(1000)")]
        public virtual string NomeDespesa { get; protected set; } = string.Empty;
        [Column(TypeName = "varchar(3000)")]
        public virtual string DescricaoDespesa { get; protected set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public virtual decimal TotalDespesa { get; protected set; }        
        public virtual DateTime DataDespesa { get; protected set; }
        [Column(TypeName = "datetime")]
        public DateTime DataDeCadastro { get; } = DateTime.Now;
        [Column(TypeName = "varchar(20)")]
        public TiposDespesas TipoDespesa { get; protected set; }
        public int IdViagem { get; protected set; }
        [JsonIgnore]
        public Viagem? Viagem { get; set; }

        public Despesa(string nomeDespesa, string descricaoDespesa, decimal totalDespesa, TiposDespesas tipoDespesa, int idViagem)
        {            
            NomeDespesa = nomeDespesa;
            DescricaoDespesa = descricaoDespesa;
            TotalDespesa = totalDespesa;
            DataDespesa = DateTime.UtcNow;
            TipoDespesa = tipoDespesa;    
            IdViagem = idViagem;
        }

        public Despesa() { }
        //Abstract class - https://sourcemaking.com/refactoring/replace-conditional-with-polymorphism 
    }
}
