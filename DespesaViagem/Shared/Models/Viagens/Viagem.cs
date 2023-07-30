using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.Models.Viagens
{
    public class Viagem
    {
        public int Id { get; private set; } = 0;
        [Column(TypeName = "varchar(200)")]
        public string NomeViagem { get; set; } = string.Empty;
        [Column(TypeName = "varchar(3000)")]
        public string DescricaoViagem { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Adiantamento { get; private set; }
        public DateTime DataInicial { get; private set; }
        public DateTime DataFinal { get; private set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDespesas { get; private set; }
        [Column(TypeName = "varchar(20)")]
        public StatusViagem StatusViagem { get; private set; }
        public IReadOnlyCollection<Despesa> Despesas
        {
            get { return _despesas.AsReadOnly(); }
        }
        public Funcionario Funcionario { get; private set; }
        [JsonIgnore]
        public int IdFuncionario { get; private set; }


        private readonly List<Despesa> _despesas = new();

        public Viagem(int id, string nomeViagem, string descricaoViagem, decimal adiantamento, StatusViagem statusViagem, DateTime dataInicial, DateTime dataFinal, Funcionario funcionario)
        {
            Id = id;
            NomeViagem = nomeViagem;
            DescricaoViagem = descricaoViagem;
            Adiantamento = adiantamento;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            Adiantamento = adiantamento;
            TotalDespesas = 0;
            StatusViagem = statusViagem;
            Funcionario = funcionario;
        }

        public Viagem()
        {
            Funcionario = new();
        }

        public void AdicionarDespesa(Despesa despesa)
        {
            if (despesa.TotalDespesa > 0 && StatusViagem == StatusViagem.EmAndamento)
                TotalDespesas += despesa.TotalDespesa;
            _despesas.Add(despesa);
        }

        public void AdicionarDespesas(IEnumerable<Despesa> despesas)
        {
            if (StatusViagem == StatusViagem.EmAndamento)
            {
                foreach (var despesa in despesas)
                {
                    if (despesa.TotalDespesa > 0)
                    {
                        TotalDespesas += despesa.TotalDespesa;
                        _despesas.Add(despesa);
                    }
                }
            }
        }

        public void RemoverDespesa(int id)
        {
            if (id > 0)
            {
                Despesa? despesa = BuscarDespesa(id);
                if (despesa != null)
                {
                    TotalDespesas -= despesa.TotalDespesa;
                    _despesas.Remove(despesa);
                    return;
                }
                throw new ArgumentException("A despesa não foi encontrada.");
            }
            throw new ArgumentException("Por favor, informe um id válido.");
        }

        public decimal GerarPrestacaoDeContas()
        {
            StatusViagem = StatusViagem.Encerrada;
            return TotalDespesas - Adiantamento;
        }

        public void IniciarViagem()
        {
            if (StatusViagem == StatusViagem.Encerrada || StatusViagem == StatusViagem.EmAndamento || StatusViagem == StatusViagem.Cancelada)
                throw new Exception("Viagem já foi fechada ou já está em andamento!");
            StatusViagem = StatusViagem.EmAndamento;
        }

        public void CancelarViagem()
        {
            StatusViagem = StatusViagem.Cancelada;
        }

        public void EncerrarViagem()
        {
            if (StatusViagem != StatusViagem.EmAndamento)
                throw new Exception("Viagem a viagem deve estar em andamento para ser encerrada!");
            StatusViagem = StatusViagem.Encerrada;
        }

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            if (funcionario.Id > 0 && (IdFuncionario != 0 || IdFuncionario != funcionario.Id))
            {
                Funcionario = funcionario;
                IdFuncionario = funcionario.Id;
            }
        }

        public void DefinirAdiantamento(decimal adiantamento)
        {
            if (adiantamento >= 0)
            {
                Adiantamento = adiantamento;
            }
        }

        public void DefinirStatusViagem(StatusViagem statusViagem)
        {
            StatusViagem = statusViagem;
        }

        public void VerificarDataInicialeFinal()
        {
            if (DataFinal < DataInicial.AddDays(1))
                throw new ArgumentException("Insira uma período válido, com pelo menos 24 horas de diferença entre as datas inicial e final. Por exemplo: Data inicial dia 01 e data final dia 02");
        }

        private Despesa? BuscarDespesa(int id)
        {
            foreach (var item in _despesas)
            {
                if (item.Id == id)
                    return item;
            }
            return default;
        }
    }
}
