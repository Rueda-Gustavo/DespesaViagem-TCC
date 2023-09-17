using AutoMapper;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
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
            despesaHospedagemDTO.TipoDespesa = TiposDespesas.Hospedagem;
            
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaHospedagemDTO, DespesaHospedagem>()
                //.ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())
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

        public static DespesaHospedagemDTO ConverterDTO(DespesaHospedagem despesaHospedagem)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaHospedagem, DespesaHospedagemDTO>()
                //.ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())                
                /*.ForMember(dst => dst.TotalDespesa, opt => opt.Ignore())*/;
                ;
            });

            Mapper mapper = new(config);

            DespesaHospedagemDTO despesa = mapper.Map<DespesaHospedagemDTO>(despesaHospedagem);            

            return despesa;
        }

        public static List<DespesaHospedagemDTO> ConverterDTO(List<DespesaHospedagem> despesasHospedagem)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaHospedagem, DespesaHospedagemDTO>()
                /*.ForMember(dst => dst.StatusViagem, opt => opt.MapFrom(src => src.StatusViagem.ToString()))*/;
            });

            Mapper mapper = new(config);

            List<DespesaHospedagemDTO> despesasHospedagemDTO = new();

            foreach (var despesa in despesasHospedagem)
            {
                despesasHospedagemDTO.Add(mapper.Map<DespesaHospedagemDTO>(despesa));
            }

            return despesasHospedagemDTO;
        }

        public static DespesaPassagem ConverterDTO(DespesaPassagemDTO despesaPassagemDTO)
        {
            despesaPassagemDTO.TipoDespesa = TiposDespesas.Passagem;

            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaPassagemDTO, DespesaPassagem>()
                //.ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())
                .ForMember(dst => dst.Viagem, opt => opt.Ignore())                 
                .ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            DespesaPassagem despesa = mapper.Map<DespesaPassagem>(despesaPassagemDTO);

            despesa.CalcularTotalDespesa();

            return despesa;
        }

        public static DespesaPassagemDTO ConverterDTO(DespesaPassagem despesaPassagem)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaPassagem, DespesaPassagemDTO>();
                ;
            });

            Mapper mapper = new(config);

            DespesaPassagemDTO despesa = mapper.Map<DespesaPassagemDTO>(despesaPassagem);

            return despesa;
        }

        public static List<DespesaPassagemDTO> ConverterDTO(List<DespesaPassagem> despesasPassagem)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaPassagem, DespesaPassagemDTO>()
                /*.ForMember(dst => dst.StatusViagem, opt => opt.MapFrom(src => src.StatusViagem.ToString()))*/;
            });

            Mapper mapper = new(config);

            List<DespesaPassagemDTO> despesasPassagemDTO = new();

            foreach (var despesa in despesasPassagem)
            {
                despesasPassagemDTO.Add(mapper.Map<DespesaPassagemDTO>(despesa));
            }

            return despesasPassagemDTO;
        }

        public static DespesaAlimentacao ConverterDTO(DespesaAlimentacaoDTO despesaAlimentacaoDTO)
        {
            despesaAlimentacaoDTO.TipoDespesa = TiposDespesas.Alimentação;

            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaAlimentacaoDTO, DespesaAlimentacao>()
                //.ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())
                .ForMember(dst => dst.Viagem, opt => opt.Ignore())
                .ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            DespesaAlimentacao despesa = mapper.Map<DespesaAlimentacao>(despesaAlimentacaoDTO);

            despesa.CalcularTotalDespesa();

            return despesa;
        }

        public static DespesaAlimentacaoDTO ConverterDTO(DespesaAlimentacao despesaAlimentacao)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaAlimentacao, DespesaAlimentacaoDTO>();
                ;
            });

            Mapper mapper = new(config);

            DespesaAlimentacaoDTO despesa = mapper.Map<DespesaAlimentacaoDTO>(despesaAlimentacao);

            return despesa;
        }

        public static List<DespesaAlimentacaoDTO> ConverterDTO(List<DespesaAlimentacao> despesasAlimentacao)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaAlimentacao, DespesaAlimentacaoDTO>();
            });

            Mapper mapper = new(config);

            List<DespesaAlimentacaoDTO> despesasAlimentacaoDTO = new();

            foreach (var despesa in despesasAlimentacao)
            {
                despesasAlimentacaoDTO.Add(mapper.Map<DespesaAlimentacaoDTO>(despesa));
            }

            return despesasAlimentacaoDTO;
        }


        public static DespesaDeslocamento ConverterDTO(DespesaDeslocamentoDTO despesaDeslocamentoDTO)
        {
            despesaDeslocamentoDTO.TipoDespesa = TiposDespesas.Deslocamento;

            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaDeslocamentoDTO, DespesaDeslocamento>()
                //.ForMember(dst => dst.NomeDespesa, opt => opt.Ignore())
                .ForMember(dst => dst.Viagem, opt => opt.Ignore())
                .ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            DespesaDeslocamento despesa = mapper.Map<DespesaDeslocamento>(despesaDeslocamentoDTO);

            despesa.CalcularTotalDespesa();

            return despesa;
        }

        public static DespesaDeslocamentoDTO ConverterDTO(DespesaDeslocamento despesaDeslocamento)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaDeslocamento, DespesaDeslocamentoDTO>();
                ;
            });

            Mapper mapper = new(config);

            DespesaDeslocamentoDTO despesa = mapper.Map<DespesaDeslocamentoDTO>(despesaDeslocamento);

            return despesa;
        }

        public static List<DespesaDeslocamentoDTO> ConverterDTO(List<DespesaDeslocamento> despesasDeslocamento)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaDeslocamento, DespesaDeslocamentoDTO>();
            });

            Mapper mapper = new(config);

            List<DespesaDeslocamentoDTO> despesasDeslocamentoDTO = new();

            foreach (var despesa in despesasDeslocamento)
            {
                despesasDeslocamentoDTO.Add(mapper.Map<DespesaDeslocamentoDTO>(despesa));
            }

            return despesasDeslocamentoDTO;
        }



        public static Viagem ConverterDTO(ViagemDTO viagemDTO)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<ViagemDTO, Viagem>()
                //.ForMember(dst => dst.Funcionario, opt => opt.Ignore())
                .ForMember(dst => dst.Despesas, opt => opt.Ignore());

                cfg.CreateMap<FuncionarioDTO, Funcionario>();
            });

            Mapper mapper = new(config);

            Viagem viagem = mapper.Map<Viagem>(viagemDTO);

            return viagem;
        }

        public static ViagemDTO ConverterDTO(Viagem viagem)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Funcionario, FuncionarioDTO>();

                cfg.CreateMap<Viagem, ViagemDTO>()
                //.ForMember(dst => dst.Funcionario, opt => opt.MapFrom(src => src.Funcionario))
                .ForMember(dst => dst.StatusViagem, opt => opt.MapFrom(src => src.StatusViagem));
            });

            Mapper mapper = new(config);

            ViagemDTO viagemDTO = mapper.Map<ViagemDTO>(viagem);

            //viagemDTO.Funcionario = ConverterDTO(viagem.Funcionario);

            return viagemDTO;
        }


        public static List<ViagemDTO> ConverterDTO(List<Viagem> viagens)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Funcionario, FuncionarioDTO>();


                cfg.CreateMap<Viagem, ViagemDTO>()
                //.ForMember(dst => dst.Funcionario, opt => opt.Ignore())
                .ForMember(dst => dst.StatusViagem, opt => opt.MapFrom(src => src.StatusViagem));
                //.ForMember(dst => dst.StatusViagem, opt => opt.MapFrom(src => src.StatusViagem.ToString()));
            });

            Mapper mapper = new(config);

            List<ViagemDTO> viagensDTO = new();

            foreach (var viagem in viagens)
            {
                viagensDTO.Add(mapper.Map<ViagemDTO>(viagem));
            }

            return viagensDTO;
        }
        
        public static Funcionario ConverterDTO(FuncionarioDTO funcionarioDTO)
        {            
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<FuncionarioDTO, Funcionario>()
                .ForMember(dst => dst.PasswordHash, opt => opt.Ignore())
                .ForMember(dst => dst.PasswordSalt, opt => opt.Ignore())
                //.ForMember(dst => dst.Endereco, opt => opt.Ignore())
                //.ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            return mapper.Map<Funcionario>(funcionarioDTO);
        }

        public static FuncionarioDTO ConverterDTO(Funcionario funcionario)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Funcionario, FuncionarioDTO>();
                //.ForMember(dst => dst., opt => opt.Ignore())
                //.ForMember(dst => dst.PasswordSalt, opt => opt.Ignore())
                //.ForMember(dst => dst.Endereco, opt => opt.Ignore())
                //.ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            return mapper.Map<FuncionarioDTO>(funcionario);
        }

    }
}
