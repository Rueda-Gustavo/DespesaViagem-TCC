using AutoMapper;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Server.Mapping
{
    public static class MappingDTOs
    {

        public static List<DespesaDTO> ConverterDTO(List<Despesa> despesas)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Despesa, DespesaDTO>();                
            });

            Mapper mapper = new(config);

            List<DespesaDTO> despesasDTO = new();

            foreach (var despesa in despesas)
            {
                despesasDTO.Add(mapper.Map<DespesaDTO>(despesa));
            }

            return despesasDTO;
        }
        public static DespesaHospedagem ConverterDTO(DespesaHospedagemDTO despesaHospedagemDTO)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaHospedagemDTO, DespesaHospedagem>()
                .ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())
                .ForMember(dst => dst.Viagem, opt => opt.Ignore())
                .ForMember(dst => dst.Endereco, opt => opt.Ignore())
                .ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            DespesaHospedagem despesa = mapper.Map<DespesaHospedagem>(despesaHospedagemDTO);
            
            despesa.CalcularTotalDespesa();

            return despesa;
        }
        public static DespesaPassagem ConverterDTO(DespesaPassagemDTO despesaPassagemDTO)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaPassagemDTO, DespesaPassagem>()
                .ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())
                .ForMember(dst => dst.Viagem, opt => opt.Ignore())                 
                .ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            DespesaPassagem despesa = mapper.Map<DespesaPassagem>(despesaPassagemDTO);

            despesa.CalcularTotalDespesa();

            return despesa;
        }

        public static DespesaAlimentacao ConverterDTO(DespesaAlimentacaoDTO despesaAlimentacaoDTO)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaAlimentacaoDTO, DespesaAlimentacao>()
                .ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())
                .ForMember(dst => dst.Viagem, opt => opt.Ignore())
                .ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            DespesaAlimentacao despesa = mapper.Map<DespesaAlimentacao>(despesaAlimentacaoDTO);

            despesa.CalcularTotalDespesa();

            return despesa;
        }       

        public static DespesaDeslocamento ConverterDTO(DespesaDeslocamentoDTO despesaDeslocamentoDTO)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaDeslocamentoDTO, DespesaDeslocamento>()
                .ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())
                .ForMember(dst => dst.Viagem, opt => opt.Ignore())
                .ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            DespesaDeslocamento despesa = mapper.Map<DespesaDeslocamento>(despesaDeslocamentoDTO);

            despesa.CalcularTotalDespesa();

            return despesa;
        }

        public static Viagem ConverterDTO(ViagemDTO viagemDTO)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<ViagemDTO, Viagem>()
                .ForMember(dst => dst.Funcionario, opt => opt.Ignore())
                .ForMember(dst => dst.Despesas, opt => opt.Ignore());
            });

            Mapper mapper = new(config);

            Viagem viagem = mapper.Map<Viagem>(viagemDTO);

            return viagem;
        }

        public static ViagemDTO ConverterDTO(Viagem viagem)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Viagem, ViagemDTO>()
                .ForMember(dst => dst.Funcionario, opt => opt.Ignore())
                .ForMember(dst => dst.StatusViagem, opt => opt.MapFrom(src => src.StatusViagem.ToString()));

            });

            Mapper mapper = new(config);

            ViagemDTO viagemDTO = mapper.Map<ViagemDTO>(viagem);

            return viagemDTO;
        }


        public static List<ViagemDTO> ConverterDTO(List<Viagem> viagens)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Viagem, ViagemDTO>()
                .ForMember(dst => dst.StatusViagem, opt => opt.MapFrom(src => src.StatusViagem.ToString()));
            });

            Mapper mapper = new(config);

            List<ViagemDTO> viagensDTO = new();

            foreach (var viagem in viagens)
            {
                viagensDTO.Add(mapper.Map<ViagemDTO>(viagem));
            }

            return viagensDTO;
        }
    }
}
