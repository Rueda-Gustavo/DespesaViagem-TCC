using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DespesaViagem.Client.Services.Services
{
    public class RelatoriosService : IRelatoriosService
    {
        #region Métodos principais 
        //Relatório geral
        public void GerarRelatorioExcel(IJSRuntime jsRuntime, List<ViagemDTO> viagens, List<DespesaDTO> despesas, string nomeRelatorio)
        {
            byte[] conteudoArquivo;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet workSheetViagens = excelPackage.Workbook.Worksheets.Add("Viagens");
                ExcelWorksheet workSheetDespesas = excelPackage.Workbook.Worksheets.Add("Despesas");

                GerarWorkSheetViagens(ref workSheetViagens, viagens);
                GerarWorkSheetDespesasGenerico(ref workSheetDespesas, despesas, viagens);

                conteudoArquivo = excelPackage.GetAsByteArray();
            }

            jsRuntime.InvokeAsync<ViagemDTO>(
            "salvarRelatorioExcel",
            $"{nomeRelatorio}.xlsx",
            Convert.ToBase64String(conteudoArquivo));
        }

        //Relatório das viagens
        public void GerarRelatorioExcel(IJSRuntime jsRuntime, List<ViagemDTO> viagens, string nomeRelatorio)
        {
            byte[] conteudoArquivo;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet workSheetViagens = excelPackage.Workbook.Worksheets.Add("Viagens");
                //ExcelWorksheet workSheetDespesas = excelPackage.Workbook.Worksheets.Add("Despesas");

                GerarWorkSheetViagens(ref workSheetViagens, viagens);

                conteudoArquivo = excelPackage.GetAsByteArray();
            }

            jsRuntime.InvokeAsync<ViagemDTO>(
            "salvarRelatorioExcel",
            $"{nomeRelatorio}.xlsx",
            Convert.ToBase64String(conteudoArquivo));
        }

        //Relatório das despesas
        public void GerarRelatorioExcel(IJSRuntime jsRuntime, List<ViagemDTO> viagens, DespesasDTO despesasDTO, bool todosTiposDespesaSelecionado, string nomeRelatorio)
        {
            byte[] conteudoArquivo;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = GerarWorkSheetDespesas(viagens, despesasDTO, todosTiposDespesaSelecionado))
            {
                conteudoArquivo = excelPackage.GetAsByteArray();
            }

            jsRuntime.InvokeAsync<ViagemDTO>(
            "salvarRelatorioExcel",
            $"{nomeRelatorio}.xlsx",
            Convert.ToBase64String(conteudoArquivo));
        }
        #endregion

        #region Métodos que geram os WorkSheets do excel
        //Método que gera o relatório das viagens
        private static void GerarWorkSheetViagens(ref ExcelWorksheet workSheetViagens, List<ViagemDTO> viagens)
        {
            #region Cabeçalho da primeira linha viagens
            workSheetViagens.Cells[1, 1].Value = "Identificador da viagem";
            workSheetViagens.Cells[1, 1].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 1].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 2].Value = "Nome da viagem";
            workSheetViagens.Cells[1, 2].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 2].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 3].Value = "Descrição da viagem";
            workSheetViagens.Cells[1, 3].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 3].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 4].Value = "Status";
            workSheetViagens.Cells[1, 4].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 4].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 5].Value = "Funcionario";
            workSheetViagens.Cells[1, 5].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 5].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 6].Value = "Data inicial";
            workSheetViagens.Cells[1, 6].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 6].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 7].Value = "Data Final";
            workSheetViagens.Cells[1, 7].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 7].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 8].Value = "Adiantamento";
            workSheetViagens.Cells[1, 8].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 8].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 9].Value = "Total das Despesas";
            workSheetViagens.Cells[1, 9].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 9].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetViagens.Cells[1, 10].Value = "Subtotal";
            workSheetViagens.Cells[1, 10].Style.Font.Size = 12;
            workSheetViagens.Cells[1, 10].Style.Font.Bold = true;
            workSheetViagens.Cells[1, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            #endregion

            int lenViagens = viagens.Count;

            #region Corpo do relatório viagens
            for (int i = 0; i < lenViagens; i++)
            {
                int celula = i + 2;

                workSheetViagens.Cells[celula, 1].Value = viagens[i].Id;
                workSheetViagens.Cells[celula, 1].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 1].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                workSheetViagens.Cells[celula, 2].Value = viagens[i].NomeViagem;
                workSheetViagens.Cells[celula, 2].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 2].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                workSheetViagens.Cells[celula, 3].Value = viagens[i].DescricaoViagem;
                workSheetViagens.Cells[celula, 3].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 3].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                string status = viagens[i].StatusViagem == StatusViagem.EmAndamento ? "Em andamento" : viagens[i].StatusViagem.ToString();
                workSheetViagens.Cells[celula, 4].Value = status;
                workSheetViagens.Cells[celula, 4].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 4].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                workSheetViagens.Cells[celula, 5].Value = viagens[i].Funcionario.Username;
                workSheetViagens.Cells[celula, 5].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 5].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                workSheetViagens.Cells[celula, 6].Value = viagens[i].DataInicial;
                workSheetViagens.Cells[celula, 6].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                workSheetViagens.Cells[celula, 6].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                workSheetViagens.Cells[celula, 7].Value = viagens[i].DataFinal;
                workSheetViagens.Cells[celula, 7].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 7].Style.Numberformat.Format = "dd/MM/yyyy";
                workSheetViagens.Cells[celula, 7].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                workSheetViagens.Cells[celula, 8].Value = viagens[i].Adiantamento;
                workSheetViagens.Cells[celula, 8].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 8].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                workSheetViagens.Cells[celula, 8].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                workSheetViagens.Cells[celula, 9].Value = viagens[i].TotalDespesas;
                workSheetViagens.Cells[celula, 9].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 9].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                workSheetViagens.Cells[celula, 9].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                decimal subtotal = viagens[i].Adiantamento - viagens[i].TotalDespesas;
                workSheetViagens.Cells[celula, 10].Value = subtotal;
                workSheetViagens.Cells[celula, 10].Style.Font.Size = 12;
                workSheetViagens.Cells[celula, 10].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                workSheetViagens.Cells[celula, 10].Style.Font.Bold = false;
                workSheetViagens.Cells[celula, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            }
            #endregion

            workSheetViagens.Cells["C:Z"].AutoFitColumns();
        }

        //Método que gera o relatório com todas as despesas, porém usando a classe base, logo o relatório é mais genérico
        private static void GerarWorkSheetDespesasGenerico(ref ExcelWorksheet workSheetDespesas, List<DespesaDTO> despesas, List<ViagemDTO> viagens)
        {
            int lenViagens = viagens.Count;

            #region Cabeçalho da primeira linha despesas
            workSheetDespesas.Cells[1, 1].Value = "Identificador da viagem";
            workSheetDespesas.Cells[1, 1].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 1].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetDespesas.Cells[1, 2].Value = "Nome da viagem";
            workSheetDespesas.Cells[1, 2].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 2].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetDespesas.Cells[1, 3].Value = "Identificador da despesa";
            workSheetDespesas.Cells[1, 3].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 3].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetDespesas.Cells[1, 4].Value = "Nome da despesa";
            workSheetDespesas.Cells[1, 4].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 4].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetDespesas.Cells[1, 5].Value = "Descrição da despesa";
            workSheetDespesas.Cells[1, 5].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 5].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetDespesas.Cells[1, 6].Value = "Funcionario";
            workSheetDespesas.Cells[1, 6].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 6].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetDespesas.Cells[1, 7].Value = "Tipo da despesa";
            workSheetDespesas.Cells[1, 7].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 7].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetDespesas.Cells[1, 8].Value = "Data da despesa";
            workSheetDespesas.Cells[1, 8].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 8].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheetDespesas.Cells[1, 9].Value = "Total da despesa";
            workSheetDespesas.Cells[1, 9].Style.Font.Size = 12;
            workSheetDespesas.Cells[1, 9].Style.Font.Bold = true;
            workSheetDespesas.Cells[1, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            #endregion


            #region Corpo do relatório despesas
            int linha = 2;
            for (int i = 0; i < lenViagens; i++)
            {
                List<DespesaDTO> despesasViagem = despesas.Where(d => d.IdViagem == viagens[i].Id).ToList();

                foreach (DespesaDTO despesa in despesasViagem)
                {
                    workSheetDespesas.Cells[linha, 1].Value = viagens[i].Id;
                    workSheetDespesas.Cells[linha, 1].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 1].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheetDespesas.Cells[linha, 2].Value = viagens[i].NomeViagem;
                    workSheetDespesas.Cells[linha, 2].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 2].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheetDespesas.Cells[linha, 3].Value = despesa.Id;
                    workSheetDespesas.Cells[linha, 3].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 3].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheetDespesas.Cells[linha, 4].Value = despesa.NomeDespesa;
                    workSheetDespesas.Cells[linha, 4].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 4].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheetDespesas.Cells[linha, 5].Value = despesa.DescricaoDespesa;
                    workSheetDespesas.Cells[linha, 5].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 5].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheetDespesas.Cells[linha, 6].Value = viagens[i].Funcionario.Username;
                    workSheetDespesas.Cells[linha, 6].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 6].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheetDespesas.Cells[linha, 7].Value = despesa.TipoDespesa;
                    workSheetDespesas.Cells[linha, 7].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 7].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheetDespesas.Cells[linha, 8].Value = despesa.DataDespesa;
                    workSheetDespesas.Cells[linha, 8].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 8].Style.Numberformat.Format = "dd/MM/yyyy";
                    workSheetDespesas.Cells[linha, 8].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheetDespesas.Cells[linha, 9].Value = despesa.TotalDespesa;
                    workSheetDespesas.Cells[linha, 9].Style.Font.Size = 12;
                    workSheetDespesas.Cells[linha, 9].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheetDespesas.Cells[linha, 9].Style.Font.Bold = false;
                    workSheetDespesas.Cells[linha, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    linha++;
                }
            }
            #endregion 

            workSheetDespesas.Cells["F:Z"].AutoFitColumns();
        }

        private static ExcelPackage GerarWorkSheetDespesas(List<ViagemDTO> viagens, DespesasDTO despesasDTO, bool todosTiposDespesaSelecionado)
        {

            if (!todosTiposDespesaSelecionado)
            {
                ExcelPackage excelPackage = new();

                if (despesasDTO.DespesasAlimentacaoDTO.Count > 0)
                {
                    ExcelWorksheet workSheetDespesasAlimentacao = excelPackage.Workbook.Worksheets.Add("Alimentação");
                    GerarExcelWorksheetDespesas(ref workSheetDespesasAlimentacao, despesasDTO.DespesasAlimentacaoDTO, viagens);
                }

                if (despesasDTO.DespesasDeslocamentoDTO.Count > 0)
                {
                    ExcelWorksheet workSheetDespesasDeslocamento = excelPackage.Workbook.Worksheets.Add("Deslocamento");
                    GerarExcelWorksheetDespesas(ref workSheetDespesasDeslocamento, despesasDTO.DespesasDeslocamentoDTO, viagens);
                }

                if (despesasDTO.DespesasHospedagemDTO.Count > 0)
                {
                    ExcelWorksheet workSheetDespesasHospedagem = excelPackage.Workbook.Worksheets.Add("Hospedagem");
                    GerarExcelWorksheetDespesas(ref workSheetDespesasHospedagem, despesasDTO.DespesasHospedagemDTO, viagens);
                }

                if (despesasDTO.DespesasPassagemDTO.Count > 0)
                {
                    ExcelWorksheet workSheetDespesasPassagem = excelPackage.Workbook.Worksheets.Add("Passagem");
                    GerarExcelWorksheetDespesas(ref workSheetDespesasPassagem, despesasDTO.DespesasPassagemDTO, viagens);
                }

                return excelPackage;
            }
            else
            {
                ExcelPackage excelPackage = GerarExcelWorkSheetTodasDespesas(despesasDTO, viagens);
                return excelPackage;
            }
        }

        //Método que fica responsável por gerar o excel com todos os tipos de despesas
        private static ExcelPackage GerarExcelWorkSheetTodasDespesas(DespesasDTO despesasDTO, List<ViagemDTO> viagens)
        {
            ExcelPackage excelPackage = new();

            ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.Add("Alimentação");
            GerarExcelWorksheetDespesas(ref workSheet, despesasDTO.DespesasAlimentacaoDTO, viagens);

            ExcelWorksheet workSheetDespesasDeslocamento = excelPackage.Workbook.Worksheets.Add("Deslocamento");
            GerarExcelWorksheetDespesas(ref workSheetDespesasDeslocamento, despesasDTO.DespesasDeslocamentoDTO, viagens);

            ExcelWorksheet workSheetDespesasHospedagem = excelPackage.Workbook.Worksheets.Add("Hospedagem");
            GerarExcelWorksheetDespesas(ref workSheetDespesasHospedagem, despesasDTO.DespesasHospedagemDTO, viagens);

            ExcelWorksheet workSheetDespesasPassagem = excelPackage.Workbook.Worksheets.Add("Passagem");
            GerarExcelWorksheetDespesas(ref workSheetDespesasPassagem, despesasDTO.DespesasPassagemDTO, viagens);

            return excelPackage;
        }

        private static void GerarExcelWorksheetDespesas(ref ExcelWorksheet workSheet, List<DespesaAlimentacaoDTO> despesas, List<ViagemDTO> viagens)
        {
            if (despesas.Count == 0)
                return;
            int lenViagens = viagens.Count;

            #region Cabeçalho relatório despesas alimentacao
            workSheet.Cells[1, 1].Value = "Identificador da viagem";
            workSheet.Cells[1, 1].Style.Font.Size = 12;
            workSheet.Cells[1, 1].Style.Font.Bold = true;
            workSheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 2].Value = "Nome da viagem";
            workSheet.Cells[1, 2].Style.Font.Size = 12;
            workSheet.Cells[1, 2].Style.Font.Bold = true;
            workSheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 3].Value = "Identificador da despesa";
            workSheet.Cells[1, 3].Style.Font.Size = 12;
            workSheet.Cells[1, 3].Style.Font.Bold = true;
            workSheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 4].Value = "Nome da despesa";
            workSheet.Cells[1, 4].Style.Font.Size = 12;
            workSheet.Cells[1, 4].Style.Font.Bold = true;
            workSheet.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 5].Value = "Descrição da despesa";
            workSheet.Cells[1, 5].Style.Font.Size = 12;
            workSheet.Cells[1, 5].Style.Font.Bold = true;
            workSheet.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 6].Value = "Funcionario";
            workSheet.Cells[1, 6].Style.Font.Size = 12;
            workSheet.Cells[1, 6].Style.Font.Bold = true;
            workSheet.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 7].Value = "Data da despesa";
            workSheet.Cells[1, 7].Style.Font.Size = 12;
            workSheet.Cells[1, 7].Style.Font.Bold = true;
            workSheet.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 8].Value = "Nome do estabelecimento";
            workSheet.Cells[1, 8].Style.Font.Size = 12;
            workSheet.Cells[1, 8].Style.Font.Bold = true;
            workSheet.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 9].Value = "CNPJ";
            workSheet.Cells[1, 9].Style.Font.Size = 12;
            workSheet.Cells[1, 9].Style.Font.Bold = true;
            workSheet.Cells[1, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 10].Value = "Valor da refeição";
            workSheet.Cells[1, 10].Style.Font.Size = 12;
            workSheet.Cells[1, 10].Style.Font.Bold = true;
            workSheet.Cells[1, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 11].Value = "Total da despesa";
            workSheet.Cells[1, 11].Style.Font.Size = 12;
            workSheet.Cells[1, 11].Style.Font.Bold = true;
            workSheet.Cells[1, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            #endregion


            #region Corpo do relatório despesas
            int linha = 2;
            for (int i = 0; i < lenViagens; i++)
            {
                List<DespesaAlimentacaoDTO> despesasViagem = despesas.Where(d => d.IdViagem == viagens[i].Id).ToList();

                foreach (var despesa in despesasViagem)
                {
                    workSheet.Cells[linha, 1].Value = viagens[i].Id;
                    workSheet.Cells[linha, 1].Style.Font.Size = 12;
                    workSheet.Cells[linha, 1].Style.Font.Bold = false;
                    workSheet.Cells[linha, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 2].Value = viagens[i].NomeViagem;
                    workSheet.Cells[linha, 2].Style.Font.Size = 12;
                    workSheet.Cells[linha, 2].Style.Font.Bold = false;
                    workSheet.Cells[linha, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 3].Value = despesa.Id;
                    workSheet.Cells[linha, 3].Style.Font.Size = 12;
                    workSheet.Cells[linha, 3].Style.Font.Bold = false;
                    workSheet.Cells[linha, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 4].Value = despesa.NomeDespesa;
                    workSheet.Cells[linha, 4].Style.Font.Size = 12;
                    workSheet.Cells[linha, 4].Style.Font.Bold = false;
                    workSheet.Cells[linha, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 5].Value = despesa.DescricaoDespesa;
                    workSheet.Cells[linha, 5].Style.Font.Size = 12;
                    workSheet.Cells[linha, 5].Style.Font.Bold = false;
                    workSheet.Cells[linha, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 6].Value = viagens[i].Funcionario.Username;
                    workSheet.Cells[linha, 6].Style.Font.Size = 12;
                    workSheet.Cells[linha, 6].Style.Font.Bold = false;
                    workSheet.Cells[linha, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 7].Value = despesa.DataDespesa;
                    workSheet.Cells[linha, 7].Style.Font.Size = 12;
                    workSheet.Cells[linha, 7].Style.Numberformat.Format = "dd/MM/yyyy";
                    workSheet.Cells[linha, 7].Style.Font.Bold = false;
                    workSheet.Cells[linha, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 8].Value = despesa.NomeEstabelecimento;
                    workSheet.Cells[linha, 8].Style.Font.Size = 12;
                    workSheet.Cells[linha, 8].Style.Font.Bold = false;
                    workSheet.Cells[linha, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 9].Value = despesa.CNPJ;
                    workSheet.Cells[linha, 9].Style.Font.Size = 12;
                    workSheet.Cells[linha, 9].Style.Font.Bold = false;
                    workSheet.Cells[linha, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 10].Value = despesa.ValorRefeicao;
                    workSheet.Cells[linha, 10].Style.Font.Size = 12;
                    workSheet.Cells[linha, 10].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheet.Cells[linha, 10].Style.Font.Bold = false;
                    workSheet.Cells[linha, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 11].Value = despesa.TotalDespesa;
                    workSheet.Cells[linha, 11].Style.Font.Size = 12;
                    workSheet.Cells[linha, 11].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheet.Cells[linha, 11].Style.Font.Bold = false;
                    workSheet.Cells[linha, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    linha++;
                }
            }
            #endregion

            workSheet.Cells["F:Z"].AutoFitColumns();
        }

        private static void GerarExcelWorksheetDespesas(ref ExcelWorksheet workSheet, List<DespesaDeslocamentoDTO> despesas, List<ViagemDTO> viagens)
        {
            if (despesas.Count == 0)
                return;//throw new ArgumentNullException("Nenhuma despesa localizada.");
            int lenViagens = viagens.Count;

            #region Cabeçalho relatório despesas com hospedagem
            workSheet.Cells[1, 1].Value = "Identificador da viagem";
            workSheet.Cells[1, 1].Style.Font.Size = 12;
            workSheet.Cells[1, 1].Style.Font.Bold = true;
            workSheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 2].Value = "Nome da viagem";
            workSheet.Cells[1, 2].Style.Font.Size = 12;
            workSheet.Cells[1, 2].Style.Font.Bold = true;
            workSheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 3].Value = "Identificador da despesa";
            workSheet.Cells[1, 3].Style.Font.Size = 12;
            workSheet.Cells[1, 3].Style.Font.Bold = true;
            workSheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 4].Value = "Nome da despesa";
            workSheet.Cells[1, 4].Style.Font.Size = 12;
            workSheet.Cells[1, 4].Style.Font.Bold = true;
            workSheet.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 5].Value = "Descrição da despesa";
            workSheet.Cells[1, 5].Style.Font.Size = 12;
            workSheet.Cells[1, 5].Style.Font.Bold = true;
            workSheet.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 6].Value = "Funcionario";
            workSheet.Cells[1, 6].Style.Font.Size = 12;
            workSheet.Cells[1, 6].Style.Font.Bold = true;
            workSheet.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 7].Value = "Data da despesa";
            workSheet.Cells[1, 7].Style.Font.Size = 12;
            workSheet.Cells[1, 7].Style.Font.Bold = true;
            workSheet.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 8].Value = "Placa";
            workSheet.Cells[1, 8].Style.Font.Size = 12;
            workSheet.Cells[1, 8].Style.Font.Bold = true;
            workSheet.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 9].Value = "Modelo";
            workSheet.Cells[1, 9].Style.Font.Size = 12;
            workSheet.Cells[1, 9].Style.Font.Bold = true;
            workSheet.Cells[1, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 10].Value = "Distância (Km)";
            workSheet.Cells[1, 10].Style.Font.Size = 12;
            workSheet.Cells[1, 10].Style.Font.Bold = true;
            workSheet.Cells[1, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 11].Value = "Valor por Km";
            workSheet.Cells[1, 11].Style.Font.Size = 12;
            workSheet.Cells[1, 11].Style.Font.Bold = true;
            workSheet.Cells[1, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 12].Value = "Total da despesa";
            workSheet.Cells[1, 12].Style.Font.Size = 12;
            workSheet.Cells[1, 12].Style.Font.Bold = true;
            workSheet.Cells[1, 12].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            #endregion


            #region Corpo do relatório despesas
            int linha = 2;
            for (int i = 0; i < lenViagens; i++)
            {
                List<DespesaDeslocamentoDTO> despesasViagem = despesas.Where(d => d.IdViagem == viagens[i].Id).ToList();

                foreach (var despesa in despesasViagem)
                {
                    workSheet.Cells[linha, 1].Value = viagens[i].Id;
                    workSheet.Cells[linha, 1].Style.Font.Size = 12;
                    workSheet.Cells[linha, 1].Style.Font.Bold = false;
                    workSheet.Cells[linha, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 2].Value = viagens[i].NomeViagem;
                    workSheet.Cells[linha, 2].Style.Font.Size = 12;
                    workSheet.Cells[linha, 2].Style.Font.Bold = false;
                    workSheet.Cells[linha, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 3].Value = despesa.Id;
                    workSheet.Cells[linha, 3].Style.Font.Size = 12;
                    workSheet.Cells[linha, 3].Style.Font.Bold = false;
                    workSheet.Cells[linha, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 4].Value = despesa.NomeDespesa;
                    workSheet.Cells[linha, 4].Style.Font.Size = 12;
                    workSheet.Cells[linha, 4].Style.Font.Bold = false;
                    workSheet.Cells[linha, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 5].Value = despesa.DescricaoDespesa;
                    workSheet.Cells[linha, 5].Style.Font.Size = 12;
                    workSheet.Cells[linha, 5].Style.Font.Bold = false;
                    workSheet.Cells[linha, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 6].Value = viagens[i].Funcionario.Username;
                    workSheet.Cells[linha, 6].Style.Font.Size = 12;
                    workSheet.Cells[linha, 6].Style.Font.Bold = false;
                    workSheet.Cells[linha, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 7].Value = despesa.DataDespesa;
                    workSheet.Cells[linha, 7].Style.Font.Size = 12;
                    workSheet.Cells[linha, 7].Style.Numberformat.Format = "dd/MM/yyyy";
                    workSheet.Cells[linha, 7].Style.Font.Bold = false;
                    workSheet.Cells[linha, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 8].Value = despesa.Placa;
                    workSheet.Cells[linha, 8].Style.Font.Size = 12;
                    workSheet.Cells[linha, 8].Style.Font.Bold = false;
                    workSheet.Cells[linha, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 9].Value = despesa.Modelo;
                    workSheet.Cells[linha, 9].Style.Font.Size = 12;
                    workSheet.Cells[linha, 9].Style.Font.Bold = false;
                    workSheet.Cells[linha, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 10].Value = despesa.Quilometragem;
                    workSheet.Cells[linha, 10].Style.Font.Size = 12;
                    workSheet.Cells[linha, 10].Style.Font.Bold = false;
                    workSheet.Cells[linha, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 11].Value = despesa.ValorPorQuilometro;
                    workSheet.Cells[linha, 11].Style.Font.Size = 12;
                    workSheet.Cells[linha, 11].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheet.Cells[linha, 11].Style.Font.Bold = false;
                    workSheet.Cells[linha, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 12].Value = despesa.TotalDespesa;
                    workSheet.Cells[linha, 12].Style.Font.Size = 12;
                    workSheet.Cells[linha, 12].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheet.Cells[linha, 12].Style.Font.Bold = false;
                    workSheet.Cells[linha, 12].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    linha++;
                }
            }
            #endregion

            workSheet.Cells["F:Z"].AutoFitColumns();
        }

        private static void GerarExcelWorksheetDespesas(ref ExcelWorksheet workSheet, List<DespesaHospedagemDTO> despesas, List<ViagemDTO> viagens)
        {
            if (despesas.Count == 0)
                return; //throw new ArgumentNullException("Nenhuma despesa localizada.");
            int lenViagens = viagens.Count;

            #region Cabeçalho relatório despesas com hospedagem
            workSheet.Cells[1, 1].Value = "Identificador da viagem";
            workSheet.Cells[1, 1].Style.Font.Size = 12;
            workSheet.Cells[1, 1].Style.Font.Bold = true;
            workSheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 2].Value = "Nome da viagem";
            workSheet.Cells[1, 2].Style.Font.Size = 12;
            workSheet.Cells[1, 2].Style.Font.Bold = true;
            workSheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 3].Value = "Identificador da despesa";
            workSheet.Cells[1, 3].Style.Font.Size = 12;
            workSheet.Cells[1, 3].Style.Font.Bold = true;
            workSheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 4].Value = "Nome da despesa";
            workSheet.Cells[1, 4].Style.Font.Size = 12;
            workSheet.Cells[1, 4].Style.Font.Bold = true;
            workSheet.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 5].Value = "Descrição da despesa";
            workSheet.Cells[1, 5].Style.Font.Size = 12;
            workSheet.Cells[1, 5].Style.Font.Bold = true;
            workSheet.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 6].Value = "Funcionario";
            workSheet.Cells[1, 6].Style.Font.Size = 12;
            workSheet.Cells[1, 6].Style.Font.Bold = true;
            workSheet.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 7].Value = "Data da despesa";
            workSheet.Cells[1, 7].Style.Font.Size = 12;
            workSheet.Cells[1, 7].Style.Font.Bold = true;
            workSheet.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 8].Value = "Logradouro";
            workSheet.Cells[1, 8].Style.Font.Size = 12;
            workSheet.Cells[1, 8].Style.Font.Bold = true;
            workSheet.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 9].Value = "Nº Casa";
            workSheet.Cells[1, 9].Style.Font.Size = 12;
            workSheet.Cells[1, 9].Style.Font.Bold = true;
            workSheet.Cells[1, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 10].Value = "Cidade";
            workSheet.Cells[1, 10].Style.Font.Size = 12;
            workSheet.Cells[1, 10].Style.Font.Bold = true;
            workSheet.Cells[1, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 11].Value = "Estado";
            workSheet.Cells[1, 11].Style.Font.Size = 12;
            workSheet.Cells[1, 11].Style.Font.Bold = true;
            workSheet.Cells[1, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 12].Value = "CEP";
            workSheet.Cells[1, 12].Style.Font.Size = 12;
            workSheet.Cells[1, 12].Style.Font.Bold = true;
            workSheet.Cells[1, 12].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 13].Value = "Quantidade de dias";
            workSheet.Cells[1, 13].Style.Font.Size = 12;
            workSheet.Cells[1, 13].Style.Font.Bold = true;
            workSheet.Cells[1, 13].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 14].Value = "Diária (R$)";
            workSheet.Cells[1, 14].Style.Font.Size = 12;
            workSheet.Cells[1, 14].Style.Font.Bold = true;
            workSheet.Cells[1, 14].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 15].Value = "Total da despesa";
            workSheet.Cells[1, 15].Style.Font.Size = 12;
            workSheet.Cells[1, 15].Style.Font.Bold = true;
            workSheet.Cells[1, 15].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            #endregion


            #region Corpo do relatório despesas
            int linha = 2;
            for (int i = 0; i < lenViagens; i++)
            {
                List<DespesaHospedagemDTO> despesasViagem = despesas.Where(d => d.IdViagem == viagens[i].Id).ToList();

                foreach (var despesa in despesasViagem)
                {
                    workSheet.Cells[linha, 1].Value = viagens[i].Id;
                    workSheet.Cells[linha, 1].Style.Font.Size = 12;
                    workSheet.Cells[linha, 1].Style.Font.Bold = false;
                    workSheet.Cells[linha, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 2].Value = viagens[i].NomeViagem;
                    workSheet.Cells[linha, 2].Style.Font.Size = 12;
                    workSheet.Cells[linha, 2].Style.Font.Bold = false;
                    workSheet.Cells[linha, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 3].Value = despesa.Id;
                    workSheet.Cells[linha, 3].Style.Font.Size = 12;
                    workSheet.Cells[linha, 3].Style.Font.Bold = false;
                    workSheet.Cells[linha, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 4].Value = despesa.NomeDespesa;
                    workSheet.Cells[linha, 4].Style.Font.Size = 12;
                    workSheet.Cells[linha, 4].Style.Font.Bold = false;
                    workSheet.Cells[linha, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 5].Value = despesa.DescricaoDespesa;
                    workSheet.Cells[linha, 5].Style.Font.Size = 12;
                    workSheet.Cells[linha, 5].Style.Font.Bold = false;
                    workSheet.Cells[linha, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 6].Value = viagens[i].Funcionario.Username;
                    workSheet.Cells[linha, 6].Style.Font.Size = 12;
                    workSheet.Cells[linha, 6].Style.Font.Bold = false;
                    workSheet.Cells[linha, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 7].Value = despesa.DataDespesa;
                    workSheet.Cells[linha, 7].Style.Font.Size = 12;
                    workSheet.Cells[linha, 7].Style.Numberformat.Format = "dd/MM/yyyy";
                    workSheet.Cells[linha, 7].Style.Font.Bold = false;
                    workSheet.Cells[linha, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 8].Value = despesa.Logradouro;
                    workSheet.Cells[linha, 8].Style.Font.Size = 12;
                    workSheet.Cells[linha, 8].Style.Font.Bold = false;
                    workSheet.Cells[linha, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 9].Value = despesa.NumeroCasa;
                    workSheet.Cells[linha, 9].Style.Font.Size = 12;
                    workSheet.Cells[linha, 9].Style.Font.Bold = false;
                    workSheet.Cells[linha, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 10].Value = despesa.Cidade;
                    workSheet.Cells[linha, 10].Style.Font.Size = 12;
                    workSheet.Cells[linha, 10].Style.Font.Bold = false;
                    workSheet.Cells[linha, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 11].Value = despesa.Estado;
                    workSheet.Cells[linha, 11].Style.Font.Size = 12;
                    workSheet.Cells[linha, 11].Style.Font.Bold = false;
                    workSheet.Cells[linha, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 12].Value = despesa.CEP;
                    workSheet.Cells[linha, 12].Style.Font.Size = 12;
                    workSheet.Cells[linha, 12].Style.Font.Bold = false;
                    workSheet.Cells[linha, 12].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 13].Value = despesa.QuantidadeDias;
                    workSheet.Cells[linha, 13].Style.Font.Size = 12;
                    workSheet.Cells[linha, 13].Style.Font.Bold = false;
                    workSheet.Cells[linha, 13].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 14].Value = despesa.ValorDiaria;
                    workSheet.Cells[linha, 14].Style.Font.Size = 12;
                    workSheet.Cells[linha, 14].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheet.Cells[linha, 14].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    workSheet.Cells[linha, 14].Style.Font.Bold = false;

                    workSheet.Cells[linha, 15].Value = despesa.TotalDespesa;
                    workSheet.Cells[linha, 15].Style.Font.Size = 12;
                    workSheet.Cells[linha, 15].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheet.Cells[linha, 15].Style.Font.Bold = false;
                    workSheet.Cells[linha, 15].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    linha++;
                }
            }
            #endregion

            workSheet.Cells["F:Z"].AutoFitColumns();
        }

        private static void GerarExcelWorksheetDespesas(ref ExcelWorksheet workSheet, List<DespesaPassagemDTO> despesas, List<ViagemDTO> viagens)
        {
            if (despesas.Count == 0)
                return;
            int lenViagens = viagens.Count;

            #region Cabeçalho da primeira linha despesas
            workSheet.Cells[1, 1].Value = "Identificador da viagem";
            workSheet.Cells[1, 1].Style.Font.Size = 12;
            workSheet.Cells[1, 1].Style.Font.Bold = true;
            workSheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 2].Value = "Nome da viagem";
            workSheet.Cells[1, 2].Style.Font.Size = 12;
            workSheet.Cells[1, 2].Style.Font.Bold = true;
            workSheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 3].Value = "Identificador da despesa";
            workSheet.Cells[1, 3].Style.Font.Size = 12;
            workSheet.Cells[1, 3].Style.Font.Bold = true;
            workSheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 4].Value = "Nome da despesa";
            workSheet.Cells[1, 4].Style.Font.Size = 12;
            workSheet.Cells[1, 4].Style.Font.Bold = true;
            workSheet.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 5].Value = "Descrição da despesa";
            workSheet.Cells[1, 5].Style.Font.Size = 12;
            workSheet.Cells[1, 5].Style.Font.Bold = true;
            workSheet.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 6].Value = "Funcionario";
            workSheet.Cells[1, 6].Style.Font.Size = 12;
            workSheet.Cells[1, 6].Style.Font.Bold = true;
            workSheet.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 7].Value = "Data da despesa";
            workSheet.Cells[1, 7].Style.Font.Size = 12;
            workSheet.Cells[1, 7].Style.Font.Bold = true;
            workSheet.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 8].Value = "Companhia";
            workSheet.Cells[1, 8].Style.Font.Size = 12;
            workSheet.Cells[1, 8].Style.Font.Bold = true;
            workSheet.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 9].Value = "Origem";
            workSheet.Cells[1, 9].Style.Font.Size = 12;
            workSheet.Cells[1, 9].Style.Font.Bold = true;
            workSheet.Cells[1, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 10].Value = "Destino";
            workSheet.Cells[1, 10].Style.Font.Size = 12;
            workSheet.Cells[1, 10].Style.Font.Bold = true;
            workSheet.Cells[1, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 11].Value = "Embarque";
            workSheet.Cells[1, 11].Style.Font.Size = 12;
            workSheet.Cells[1, 11].Style.Font.Bold = true;
            workSheet.Cells[1, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 12].Value = "Passagem (R$)";
            workSheet.Cells[1, 12].Style.Font.Size = 12;
            workSheet.Cells[1, 12].Style.Font.Bold = true;
            workSheet.Cells[1, 12].Style.Border.Top.Style = ExcelBorderStyle.Hair;

            workSheet.Cells[1, 13].Value = "Total da despesa";
            workSheet.Cells[1, 13].Style.Font.Size = 12;
            workSheet.Cells[1, 13].Style.Font.Bold = true;
            workSheet.Cells[1, 13].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            #endregion


            #region Corpo do relatório despesas
            int linha = 2;
            for (int i = 0; i < lenViagens; i++)
            {
                List<DespesaPassagemDTO> despesasViagem = despesas.Where(d => d.IdViagem == viagens[i].Id).ToList();

                foreach (var despesa in despesasViagem)
                {
                    workSheet.Cells[linha, 1].Value = viagens[i].Id;
                    workSheet.Cells[linha, 1].Style.Font.Size = 12;
                    workSheet.Cells[linha, 1].Style.Font.Bold = false;
                    workSheet.Cells[linha, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 2].Value = viagens[i].NomeViagem;
                    workSheet.Cells[linha, 2].Style.Font.Size = 12;
                    workSheet.Cells[linha, 2].Style.Font.Bold = false;
                    workSheet.Cells[linha, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 3].Value = despesa.Id;
                    workSheet.Cells[linha, 3].Style.Font.Size = 12;
                    workSheet.Cells[linha, 3].Style.Font.Bold = false;
                    workSheet.Cells[linha, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 4].Value = despesa.NomeDespesa;
                    workSheet.Cells[linha, 4].Style.Font.Size = 12;
                    workSheet.Cells[linha, 4].Style.Font.Bold = false;
                    workSheet.Cells[linha, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 5].Value = despesa.DescricaoDespesa;
                    workSheet.Cells[linha, 5].Style.Font.Size = 12;
                    workSheet.Cells[linha, 5].Style.Font.Bold = false;
                    workSheet.Cells[linha, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 6].Value = viagens[i].Funcionario.Username;
                    workSheet.Cells[linha, 6].Style.Font.Size = 12;
                    workSheet.Cells[linha, 6].Style.Font.Bold = false;
                    workSheet.Cells[linha, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 7].Value = despesa.DataDespesa;
                    workSheet.Cells[linha, 7].Style.Font.Size = 12;
                    workSheet.Cells[linha, 7].Style.Numberformat.Format = "dd/MM/yyyy";
                    workSheet.Cells[linha, 7].Style.Font.Bold = false;
                    workSheet.Cells[linha, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 8].Value = despesa.Companhia;
                    workSheet.Cells[linha, 8].Style.Font.Size = 12;
                    workSheet.Cells[linha, 8].Style.Font.Bold = false;
                    workSheet.Cells[linha, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 9].Value = despesa.Origem;
                    workSheet.Cells[linha, 9].Style.Font.Size = 12;
                    workSheet.Cells[linha, 9].Style.Font.Bold = false;
                    workSheet.Cells[linha, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 10].Value = despesa.Destino;
                    workSheet.Cells[linha, 10].Style.Font.Size = 12;
                    workSheet.Cells[linha, 10].Style.Font.Bold = false;
                    workSheet.Cells[linha, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 11].Value = despesa.DataHoraEmbarque;
                    workSheet.Cells[linha, 11].Style.Font.Size = 12;
                    workSheet.Cells[linha, 11].Style.Numberformat.Format = "dd/MM/yyyy";
                    workSheet.Cells[linha, 11].Style.Font.Bold = false;
                    workSheet.Cells[linha, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 12].Value = despesa.Preco;
                    workSheet.Cells[linha, 12].Style.Font.Size = 12;
                    workSheet.Cells[linha, 12].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheet.Cells[linha, 12].Style.Font.Bold = false;
                    workSheet.Cells[linha, 12].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    workSheet.Cells[linha, 13].Value = despesa.TotalDespesa;
                    workSheet.Cells[linha, 13].Style.Font.Size = 12;
                    workSheet.Cells[linha, 13].Style.Numberformat.Format = "_(R$* #,##0.00_);_(R$* (#,##0.00);_(R$* \"-\"??_);_(@_)";
                    workSheet.Cells[linha, 13].Style.Font.Bold = false;
                    workSheet.Cells[linha, 13].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    linha++;
                }
            }
            #endregion

            workSheet.Cells["F:Z"].AutoFitColumns();
        }
        #endregion
    }
}
