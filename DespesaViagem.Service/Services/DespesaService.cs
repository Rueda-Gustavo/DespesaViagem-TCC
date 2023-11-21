using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDespesaRepository _despesaRepository;

        public DespesaService(IDespesaRepository despesaRepository, IViagemRepository viagemRepository, IUsuarioRepository usuarioRepository)
        {
            _despesaRepository = despesaRepository;
            _viagemRepository = viagemRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result<IEnumerable<DespesaDTO>>> ObterTodasDespesas(int idUsuario)
        {
            Usuario usuario = await _usuarioRepository.ObterUsuario(idUsuario) ?? new();

            if (usuario is null)
                return Result.Failure<IEnumerable<DespesaDTO>>("Usuário não encontrado.");

            List<Viagem> viagens = new();

            if (usuario.TipoDeUsuario == RolesUsuario.Gestor)
            {
                viagens = await _viagemRepository.ObterTodosGestor(idUsuario);
            }
            else if (usuario.TipoDeUsuario == RolesUsuario.Administrador)
            {
                viagens = await _viagemRepository.ObterTodos();
            }
            else 
            {
                viagens = await _viagemRepository.ObterTodos(idUsuario);
            }

            if (!viagens.Any())
                return Result.Failure<IEnumerable<DespesaDTO>>("Não existem viagens cadastradas.");

            List<int> idsViagens = viagens.Select(v => v.Id).ToList();

            IEnumerable<Despesa> despesas = await _despesaRepository.ObterTodos(idsViagens);

            IEnumerable<DespesaDTO> despesasDTO = MappingDTOs.ConverterDTO(despesas.ToList());

            if (despesasDTO is null || !despesasDTO.Any())
                return Result.Failure<IEnumerable<DespesaDTO>>("Não existem despesas cadastradas!");

            return Result.Success(despesasDTO);
        }


        public async Task<Result<IEnumerable<DespesaDTO>>> ObterTodasDespesasDaViagem(int idViagem)
        {
            Viagem viagem = await _viagemRepository.ObterPorId(idViagem);

            idViagem = viagem.Id;
            IEnumerable<Despesa> despesas = await _despesaRepository.ObterTodos(idViagem);

            IEnumerable<DespesaDTO> despesasDTO = MappingDTOs.ConverterDTO(despesas.ToList());

            if (despesasDTO is null || !despesasDTO.Any())
                return Result.Failure<IEnumerable<DespesaDTO>>("Não existem despesas para a viagem informada!!");

            return Result.Success(despesasDTO);
        }

        public async Task<Result<DespesaDTO>> ObterDespesaPorId(int idDespesa)
        {
            if (idDespesa > 0)
            {
                Despesa despesa = await _despesaRepository.ObterPorId(idDespesa);
                DespesaDTO despesaDTO = MappingDTOs.ConverterDTO(despesa);

                if (despesaDTO is null)
                    return Result.Failure<DespesaDTO>("Essa despesa não foi encontrada ou não existe!");

                return Result.Success(despesaDTO);
            }

            return Result.Failure<DespesaDTO>("Especifique um id válido!!");
        }

        public async Task<Result<IEnumerable<DespesaDTO>>> ObterDespesasPorFiltro(string filtro, int idViagem)
        {
            IEnumerable<Despesa> despesas = await _despesaRepository.ObterPorFiltro(filtro, idViagem);

            IEnumerable<DespesaDTO> despesasDTO = MappingDTOs.ConverterDTO(despesas.ToList());

            if (despesasDTO is null || !despesasDTO.Any())
                return Result.Failure<IEnumerable<DespesaDTO>>("Essas despesas não foram encontradas ou não existem!");

            return Result.Success(despesasDTO);
        }
    }
}
