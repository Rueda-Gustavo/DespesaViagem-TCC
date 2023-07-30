using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class ServiceResponse<T>
    {
        public T? Dados { get; set; }
        public bool Sucesso { get; set; } = true;
        public string Mensagem { get; set; } = string.Empty;
    }
}
