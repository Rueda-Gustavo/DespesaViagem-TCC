namespace DespesaViagem.Shared.DTOs.Despesas
{
    public class DespesasDTO
    {
        public DespesasDTO(List<DespesaAlimentacaoDTO> despesasAlimentacaoDTO, List<DespesaDeslocamentoDTO> despesasDeslocamentoDTO, List<DespesaHospedagemDTO> despesasHospedagemDTO, List<DespesaPassagemDTO> despesasPassagemDTO)
        {
            DespesasAlimentacaoDTO = despesasAlimentacaoDTO;
            DespesasDeslocamentoDTO = despesasDeslocamentoDTO;
            DespesasHospedagemDTO = despesasHospedagemDTO;
            DespesasPassagemDTO = despesasPassagemDTO;
        }

        public DespesasDTO()
        {
            DespesasAlimentacaoDTO = new();
            DespesasDeslocamentoDTO = new();
            DespesasHospedagemDTO = new();
            DespesasPassagemDTO = new();
        }

        public List<DespesaAlimentacaoDTO> DespesasAlimentacaoDTO { get; set; } = new();
        public List<DespesaDeslocamentoDTO> DespesasDeslocamentoDTO { get; set; } = new();
        public List<DespesaHospedagemDTO> DespesasHospedagemDTO { get; set; } = new();
        public List<DespesaPassagemDTO> DespesasPassagemDTO { get; set; } = new();
    }
}
