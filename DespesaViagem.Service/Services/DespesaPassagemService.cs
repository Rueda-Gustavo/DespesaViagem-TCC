using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Infra.Repositories;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Services
{
    public class DespesaPassagemService : IDespesasService<DespesaPassagem>
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IDespesasRepository<DespesaPassagem> _despesaRepository;

        public DespesaPassagemService(IDespesasRepository<DespesaPassagem> despesaRepository,
            IViagemRepository viagemRepository)
        {
            _viagemRepository = viagemRepository;
            _despesaRepository = despesaRepository;            
        }

        public async Task<Result<IEnumerable<DespesaPassagem>>> ObterTodasDespesas(int idViagem)
        {            
            Viagem viagem = await _viagemRepository.ObterPorId(idViagem);

            if (viagem is null)
                return Result.Failure<IEnumerable<DespesaPassagem>>("Informe uma viagem válida.");
                        
            IEnumerable<DespesaPassagem> despesa = await _despesaRepository.ObterTodos(idViagem);
            return Result.FailureIf(despesa is null, despesa, "Não existem despesas para a viagem informada!!");
        }        

        public async Task<Result<DespesaPassagem>> ObterDespesaPorId(string id)
        {
            _ = int.TryParse(id, out int idDespesa);

            if (idDespesa > 0)
            {
                DespesaPassagem despesa = await _despesaRepository.ObterPorId(idDespesa);
                return Result.FailureIf(despesa is null, despesa, "Essa despesa não foi encontrada ou não existe!");
            }

            return Result.Failure<DespesaPassagem>("Especifique um id válido!!");
        }

        public async Task<Result<IEnumerable<DespesaPassagem>>> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            _ = int.TryParse(idViagem, out int id);

            if (id > 0)
            {
                IEnumerable<DespesaPassagem> despesas = await _despesaRepository.ObterPorFiltro(filtro, id);
                return Result.FailureIf(despesas is null, despesas, "Essas despesas não foram encontradas ou não existem!");
            }

            return Result.Failure<IEnumerable<DespesaPassagem>>("Especifique um id válido para a viagem.");
        }

        public async Task<Result<DespesaPassagem>> AdicionarDespesa(DespesaPassagem despesa)
        {
            if (await _despesaRepository.ObterPorId(despesa.Id) is not null)
                return Result.Failure<DespesaPassagem>("Já existe uma despesa como essa!");

            Viagem viagem = await _viagemRepository.ObterPorId(despesa.IdViagem);

            if (viagem is null || (viagem.StatusViagem != StatusViagem.Aberta && viagem.StatusViagem != StatusViagem.EmAndamento))
                return Result.Failure<DespesaPassagem>("Viagem não encontrada ou não existe uma viagem aberta ou em andamento.");

            viagem.AdicionarDespesa(despesa);
            viagem.AtualizarTotalDespesas();

            //await _viagemRepository.Update(viagem);         
            await _despesaRepository.Insert(despesa);
            return Result.Success(despesa);
        }

        public async Task<Result<DespesaPassagem>> AlterarDespesa(DespesaPassagem despesa)
        {
            DespesaPassagem despesaAtual = await _despesaRepository.ObterPorId(despesa.Id);

            if (despesaAtual is null)
                return Result.Failure<DespesaPassagem>("Despesa não encontrada!");

            if (despesaAtual.TotalDespesa != despesa.TotalDespesa && despesa.TotalDespesa > 0)
            {
                Viagem viagem = await _viagemRepository.ObterPorId(despesa.IdViagem);
                viagem.AtualizarDespesa(despesa);
                await _viagemRepository.Update(viagem);
            }

            await _despesaRepository.Update(despesa);
            return Result.Success(despesa);
        }

        public async Task<Result<DespesaPassagem>> RemoverViagem(int id)
        {
            DespesaPassagem despesa = await _despesaRepository.ObterPorId(id);
            if (despesa is null)
                return Result.Failure<DespesaPassagem>("Despesa não encontrada!");

            await _despesaRepository.Delete(despesa);
            return Result.Success(despesa); 
        }
    }
}
