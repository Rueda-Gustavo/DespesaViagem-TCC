using DespesaViagem.Shared.Models.Viagens;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaViagem.Shared.Models.Despesas
{
    public abstract class Despesa
    {
        [ForeignKey("Viagem")]
        public int Id { get; protected set; }
        [Column(TypeName = "varchar(30)")]
        public virtual string NomeDespesa { get; protected set; } = string.Empty;
        [Column(TypeName = "varchar(200)")]
        public virtual string DescricaoDespesa { get; protected set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public virtual decimal TotalDespesa { get; protected set; }
        public virtual DateTime DataDespesa { get; protected set; }
        public DateTime DataDeCadastro { get; } = DateTime.Now;        
        public string TipoDespesa { get; protected set; } = string.Empty;
        public int IdViagem { get; protected set; }
        public Viagem? Viagem { get; set; }

        public Despesa(string nomeDespesa, string descricaoDespesa, decimal totalDespesa, string tipoDespesa, int idViagem)
        {            
            NomeDespesa = nomeDespesa;
            DescricaoDespesa = descricaoDespesa;
            TotalDespesa = totalDespesa;
            DataDespesa = DateTime.UtcNow;
            TipoDespesa = tipoDespesa;            
        }

        public Despesa() { }
        //Abstract class - https://sourcemaking.com/refactoring/replace-conditional-with-polymorphism 
    }
}
