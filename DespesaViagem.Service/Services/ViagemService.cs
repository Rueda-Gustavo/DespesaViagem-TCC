using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Infra.Repositories;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Services.Interfaces;
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

        public async Task<Result<IEnumerable<Viagem>>> ObterTodasViagens()
        {
            var viagens = await _viagemRepository.ObterTodos();

            if (!viagens.Any())
                return Result.Failure<IEnumerable<Viagem>>("Não existem viagens cadastradas.");

            foreach (var viagem in viagens)
            {
                IEnumerable<Despesa> despesas = await _despesaRepository.ObterTodos(viagem.Id);
                if (despesas.Any())
                    viagem.AdicionarDespesas(despesas);
            }

            return Result.Success(viagens);
        }

        public async Task<Result<Viagem>> ObterViagemPorId(int idViagem)
        {
            //_ = int.TryParse(id, out int idViagem);

            if (idViagem > 0)
            {
                Viagem viagem = await _viagemRepository.ObterPorId(idViagem);

                if (viagem is null)
                    return Result.Failure<Viagem>("Não existem viagens com o filtro especificado!");

                viagem = await AdicionarDespesaParaAViagem(viagem);
                viagem.AdicionarFuncionario(await _funcionarioRepository.ObterPorId(viagem.IdFuncionario));

                return Result.Success(viagem);
            }

            return Result.Failure<Viagem>("Especifique um id válido!!");
        }

        public async Task<Result<IEnumerable<Viagem>>> ObterViagemPorFiltro(string filtro)
        {
            IEnumerable<Viagem> viagens = await _viagemRepository.ObterPorFiltro(filtro);

            if (!viagens.Any())
                return Result.Failure<IEnumerable<Viagem>>("Não existem viagens com o filtro especificado!");

            viagens = await AdicionarDespesasParaAViagem(viagens);
            viagens = await AdicionarFuncionarioParaAViagem(viagens); 

            return Result.Success(viagens);
        }

        public async Task<Result<IEnumerable<Viagem>>> ObterViagemPorStatus(StatusViagem statusViagem)
        {
            IEnumerable<Viagem> viagens = await _viagemRepository.ObterViagemPorStatus(statusViagem);

            if (viagens is null || !viagens.Any())
                return Result.Failure<IEnumerable<Viagem>>("Não existem viagens com o status especificado!");

            viagens = await AdicionarDespesasParaAViagem(viagens);

            return Result.Success(viagens);
        }

        public async Task<Result<IEnumerable<Despesa>>> ObterTodasDespesas(int id)
        {
            return Result.Success(await _viagemRepository.ObterTodasDepesas(id));
        }

        public async Task<Result<Viagem>> AdicionarViagem(ViagemDTO viagemDTO)
        {
            viagemDTO.StatusViagem = StatusViagem.Aberta;
            Viagem viagem = MappingDTOs.ConverterDTO(viagemDTO);

            if (viagem is null || viagemDTO.Id != 0) //Caso o Id não seja 0 irá dar erro na hora de adicionar a viagem
                return Result.Failure<Viagem>("Nenhuma viagem informada.");

            if ((await ObterViagemAbertaOuEmAndamento()) is not null)
                return Result.Failure<Viagem>("Já existe uma viagem aberta ou em andamento.");

            viagem.VerificarDataInicialeFinal();

            Funcionario funcinonario = await _funcionarioRepository.ObterPorId(viagem.IdFuncionario);
            if (funcinonario is null)
                return Result.Failure<Viagem>("Funcionário não encontrado!");

            viagem.AdicionarFuncionario(funcinonario);

            await _viagemRepository.Insert(viagem);
            return Result.Success(viagem);
        }

        public async Task<Result<Viagem>> AlterarViagem(ViagemDTO viagemDTO)
        {
            Viagem viagem = MappingDTOs.ConverterDTO(viagemDTO);

            if (viagem is null || viagemDTO.Id <= 0)
                return Result.Failure<Viagem>("Nenhuma viagem informada.");

            Viagem? viagemAuxiliar = await ObterViagemAbertaOuEmAndamento();

            if (viagemAuxiliar is null || viagemAuxiliar.Id != viagem.Id)
                return Result.Failure<Viagem>("Nenhuma viagem em Aberto ou Em Andamento, ou a viagem informada está Cancelada ou Encerrada.");

            if (viagemAuxiliar.Adiantamento != viagem.Adiantamento && viagemAuxiliar.StatusViagem != StatusViagem.Aberta)
                return Result.Failure<Viagem>("O Adiantamento incial não pode ser modificado.");


            viagem.DefinirStatusViagem(viagemAuxiliar.StatusViagem); //O status é utilizado da forma como já está no banco por ele não ser mudado pelo usuário diretamente
            viagem.DefinirAdiantamento(viagem.Adiantamento); //Caso a viagem esteja em aberto será possível modificar o adiantamento

            await _viagemRepository.Update(viagem);
            return Result.Success(viagem);
        }

        public async Task<Result<Viagem>> RemoverViagem(int id)
        {
            Viagem viagem = await _viagemRepository.ObterPorId(id);

            if (viagem is null)
                return Result.Failure<Viagem>("Viagem não existe!");
            await _viagemRepository.Delete(viagem);
            return Result.Success(viagem);
        }

        public Result<decimal> ObterPrestacaoDeContas(Viagem viagem)
        {
            return Result.Success(viagem.GerarPrestacaoDeContas());
        }

        public async Task<Result<Viagem>> IniciarViagem()
        {
            var viagemAberta = await _viagemRepository.ObterViagemPorStatus(StatusViagem.Aberta);

            if (viagemAberta is null || !viagemAberta.Any())
                return Result.Failure<Viagem>("Não há nenhuma viagem Aberta");

            Viagem viagem = viagemAberta.First();

            viagem.IniciarViagem();
            await _viagemRepository.Update(viagem);
            return Result.Success(viagem);
        }

        public async Task<Result<Viagem>> EncerrarViagem()
        {
            Viagem? viagem = await ObterViagemAbertaOuEmAndamento();

            if (viagem is null) return Result.Failure<Viagem>("Nenhuma viagem Em Andamento ou Aberta.");

            if (viagem.StatusViagem != StatusViagem.EmAndamento)
            {
                return Result.Failure<Viagem>("A viagem deve estar em andamento para ser encerrada.");
            }

            viagem.EncerrarViagem();
            await _viagemRepository.Update(viagem);
            return Result.Success(viagem);
        }

        public async Task<Result<Viagem>> CancelarViagem()
        {
            Viagem? viagem = await ObterViagemAbertaOuEmAndamento();

            if (viagem is null) return Result.Failure<Viagem>("Nenhuma viagem Em Andamento ou Aberta.");

            if (await ViagemCancelada(viagem.Id))
            {
                return Result.Failure<Viagem>("Viagem já foi cancelada.");
            }

            viagem.CancelarViagem();
            await _viagemRepository.Update(viagem);
            return Result.Success(viagem);
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

        private async Task<IEnumerable<Viagem>> AdicionarDespesasParaAViagem(IEnumerable<Viagem> viagens)
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

        private async Task<IEnumerable<Viagem>> AdicionarFuncionarioParaAViagem(IEnumerable<Viagem> viagens)
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
