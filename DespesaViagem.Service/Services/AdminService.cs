using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Infra.Repositories;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public AdminService(IAdminRepository adminRepository, IUsuarioRepository usuarioRepository, IFuncionarioRepository funcionarioRepository)
        {
            _adminRepository = adminRepository;
            _usuarioRepository = usuarioRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<Result<AdminManutencaoDTO>> ObterListaUsuarios(int idAdmin)
        {
            Usuario? usuario = await _usuarioRepository.ObterUsuario(idAdmin);

            if (usuario is null || usuario.TipoDeUsuario != RolesUsuario.Administrador)
                return Result.Failure<AdminManutencaoDTO>("Usuário não encontrado ou não está autorizado!");

            AdminManutencao manutencao = await _adminRepository.ObterListaUsuarios();

            if (manutencao is null || !manutencao.Funcionarios.Any())
                return Result.Failure<AdminManutencaoDTO>("Nenhum usuário foi encontrado.");

            AdminManutencaoDTO manutencaoDTO = MappingDTOs.ConverterDTO(manutencao);

            return Result.Success(manutencaoDTO);
        }

        public async Task<Result<IEnumerable<FuncionarioDTO>>> ObterListaFuncionarios(int idAdmin)
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterTodos();

            Usuario? usuario = await _usuarioRepository.ObterUsuario(idAdmin);

            if (usuario is null || usuario.TipoDeUsuario != RolesUsuario.Administrador)
                return Result.Failure<IEnumerable<FuncionarioDTO>>("Usuário não encontrado ou não está autorizado!");

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<FuncionarioDTO>>("Não foram encontrados funcionarios.");

            IEnumerable<FuncionarioDTO> funcionariosDTO = MappingDTOs.ConverterDTO(funcionarios.ToList());

            return Result.Success(funcionariosDTO);
        }
    }
}
