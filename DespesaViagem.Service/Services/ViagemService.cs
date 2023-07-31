using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Infra.Repositories;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Services
{
    public class ViagemService : IViagemService
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IDespesaRepository _despesaRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ViagemService(IViagemRepository viagemRepository,
            IDespesaRepository despesaRepository, IFuncionarioRepository funcionarioRepository)
        {
            _viagemRepository = viagemRepository;
            _despesaRepository = despesaRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<Result<List<ViagemDTO>>> ObterTodasViagens()
        {
            List<Viagem> viagens = await _viagemRepository.ObterTodos();

            if (!viagens.Any())
                return Result.Failure<List<ViagemDTO>>("Não existem viagens cadastradas.");

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            return Result.Success(viagensDTO);
        }

        public async Task<Result<ViagemDTO>> ObterViagemPorId(int idViagem)
        {
            //_ = int.TryParse(id, out int idViagem);

            if (idViagem > 0)
            {
                Viagem viagem = await _viagemRepository.ObterPorId(idViagem);

                if (viagem is null)
                    return Result.Failure<ViagemDTO>("Não existem viagens com o filtro especificado!");

                viagem = await AdicionarDespesaParaAViagem(viagem);
                viagem.AdicionarFuncionario(await _funcionarioRepository.ObterPorId(viagem.IdFuncionario));

                ViagemDTO viagemDTO = MappingDTOs.ConverterDTO(viagem);

                return Result.Success(viagemDTO);
            }

            return Result.Failure<ViagemDTO>("Especifique um id válido!!");
        }

        public async Task<Result<List<ViagemDTO>>> ObterViagemPorFiltro(string filtro)
        {
            List<Viagem> viagens = await _viagemRepository.ObterPorFiltro(filtro);

            if (!viagens.Any())
                return Result.Failure<List<ViagemDTO>>("Não existem viagens com o filtro especificado!");

            //viagens = await AdicionarDespesasParaAViagem(viagens);
            //viagens = await AdicionarFuncionarioParaAViagem(viagens);
            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            return Result.Success(viagensDTO);
        }

        public async Task<Result<List<ViagemDTO>>> ObterViagemPorStatus(StatusViagem statusViagem)
        {
            List<Viagem> viagens = await _viagemRepository.ObterViagemPorStatus(statusViagem);

            if (viagens is null || !viagens.Any())
                return Result.Failure<List<ViagemDTO>>("Não existem viagens com o status especificado!");

            //viagens = await AdicionarDespesasParaAViagem(viagens);
            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            return Result.Success(viagensDTO);
        }

        public async Task<Result<List<DespesaDTO>>> ObterTodasDespesas(int id)
        {
            List<Despesa> despesas = await _viagemRepository.ObterTodasDepesas(id);
            List<DespesaDTO> despesasDTO = MappingDTOs.ConverterDTO(despesas);
            return Result.Success(despesasDTO);
        }

        public async Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagemDTO)
        {
            viagemDTO.StatusViagem = StatusViagem.Aberta.ToString();
            Viagem viagem = MappingDTOs.ConverterDTO(viagemDTO);

            if (viagem is null || viagemDTO.Id != 0) //Caso o Id não seja 0 irá dar erro na hora de adicionar a viagem
                return Result.Failure<ViagemDTO>("Nenhuma viagem informada.");

            if ((await ObterViagemAbertaOuEmAndamento()) is not null)
                return Result.Failure<ViagemDTO>("Já existe uma viagem aberta ou em andamento.");

            viagem.VerificarDataInicialeFinal();

            Funcionario funcinonario = await _funcionarioRepository.ObterPorId(viagem.IdFuncionario);
            if (funcinonario is null)
                return Result.Failure<ViagemDTO>("Funcionário não encontrado!");

            //viagem.AdicionarFuncionario(funcinonario);

            await _viagemRepository.Insert(viagem);

            return Result.Success(viagemDTO);
        }

        public async Task<Result<ViagemDTO>> AlterarViagem(ViagemDTO viagemDTO)
        {
            Viagem viagem = MappingDTOs.ConverterDTO(viagemDTO);

            if (viagem is null || viagemDTO.Id <= 0)
                return Result.Failure<ViagemDTO>("Nenhuma viagem informada.");

            Viagem? viagemAuxiliar = await ObterViagemAbertaOuEmAndamento();

            if (viagemAuxiliar is null || viagemAuxiliar.Id != viagem.Id)
                return Result.Failure<ViagemDTO>("Nenhuma viagem em Aberto ou Em Andamento, ou a viagem informada está Cancelada ou Encerrada.");

            if (viagemAuxiliar.Adiantamento != viagem.Adiantamento && viagemAuxiliar.StatusViagem != StatusViagem.Aberta)
                return Result.Failure<ViagemDTO>("O Adiantamento incial não pode ser modificado.");


            viagem.DefinirStatusViagem(viagemAuxiliar.StatusViagem); //O status é utilizado da forma como já está no banco por ele não ser mudado pelo usuário diretamente
            viagem.DefinirAdiantamento(viagem.Adiantamento); //Caso a viagem esteja em aberto será possível modificar o adiantamento

            await _viagemRepository.Update(viagem);
            return Result.Success(viagemDTO);
        }

        public async Task<Result<ViagemDTO>> RemoverViagem(int id)
        {
            Viagem viagem = await _viagemRepository.ObterPorId(id);

            if (viagem is null)
                return Result.Failure<ViagemDTO>("Viagem não existe!");
            await _viagemRepository.Delete(viagem);
            ViagemDTO viagemDTO = MappingDTOs.ConverterDTO(viagem);
            return Result.Success(viagemDTO);
        }

        public Result<decimal> ObterPrestacaoDeContas(Viagem viagem)
        {
            return Result.Success(viagem.GerarPrestacaoDeContas());
        }

        public async Task<Result<ViagemDTO>> IniciarViagem()
        {
            var viagemAberta = await _viagemRepository.ObterViagemPorStatus(StatusViagem.Aberta);

            if (viagemAberta is null || !viagemAberta.Any())
                return Result.Failure<ViagemDTO>("Não há nenhuma viagem Aberta");

            Viagem viagem = viagemAberta.First();

            viagem.IniciarViagem();
            await _viagemRepository.Update(viagem);
            ViagemDTO viagemDTO = MappingDTOs.ConverterDTO(viagem);

            return Result.Success(viagemDTO);
        }

        public async Task<Result<ViagemDTO>> EncerrarViagem()
        {
            Viagem? viagem = await ObterViagemAbertaOuEmAndamento();

            if (viagem is null) return Result.Failure<ViagemDTO>("Nenhuma viagem Em Andamento ou Aberta.");

            if (viagem.StatusViagem != StatusViagem.EmAndamento)
            {
                return Result.Failure<ViagemDTO>("A viagem deve estar em andamento para ser encerrada.");
            }

            viagem.EncerrarViagem();
            await _viagemRepository.Update(viagem);
            ViagemDTO viagemDTO = MappingDTOs.ConverterDTO(viagem);

            return Result.Success(viagemDTO);
        }

        public async Task<Result<ViagemDTO>> CancelarViagem()
        {
            Viagem? viagem = await ObterViagemAbertaOuEmAndamento();

            if (viagem is null) return Result.Failure<ViagemDTO>("Nenhuma viagem Em Andamento ou Aberta.");

            if (await ViagemCancelada(viagem.Id))
            {
                return Result.Failure<ViagemDTO>("Viagem já foi cancelada.");
            }

            viagem.CancelarViagem();
            await _viagemRepository.Update(viagem);
            ViagemDTO viagemDTO = MappingDTOs.ConverterDTO(viagem);

            return Result.Success(viagemDTO);
        }

        //Verifica se já existe alguma viagem em andamento, caso não existe pode prosseguir com a criação.
        private async Task<Viagem?> ObterViagemAbertaOuEmAndamento()
        {
            var viagem = await _viagemRepository.ObterViagemPorStatus(StatusViagem.Aberta);
            if (!viagem.Any())
                viagem = await _viagemRepository.ObterViagemPorStatus(StatusViagem.EmAndamento);

            if (!viagem.Any())
                return null;


            return viagem.First();
        }

        private async Task<bool> ViagemCancelada(int idViagem)
        {
            Viagem viagem = await _viagemRepository.ObterPorId(idViagem);

            if (viagem.StatusViagem == StatusViagem.Cancelada)
            {
                return true;
            }

            return false;
        }

        private async Task<List<Viagem>> AdicionarDespesasParaAViagem(List<Viagem> viagens)
        {
            foreach (var viagem in viagens)
            {
                IEnumerable<Despesa> despesas = await _despesaRepository.ObterTodos(viagem.Id);
                if (despesas.Any())
                    viagem.AdicionarDespesas(despesas);
            }
            return viagens;
        }

        private async Task<Viagem> AdicionarDespesaParaAViagem(Viagem viagem)
        {
            IEnumerable<Despesa> despesas = await _despesaRepository.ObterTodos(viagem.Id);
            if (despesas.Any())
                viagem.AdicionarDespesas(despesas);

            return viagem;
        }

        private async Task<List<Viagem>> AdicionarFuncionarioParaAViagem(List<Viagem> viagens)
        {
            foreach (var viagem in viagens)
            {
                Funcionario funcionario = await _funcionarioRepository.ObterPorId(viagem.IdFuncionario);
                viagem.AdicionarFuncionario(funcionario);
            }

            return viagens;
        }
    }
}
