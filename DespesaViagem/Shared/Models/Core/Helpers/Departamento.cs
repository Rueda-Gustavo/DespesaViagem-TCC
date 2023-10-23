using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class Departamento
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
