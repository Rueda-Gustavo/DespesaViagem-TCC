using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Services
{
    public class ViagemService : IViagemService
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IDespesaService _despesaService;

        public ViagemService(IViagemRepository viagemRepository, IDespesaService despesaService)
        {
            _viagemRepository = viagemRepository;
        }

        public async Task<Result<IEnumerable<Viagem>>> ObterTodasViagens()
        {
            var viagens = await _viagemRepository.ObterTodos();
            /*
            foreach (var viagem in viagens)
            {
                Result<IEnumerable<Despesa>> despesas = await _despesaService.ObterTodasDespesas(viagem.Id);
                viagem.AdicionarDespesas(despesas.Value.ToList());
            }
            */

            return Result.Success(viagens);
        }

        public async Task<Result<Viagem>> ObterViagemPorId(string id)
        {
            _ = int.TryParse(id, out int idViagem);

            if (idViagem > 0)
            {
                Viagem viagem = await _viagemRepository.ObterPorId(idViagem);

                return Result.FailureIf(viagem is null, viagem, "Não existem viagens com o filtro especificado!");
            }

            return Result.Failure<Viagem>("Especifique um id válido!!");
        }

        public async Task<Result<IEnumerable<Viagem>>> ObterViagemPorFiltro(string filtro)
        {
            IEnumerable<Viagem> viagens = await _viagemRepository.ObterPorFiltro(filtro);
            return Result.FailureIf(viagens is null, viagens, "Não existem viagens com o filtro especificado!");
        }

        public async Task<Result<IEnumerable<Despesa>>> ObterTodasDespesas(int id)
        {
            return Result.Success(await _viagemRepository.ObterTodasDepesas(id));
        }

        public async Task<Result<Viagem>> ObterViagemEmAndamento()
        {
            Viagem viagem = await _viagemRepository.ObterViagemAbertaOuEmAndamento();
            
            if (viagem is not null && viagem.StatusViagem == StatusViagem.EmAndamento)
                return viagem;

            return Result.Failure<Viagem>("Não existe uma viagem em andamento.");
        }
        public async Task<Result<Viagem>> ObterViagemAberta()
        {
            Viagem viagem = await _viagemRepository.ObterViagemAbertaOuEmAndamento();            

            if (viagem is not null && viagem.StatusViagem == StatusViagem.Aberta )
                return viagem;

            return Result.Failure<Viagem>("Não existe uma viagem aberta.");
        }

        public async Task<Result<Viagem>> AdicionarViagem(Viagem viagem)
        {
            if (await ViagemEmAndamento() || await ViagemAberta())
                return Result.Failure<Viagem>("Já existe uma viagem aberta ou em andamento.");

            if(viagem.DataFinal < viagem.DataInicial)
                return Result.Failure<Viagem>("Insira uma período válido, com pelo menos 1 dia de diferença entre as datas inicial e final. Por exemplo: Data inicial dia 01 e data final dia 02");

            await _viagemRepository.Insert(viagem);
            return Result.Success(viagem);
        }

        public async Task<Result<Viagem>> AlterarViagem(Viagem viagem)
        {
            if (!await ViagemAberta())
                return Result.Failure<Viagem>("Nenhuma viagem em aberto.");

            if ((await _viagemRepository.ObterPorId(viagem.Id)).StatusViagem == StatusViagem.EmAndamento)
                return Result.Failure<Viagem>("A viagem já está em andamento, não é possível alterar os dados.");

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

        public async Task<Result<decimal>> ObterPrestacaoDeContas(Viagem viagem)
        {
            return Result.Success(viagem.GerarPrestacaoDeContas());
        }

        public async Task<Result<Viagem>> IniciarViagem(Viagem viagem)
        {
            if (await ViagemEmAndamento())
            {
                return Result.Failure<Viagem>("Já existe uma viagem aberta ou em andamento.");
            }
            viagem.IniciarViagem();
            await _viagemRepository.Update(viagem);
            return Result.Success(viagem);
        }

        public async Task<Result<Viagem>> EncerrarViagem(Viagem viagem)
        {
            if (!await ViagemEmAndamento())
            {
                return Result.Failure<Viagem>("A viagem deve estar em andamento para ser encerrada.");
            }
            viagem.EncerrarViagem();
            await _viagemRepository.Update(viagem);
            return Result.Success(viagem);
        }

        public async Task<Result<Viagem>> CancelarViagem(Viagem viagem)
        {
            if (await ViagemCancelada(viagem.Id))
            {
                return Result.Failure<Viagem>("Viagem já foi cancelada.");
            }
            viagem.CancelarViagem();
            await _viagemRepository.Update(viagem);
            return Result.Success(viagem);
        }



        //Verifica se já existe alguma viagem em andamento, caso não existe pode prosseguir com a criação.
        private async Task<bool> ViagemEmAndamento()
        {
            Viagem viagem = await _viagemRepository.ObterViagemAbertaOuEmAndamento();
            if (viagem is null)
                return false;

            if (viagem.StatusViagem == StatusViagem.EmAndamento)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ViagemAberta()
        {
            Viagem viagem = await _viagemRepository.ObterViagemAbertaOuEmAndamento();
            if (viagem is null)
                return false;

            if (viagem.StatusViagem == StatusViagem.Aberta)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ViagemCancelada(int idViagem)
        {
            Viagem viagem = await _viagemRepository.ObterPorId(idViagem);
            //foreach (var viagem in viagens)
            //{
            if (viagem.StatusViagem == StatusViagem.Cancelada)
            {
                return true;
            }
            //}
            return false;
        }
    }
}
