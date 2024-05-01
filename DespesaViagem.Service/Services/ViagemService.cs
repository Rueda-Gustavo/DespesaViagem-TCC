using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly float _despesasPorPagina = 6f;
        private readonly float _viagensPorPagina = 6f;
        public ViagemService(IViagemRepository viagemRepository,
            IDespesaRepository despesaRepository,
            IFuncionarioRepository funcionarioRepository,
            IUsuarioRepository usuarioRepository)
        {
            _viagemRepository = viagemRepository;
            _despesaRepository = despesaRepository;
            _funcionarioRepository = funcionarioRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result<List<ViagemDTO>>> ObterTodasViagens(int idUsuario)
        {
            if (idUsuario == 0)
                return Result.Failure<List<ViagemDTO>>("Usuário não autorizado.");

            Usuario? usuario = await _usuarioRepository.ObterUsuario(idUsuario);

            if (usuario is null)
                return Result.Failure<List<ViagemDTO>>("Usuário não encontrado.");

            List<Viagem> viagens;

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
                return Result.Failure<List<ViagemDTO>>("Não existem viagens cadastradas.");

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            return Result.Success(viagensDTO);
        }



        /*Trecho antigo, inutilizado------------------------------------------------------------------------------------
        public async Task<Result<List<ViagemDTO>>> ObterViagensPorFuncionario(int idFuncionario)
        {
            Funcionario? funcionario = await _funcionarioRepository.ObterPorId(idFuncionario);

            if (funcionario is null)
                return Result.Failure<List<ViagemDTO>>("Funcionário não encontrado.");

            List<Viagem> viagens = await _viagemRepository.ObterTodos(idFuncionario);

            if (!viagens.Any())
                return Result.Failure<List<ViagemDTO>>("Não existem viagens cadastradas.");

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            return Result.Success(viagensDTO);
        }
        //-------------------------------------------------------------------------------------------------------------*/

        public async Task<Result<ViagensPorPagina>> ObterViagensPorFuncionario(int idFuncionario, int pagina)
        {
            Funcionario? funcionario = await _funcionarioRepository.ObterPorId(idFuncionario);

            if (funcionario is null)
                return Result.Failure<ViagensPorPagina>("Funcionário não encontrado.");

            List<Viagem> viagens = await _viagemRepository.ObterTodos(idFuncionario);

            if (!viagens.Any())
                return Result.Failure<ViagensPorPagina>("Não existem viagens cadastradas.");

            double quantidadeDePaginas = Math.Ceiling(viagens.Count / _viagensPorPagina);
            viagens = viagens
                .Skip((pagina - 1) * (int)_viagensPorPagina)
                .Take((int)_viagensPorPagina)
                .ToList();

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            ViagensPorPagina result = new()
            {
                Viagens = viagensDTO,
                PaginaAtual = pagina,
                QuantidadeDePaginas = (int)quantidadeDePaginas
            };

            return Result.Success(result);
        }


        /*Trecho antigo, inutilizado------------------------------------------------------------------------------------
        public async Task<Result<List<ViagemDTO>>> ObterViagensPorDepartamento(int idDepartamento)
        {
                        
            List<Viagem> viagens = await _viagemRepository.ObterPorDepartamento(idDepartamento);

            if (!viagens.Any())
                return Result.Failure<List<ViagemDTO>>("Não existem viagens cadastradas.");

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            return Result.Success(viagensDTO);
        }
        //-------------------------------------------------------------------------------------------------------------*/

        public async Task<Result<ViagensPorPagina>> ObterViagensPorDepartamento(int idDepartamento, int pagina)
        {
            List<Viagem> viagens = await _viagemRepository.ObterPorDepartamento(idDepartamento);

            if (!viagens.Any())
                return Result.Failure<ViagensPorPagina>("Não existem viagens cadastradas.");

            double quantidadeDePaginas = Math.Ceiling(viagens.Count / _viagensPorPagina);
            viagens = viagens
                .Skip((pagina - 1) * (int)_viagensPorPagina)
                .Take((int)_viagensPorPagina)
                .ToList();

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            ViagensPorPagina result = new()
            {
                Viagens = viagensDTO,
                PaginaAtual = pagina,
                QuantidadeDePaginas = (int)quantidadeDePaginas
            };

            return Result.Success(result);
        }

        public async Task<Result<ViagemDTO>> ObterViagemPorId(int idViagem)
        {
            //_ = int.TryParse(id, out int idViagem);

            if (idViagem > 0)
            {
                Viagem viagem = await _viagemRepository.ObterPorId(idViagem);

                if (viagem is null)
                    return Result.Failure<ViagemDTO>("Não existem viagens com o filtro especificado!");

                //viagem = await AdicionarDespesaParaAViagem(viagem);
                viagem.AdicionarFuncionario(await _funcionarioRepository.ObterPorId(viagem.IdFuncionario));

                ViagemDTO viagemDTO = MappingDTOs.ConverterDTO(viagem);

                return Result.Success(viagemDTO);
            }

            return Result.Failure<ViagemDTO>("Especifique um id válido!!");
        }

        /*Trecho antigo, inutilizado------------------------------------------------------------------------------------
        public async Task<Result<List<ViagemDTO>>> ObterViagensPorFiltro(string filtro)
        {
            List<Viagem> viagens = await _viagemRepository.ObterPorFiltro(filtro);

            if (!viagens.Any())
                return Result.Failure<List<ViagemDTO>>("Não existem viagens com o filtro especificado!");

            //viagens = await AdicionarDespesasParaAViagem(viagens);
            //viagens = await AdicionarFuncionarioParaAViagem(viagens);
            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            return Result.Success(viagensDTO);
        }
        //-------------------------------------------------------------------------------------------------------------*/

        public async Task<Result<ViagensPorPagina>> ObterViagensPorFiltro(string filtro, int pagina)
        {
            List<Viagem> viagens = await _viagemRepository.ObterPorFiltro(filtro);

            if (!viagens.Any())
                return Result.Failure<ViagensPorPagina>("Não existem viagens com o filtro especificado!");

            double quantidadeDePaginas = Math.Ceiling(viagens.Count / _viagensPorPagina);
            viagens = viagens
                .Skip((pagina - 1) * (int)_viagensPorPagina)
                .Take((int)_viagensPorPagina)
                .ToList();

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            ViagensPorPagina result = new()
            {
                Viagens = viagensDTO,
                PaginaAtual = pagina,
                QuantidadeDePaginas = (int)quantidadeDePaginas
            };

            return Result.Success(result);
        }


        /*Trecho antigo, inutilizado------------------------------------------------------------------------------------
        public async Task<Result<List<ViagemDTO>>> ObterViagemPorStatus(StatusViagem statusViagem, int idFuncionario)
        {
            List<Viagem> viagens = await _viagemRepository.ObterViagemPorStatus(statusViagem, idFuncionario) ?? new();

            if (viagens is null || !viagens.Any())
                return Result.Failure<List<ViagemDTO>>("Não existem viagens com o status especificado!");

            //viagens = await AdicionarDespesasParaAViagem(viagens);
            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            return Result.Success(viagensDTO);
        }
        //-------------------------------------------------------------------------------------------------------------*/

        public async Task<Result<ViagensPorPagina>> ObterViagemPorStatus(StatusViagem statusViagem, int idFuncionario, int pagina)
        {
            var viagens = await _viagemRepository.ObterViagemPorStatus(statusViagem, idFuncionario) ?? new();

            if (viagens is null || !viagens.Any())
                return Result.Failure<ViagensPorPagina>("Não existem viagens com o status especificado!");

            double quantidadeDePaginas = Math.Ceiling(viagens.Count / _viagensPorPagina);
            viagens = viagens
                .Skip((pagina - 1) * (int)_viagensPorPagina)
                .Take((int)_viagensPorPagina)
                .ToList();

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            ViagensPorPagina result = new()
            {
                Viagens = viagensDTO,
                PaginaAtual = pagina,
                QuantidadeDePaginas = (int)quantidadeDePaginas
            };

            return Result.Success(result);
        }

        public async Task<Result<ViagensPorPagina>> ObterTodasViagensPorStatus(StatusViagem statusViagem, int idUsuario, int pagina)
        {
            var todasViagens = await ObterTodasViagens(idUsuario);

            if (todasViagens.IsFailure)
                return Result.Failure<ViagensPorPagina>(todasViagens.Error);

            List<ViagemDTO> viagens = todasViagens.Value.ToList();

            viagens = viagens.Where(v => v.StatusViagem == statusViagem).ToList();

            if (viagens is null || !viagens.Any())
                return Result.Failure<ViagensPorPagina>("Não existem viagens com o status especificado!");

            double quantidadeDePaginas = Math.Ceiling(viagens.Count / _viagensPorPagina);
            viagens = viagens
                .Skip((pagina - 1) * (int)_viagensPorPagina)
                .Take((int)_viagensPorPagina)
                .ToList();

            ViagensPorPagina result = new()
            {
                Viagens = viagens,
                PaginaAtual = pagina,
                QuantidadeDePaginas = (int)quantidadeDePaginas
            };

            return Result.Success(result);
        }

        public async Task<Result<List<ViagemDTO>>> ObterTodasViagensStatus(StatusViagem statusViagem, int idUsuario)
        {
            Usuario? usuario = await _usuarioRepository.ObterUsuario(idUsuario);

            if (usuario is null)
                return Result.Failure<List<ViagemDTO>>("Usuário não encontrado.");

            List<Viagem> viagens;

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
                return Result.Failure<List<ViagemDTO>>("Não existem viagens cadastradas.");

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            viagensDTO = viagensDTO.Where(v => v.StatusViagem == statusViagem).ToList();

            return Result.Success(viagensDTO);
        }


        public async Task<Result<List<ViagemDTO>>> ObterTodasViagensStatus(List<StatusViagem> statusViagem, int idUsuario)
        {
            Usuario? usuario = await _usuarioRepository.ObterUsuario(idUsuario);

            if (usuario is null)
                return Result.Failure<List<ViagemDTO>>("Usuário não encontrado.");

            List<Viagem> viagens;

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
                return Result.Failure<List<ViagemDTO>>("Não existem viagens cadastradas.");

            List<ViagemDTO> viagensDTO = MappingDTOs.ConverterDTO(viagens);

            viagensDTO = viagensDTO.Where(v => statusViagem.Contains(v.StatusViagem)).ToList();

            return Result.Success(viagensDTO);
        }

        public async Task<Result<List<DespesaDTO>>> ObterTodasDespesas(int idViagem)
        {
            List<Despesa> despesas = await _viagemRepository.ObterTodasDepesas(idViagem);
            List<DespesaDTO> despesasDTO = MappingDTOs.ConverterDTO(despesas);
            return Result.Success(despesasDTO);
        }

        public async Task<Result<DespesasPorPagina>> ObterDespesasPorPagina(int idViagem, int pagina)
        {
            double quantidadeDePaginas = Math.Ceiling((await _viagemRepository.ObterTodasDepesas(idViagem)).Count / _despesasPorPagina);
            List<Despesa> despesas = await _viagemRepository.ObterTodasDepesas(idViagem);
            despesas = despesas
                .Skip((pagina - 1) * (int)_despesasPorPagina)
                .Take((int)_despesasPorPagina)
                .ToList();

            //List<Despesa> despesas = await _viagemRepository.ObterDepesasPorPagina(idViagem, pagina, (int) despesasPorPagina);

            return new DespesasPorPagina()
            {
                Despesas = MappingDTOs.ConverterDTO(despesas),
                PaginaAtual = pagina,
                QuantidadeDePaginas = (int)quantidadeDePaginas
            };
        }

        public async Task<Result<ViagensPorPagina>> ObterViagensPaginadas(int idUsuario, int pagina)
        {
            var todasViagens = await ObterTodasViagens(idUsuario);

            if (todasViagens.IsFailure)
                return Result.Failure<ViagensPorPagina>(todasViagens.Error);

            double quantidadeDePaginas = Math.Ceiling(todasViagens.Value.Count / _viagensPorPagina);
            List<ViagemDTO> viagens = todasViagens.Value
                .Skip((pagina - 1) * (int)_viagensPorPagina)
                .Take((int)_viagensPorPagina)
                .ToList();

            return new ViagensPorPagina()
            {
                Viagens = viagens,
                PaginaAtual = pagina,
                QuantidadeDePaginas = (int)quantidadeDePaginas
            };
        }

        public async Task<Result<DespesasPorPagina>> ObterTodasDespesasPaginadasPorTipo(int idViagem, int pagina, string stringTipoDespesa)
        {
            TiposDespesas tipoDespesa = ConverterStringParaEnumTipoDespesa(stringTipoDespesa);
            double quantidadeDePaginas = Math.Ceiling((await _viagemRepository.ObterDespesasPorTipo(idViagem, tipoDespesa)).Count / _despesasPorPagina);
            List<Despesa> despesas = await _viagemRepository.ObterDespesasPorTipo(idViagem, tipoDespesa);
            despesas = despesas
                .Skip((pagina - 1) * (int)_despesasPorPagina)
                .Take((int)_despesasPorPagina)
                .ToList();

            return new DespesasPorPagina()
            {
                Despesas = MappingDTOs.ConverterDTO(despesas),
                PaginaAtual = pagina,
                QuantidadeDePaginas = (int)quantidadeDePaginas
            };
        }


        public async Task<Result<List<DespesaPorCategoria>>> ObterTotalDasDespesasPorCategoria(int viagemId)
        {
            /*
            // Método para usar com o SQL Server
            List<DespesaPorCategoria> despesas = await _viagemRepository.ObterTotalDasDespesasPorCategoria(viagemId);
            
            return despesas;
            */

            //Método para usar com o SQLite
            List<Despesa> despesas = await _viagemRepository.ObterTodasDepesas(viagemId);

            var result = despesas            
            .GroupBy(despesa => despesa.TipoDespesa)
            .Select(agrupamento => new DespesaPorCategoria
            {
                TipoDespesa = agrupamento.Key,
                TotalDespesa = agrupamento.Sum(despesa => despesa.TotalDespesa)
            }).ToList();

            return result;            
        }


        public async Task<Result<ViagemDTO>> AdicionarViagem(ViagemDTO viagemDTO, int idFuncionario)
        {
            try
            {
                viagemDTO.StatusViagem = StatusViagem.Aberta;
                Viagem viagem = MappingDTOs.ConverterDTO(viagemDTO);

                if (viagem is null || viagemDTO.Id != 0) //Caso o Id não seja 0 irá dar erro na hora de adicionar a viagem
                    return Result.Failure<ViagemDTO>("Nenhuma viagem informada.");

                if ((await ViagemAbertaOuEmAndamento(idFuncionario)) is not null)
                    return Result.Failure<ViagemDTO>("Já existe uma viagem aberta ou em andamento.");

                viagem.VerificarDataInicialeFinal();

                Funcionario funcionario = await _funcionarioRepository.ObterPorId(viagem.IdFuncionario);

                if (funcionario is null)
                    return Result.Failure<ViagemDTO>("Funcionário não encontrado!");

                viagem.AdicionarFuncionario(funcionario);

                if (viagem.NomeViagem.Length < 4 || viagem.DescricaoViagem.Length < 4)
                    return Result.Failure<ViagemDTO>("Preencha os campos Nome e Descrição para a viagem. (Mínimo de 4 caracteres)");

                await _viagemRepository.Insert(viagem);

                return Result.Success(viagemDTO);
            }
            catch (Exception ex)
            {
                return Result.Failure<ViagemDTO>(ex.Message);
            }
        }

        public async Task<Result<ViagemDTO>> AlterarViagem(ViagemDTO viagemDTO, int idFuncionario)
        {
            try
            {
                Viagem viagem = MappingDTOs.ConverterDTO(viagemDTO);

                if (viagem is null || viagemDTO.Id <= 0)
                    return Result.Failure<ViagemDTO>("Nenhuma viagem informada.");

                Viagem? viagemAuxiliar = await ViagemAbertaOuEmAndamento(idFuncionario);

                if (viagemAuxiliar is null || viagemAuxiliar.Id != viagem.Id)
                    return Result.Failure<ViagemDTO>("Nenhuma viagem em Aberto ou Em Andamento, ou a viagem informada está Cancelada ou Encerrada.");

                if (viagemAuxiliar.Adiantamento != viagem.Adiantamento && viagemAuxiliar.StatusViagem != StatusViagem.Aberta)
                    return Result.Failure<ViagemDTO>("O Adiantamento incial não pode ser modificado.");

                if ((viagemAuxiliar.DataInicial != viagem.DataInicial || viagemAuxiliar.DataFinal != viagem.DataFinal) && viagemAuxiliar.StatusViagem != StatusViagem.Aberta)
                    return Result.Failure<ViagemDTO>("As datas incial e final não podem ser modificadas.");

                viagem.VerificarDataInicialeFinal();

                viagem.AdicionarFuncionario(await _funcionarioRepository.ObterPorId(viagemDTO.IdFuncionario));

                viagem.DefinirStatusViagem(viagemAuxiliar.StatusViagem); //O status é utilizado da forma como já está no banco por ele não ser mudado pelo usuário diretamente
                viagem.DefinirAdiantamento(viagem.Adiantamento); //Caso a viagem esteja em aberto será possível modificar o adiantamento

                await _viagemRepository.Update(viagem);
                return Result.Success(viagemDTO);
            }
            catch (Exception ex)
            {
                return Result.Failure<ViagemDTO>(ex.Message);
            }

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

        public async Task<Result<decimal>> ObterPrestacaoDeContas(int idViagem)
        {
            Viagem viagem = await _viagemRepository.ObterPorId(idViagem);
            if (viagem is null) return Result.Failure<decimal>("Viagem não encontrada");

            decimal prestacaoDeContas = viagem.GerarPrestacaoDeContas();

            await _viagemRepository.Update(viagem);

            return Result.Success(prestacaoDeContas);
        }

        public async Task<Result<ViagemDTO>> IniciarViagem(int idFuncionario)
        {
            var viagemAberta = await _viagemRepository.ObterViagemPorStatus(StatusViagem.Aberta, idFuncionario);

            if (viagemAberta is null || !viagemAberta.Any())
                return Result.Failure<ViagemDTO>("Não há nenhuma viagem Aberta");

            Viagem viagem = viagemAberta.First() ?? new();

            viagem.IniciarViagem();
            await _viagemRepository.Update(viagem);
            ViagemDTO viagemDTO = MappingDTOs.ConverterDTO(viagem);

            return Result.Success(viagemDTO);
        }

        public async Task<Result<ViagemDTO>> EncerrarViagem(int idFuncionario)
        {
            Viagem? viagem = await ViagemAbertaOuEmAndamento(idFuncionario);

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

        public async Task<Result<ViagemDTO>> CancelarViagem(int idFuncionario)
        {
            Viagem? viagem = await ViagemAbertaOuEmAndamento(idFuncionario);

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

        public async Task<Result<ViagemDTO?>> ObterViagemAbertaOuEmAndamento(int idFuncionario)
        {
            var viagem = await ViagemAbertaOuEmAndamento(idFuncionario);

            if (viagem is null)
                return Result.Failure<ViagemDTO?>("Nenhuma viagem em Aberta ou em Andamento encontrada.");

            ViagemDTO viagemDTO = MappingDTOs.ConverterDTO(viagem);

            return Result.Success<ViagemDTO?>(viagemDTO);
        }

        //Verifica se já existe alguma viagem em andamento, caso não existe pode prosseguir com a criação.        
        private async Task<Viagem?> ViagemAbertaOuEmAndamento(int idFuncionario)
        {
            var viagem = await _viagemRepository.ObterViagemPorStatus(StatusViagem.Aberta, idFuncionario);
            if (!viagem.Any())
                viagem = await _viagemRepository.ObterViagemPorStatus(StatusViagem.EmAndamento, idFuncionario);

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
        /*
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
        */
            /*
            private async Task<List<Viagem>> AdicionarFuncionarioParaAViagem(List<Viagem> viagens)
            {
                foreach (var viagem in viagens)
                {
                    Funcionario funcionario = await _funcionarioRepository.ObterPorId(viagem.IdFuncionario);
                    viagem.AdicionarFuncionario(funcionario);
                }

                return viagens;
            }
            */
            private static TiposDespesas ConverterStringParaEnumTipoDespesa(string tipoDespesa)
        {
            return tipoDespesa switch
            {
                "Hospedagem" => TiposDespesas.Hospedagem,
                "Deslocamento" => TiposDespesas.Deslocamento,
                "Alimentação" => TiposDespesas.Alimentação,
                "Passagem" => TiposDespesas.Passagem,
                _ => throw new Exception("Valor não é valido para tipo de despesa"),
            };
        }
    }
}
