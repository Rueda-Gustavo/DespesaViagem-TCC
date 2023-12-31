﻿using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using Microsoft.JSInterop;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IRelatoriosService
    {
        //Relatorio geral
        void GerarRelatorioGeralExcel(IJSRuntime jsRuntime, List<ViagemDTO> viagens, List<DespesaDTO> despesas, string nomeRelatorio); 
        //Relatorio viagens
        void GerarRelatorioViagensExcel(IJSRuntime jsRuntime, List<ViagemDTO> viagens, List<DespesaDTO> despesas, string nomeRelatorio);
        //Relatorio despesas        
        void GerarRelatorioDespesasExcel(IJSRuntime jsRuntime, List<ViagemDTO> viagens, DespesasDTO despesasDTO, bool todosTiposDespesaSelecionado, string nomeRelatorio);
    }
}
