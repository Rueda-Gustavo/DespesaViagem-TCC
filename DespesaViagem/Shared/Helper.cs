namespace DespesaViagem.Shared
{
    public static class Helper
    {
        /*
        private static bool JaInicializado = false;
        public static Dictionary<string, string> ApresentacaoDespesas { get; set; } = new();

        public static void InicializarApresentacaoDespesas()
        {
            if (!JaInicializado)
            {
                ApresentacaoDespesas.Add("QuantidadeDias", "Quantidade de dias");
                ApresentacaoDespesas.Add("ValorDiaria", "Valor da diária");
                ApresentacaoDespesas.Add("NomeEstabelecimento", "Nome do estabelecimento");
                ApresentacaoDespesas.Add("ValorRefeicao", "Valor da refeição");
                ApresentacaoDespesas.Add("ValorPorQuilometro", "Valor por quilômetro");
                ApresentacaoDespesas.Add("DataHoraEmbarque", "Data e hora do embarque");
            }
            JaInicializado = true;
        }
        
        public static bool ValorValido(string valor)
        {
            var camposQueNaoDevemSerMostradosOuRepetidos = new HashSet<string> { "Id", "NomeDespesa", "DescricaoDespesa", "TotalDespesa", "DataDespesa", "DataDeCadastro", "TipoDespesa", "IdViagem", "IdEndereco", "IdFuncionario", "DespesasHospedagem", "DespesasPassagem", "DespesasDeslocamento", "DespesasAlimentacao" };

            if (!camposQueNaoDevemSerMostradosOuRepetidos.Contains(valor))
            {
                return true;
            }
            return false;
        }        

        public void ApresentarNoConsole(dynamic Despesa)
        {
            var camposQueNaoDevemSerMostradosOuRepetidos = new HashSet<string> { "Id", "NomeDespesa", "DescricaoDespesa", "TotalDespesa", "DataDespesa", "DataDeCadastro", "TipoDespesa", "IdViagem", "IdEndereco", "IdFuncionario", "DespesasHospedagem" };

            foreach (var propriedade in Despesa.GetType().GetProperties())
            {
                if (!camposQueNaoDevemSerMostradosOuRepetidos.Contains(propriedade.Name))
                    Console.WriteLine($"{propriedade.Name}: {propriedade.GetValue(Despesa)}");
            }

            if (Despesa.TipoDespesa == TiposDespesas.Hospedagem && Endereco is not null)
            {
                Console.WriteLine("Endereço:");
                foreach (var propriedade in Endereco.GetType().GetProperties())
                {
                    if (!camposQueNaoDevemSerMostradosOuRepetidos.Contains(propriedade.Name))
                        Console.WriteLine($"{propriedade.Name}: {propriedade.GetValue(Endereco)}");
                }
            }
        }
        */
    }
}
