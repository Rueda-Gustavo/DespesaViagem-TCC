using AutoMapper;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Server.Mapping
{
    public static class MappingDTOs
    {
        public static DespesaHospedagem ConverterDTO(DespesaHospedagemDTO despesaHospedagemDTO)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<DespesaHospedagemDTO, DespesaHospedagem>()
                .ForMember(dst => dst.Viagem, opt => opt.Ignore())
                .ForMember(dst => dst.Endereco, opt => opt.Ignore());
            });

            Mapper mapper = new(config);

            DespesaHospedagem despesa = mapper.Map<DespesaHospedagem>(despesaHospedagemDTO);

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
    }
}
