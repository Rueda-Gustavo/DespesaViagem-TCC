﻿using AutoMapper;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
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
                .ForMember(dst => dst.Endereco, opt => opt.Ignore())
                .ForMember(dst => dst.TotalDespesa, opt => opt.Ignore());
                ;
            });

            Mapper mapper = new(config);

            DespesaHospedagem despesa = mapper.Map<DespesaHospedagem>(despesaHospedagemDTO);
            
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
