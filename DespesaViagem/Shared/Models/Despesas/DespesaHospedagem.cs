using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaViagem.Shared.Models.Despesas
{
    public class DespesaHospedagem : Despesa
    {
        public int QuantidadeDias { get; private set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorDiaria { get; private set; }
        //wpublic int IdEndereco { get; private set; }
        //public Endereco? Endereco { get; private set; }
        [Column(TypeName = "varchar(1000)")]
        public string Logradouro { get; set; } = string.Empty;
        public int NumeroCasa { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string CEP { get; set; } = string.Empty;
        [Column(TypeName = "varchar(100)")]
        public string Cidade { get; set; } = string.Empty;
        [Column(TypeName = "varchar(100)")]
        public string Estado { get; set; } = string.Empty;
        public DespesaHospedagem(string nomeDespesa, string descricaoDespesa/*, Endereco endereco*/,string logradouro, int numeroCasa, string CEP, string cidade, string estado, int quantidadeDias, decimal valorDiaria, int idViagem)
            : base(nomeDespesa, descricaoDespesa, quantidadeDias * valorDiaria, TiposDespesas.Hospedagem, idViagem)
        {
            //Endereco = endereco;
            Logradouro = logradouro;
            NumeroCasa = numeroCasa;
            this.CEP = CEP;
            Cidade = cidade;
            Estado = estado;
            QuantidadeDias = quantidadeDias;
            ValorDiaria = valorDiaria;
        }
        public DespesaHospedagem() {}

        public void CalcularTotalDespesa()
        {
            if (QuantidadeDias >= 0 && ValorDiaria >= 0)
                TotalDespesa = QuantidadeDias * ValorDiaria;
        }
    }
}
