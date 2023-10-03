using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
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

        public AdminService(IAdminRepository adminRepository, IUsuarioRepository usuarioRepository)
        {
            _adminRepository = adminRepository;
            _usuarioRepository = usuarioRepository;
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
    }
}
