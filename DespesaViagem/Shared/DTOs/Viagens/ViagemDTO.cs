﻿using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using System.Text.Json.Serialization;

namespace DespesaViagem.Shared.DTOs.Viagens
{
    public class ViagemDTO
    {
        public int Id { get; set; } //= 0;
        public string NomeViagem { get; set; } = string.Empty;
        public string DescricaoViagem { get; set; } = string.Empty;
        public decimal Adiantamento { get; set; }
        public DateTime DataInicial { get; set; } = DateTime.UtcNow;
        public DateTime DataFinal { get; set; } = DateTime.UtcNow;
        public decimal TotalDespesas { get; set; }        
        public StatusViagem StatusViagem { get; set; }        
        public FuncionarioDTO Funcionario { get; set; } = new();
        public int IdFuncionario { get; set; }
    }
}
