using DespesaViagem.Shared.Models.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespesaViagem.Shared.DTOs.Helpers
{
    public class AdminManutencaoDTO
    {
        public List<FuncionarioDTO> Funcionarios { get; set; } = new();
        public List<GestorDTO> Gestores { get; set; } = new();
    }
}
