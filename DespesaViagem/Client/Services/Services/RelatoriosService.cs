using DespesaViagem.Shared.DTOs.Viagens;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DespesaViagem.Client.Services.Services
{
    public class RelatoriosService
    {
        #region declaração
        int _maximoColunas = 7;
        Document _documento;
        PdfPTable _tabelaPdf = new(7);
        PdfPCell _celulaPdf;
        Font _fontStyle;
        MemoryStream _memoryStream = new();
        List<ViagemDTO> _viagens = new();
        #endregion

        public byte[] GerarRelatorio(List<ViagemDTO> viagens)
        {
            _viagens = viagens;

            _documento = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            _tabelaPdf.WidthPercentage = 100;
            _tabelaPdf.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_documento, _memoryStream);
            _documento.Open();

            float[] tamanhos = new float[_maximoColunas];
            for (int i = 0; i < _maximoColunas; i++)
            {
                if (i == 0) tamanhos[i] = 50;
                else tamanhos[i] = 100;
            }
            _tabelaPdf.SetWidths(tamanhos);

            this.CabecalhoDoRelatorioViagens();
            this.CorpoDoRelatorioViagens();

            _tabelaPdf.HeaderRows = 2;
            _documento.Add(_tabelaPdf);
            _documento.Close();

            return _memoryStream.ToArray();
        }

        private void CabecalhoDoRelatorioViagens()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);
            _celulaPdf = new PdfPCell(new Phrase("Relatório de viagens", _fontStyle));
            _celulaPdf.Colspan = _maximoColunas;
            _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.Border = 2;
            _celulaPdf.ExtraParagraphSpace = 0;            
            _tabelaPdf.AddCell(_celulaPdf);
            _tabelaPdf.CompleteRow();
        }

        private void CorpoDoRelatorioViagens()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            var fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);

            #region Cabeçalho da tabela
            _celulaPdf = new PdfPCell(new Phrase("Nome da viagem", _fontStyle));
            _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.BackgroundColor = BaseColor.Gray;
            _tabelaPdf.AddCell(_celulaPdf);

            _celulaPdf = new PdfPCell(new Phrase("Período", _fontStyle));
            _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.BackgroundColor = BaseColor.Gray;
            _tabelaPdf.AddCell(_celulaPdf);

            _celulaPdf = new PdfPCell(new Phrase("Status", _fontStyle));
            _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.BackgroundColor = BaseColor.Gray;
            _tabelaPdf.AddCell(_celulaPdf);

            _celulaPdf = new PdfPCell(new Phrase("Funcionario", _fontStyle));
            _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.BackgroundColor = BaseColor.Gray;
            _tabelaPdf.AddCell(_celulaPdf);

            _celulaPdf = new PdfPCell(new Phrase("Adiantamento", _fontStyle));
            _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.BackgroundColor = BaseColor.Gray;
            _tabelaPdf.AddCell(_celulaPdf);

            _celulaPdf = new PdfPCell(new Phrase("Total das despesas", _fontStyle));
            _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.BackgroundColor = BaseColor.Gray;
            _tabelaPdf.AddCell(_celulaPdf);

            _celulaPdf = new PdfPCell(new Phrase("Subtotal", _fontStyle));
            _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
            _celulaPdf.BackgroundColor = BaseColor.Gray;
            _tabelaPdf.AddCell(_celulaPdf);

            _tabelaPdf.CompleteRow();
            #endregion

            #region Corpo da tabela            
            foreach (var viagem in _viagens)
            {
                _celulaPdf = new PdfPCell(new Phrase(viagem.NomeViagem, fontStyle));
                _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.BackgroundColor = BaseColor.LightGray;
                _tabelaPdf.AddCell(_celulaPdf);

                _celulaPdf = new PdfPCell(new Phrase($"{viagem.DataInicial.ToString("dd/MM/yyyy")} - {viagem.DataFinal.ToString("dd/MM/yyyy")}", fontStyle));
                _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.BackgroundColor = BaseColor.White;
                _tabelaPdf.AddCell(_celulaPdf);

                string status = viagem.StatusViagem.ToString();
                if (viagem.StatusViagem == DespesaViagem.Shared.Models.Core.Enums.StatusViagem.EmAndamento) status = "Em andamento";

                _celulaPdf = new PdfPCell(new Phrase(status, fontStyle));
                _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.BackgroundColor = BaseColor.LightGray;
                _tabelaPdf.AddCell(_celulaPdf);

                _celulaPdf = new PdfPCell(new Phrase(viagem.Funcionario.Username, fontStyle));
                _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.BackgroundColor = BaseColor.White;
                _tabelaPdf.AddCell(_celulaPdf);

                _celulaPdf = new PdfPCell(new Phrase($"R${viagem.Adiantamento}", fontStyle));
                _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.BackgroundColor = BaseColor.LightGray;
                _tabelaPdf.AddCell(_celulaPdf);

                _celulaPdf = new PdfPCell(new Phrase($"R${viagem.TotalDespesas}", fontStyle));
                _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.BackgroundColor = BaseColor.White;
                _tabelaPdf.AddCell(_celulaPdf);

                decimal subTotal = (viagem.Adiantamento - viagem.TotalDespesas);
                string apresentacaoSubTotal = subTotal < 0 ? $"-R${subTotal*(-1)}" : $"R${subTotal}";
                
                _celulaPdf = new PdfPCell(new Phrase(apresentacaoSubTotal, fontStyle));
                _celulaPdf.HorizontalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.VerticalAlignment = Element.ALIGN_CENTER;
                _celulaPdf.BackgroundColor = BaseColor.LightGray;
                _tabelaPdf.AddCell(_celulaPdf);

                _tabelaPdf.CompleteRow();
            }
            #endregion
        }
    }
}
