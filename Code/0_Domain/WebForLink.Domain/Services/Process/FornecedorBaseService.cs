using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFornecedorBaseWebForLinkService : IService<FORNECEDORBASE>
    {
        FORNECEDORBASE Buscar(Expression<Func<FORNECEDORBASE, bool>> filtro);
        FORNECEDORBASE BuscarPorID(int id);
        FORNECEDORBASE BuscarPorIdPreCadastro(int id);
        FORNECEDORBASE BuscarPorIDContratanteID(int id, int contratanteId);
        List<FORNECEDORBASE> ListarPorDocumento(string documentoFornecedor);
        FORNECEDORBASE BuscarPorId(int id);
        BloqueioDTO ModificarStatusBloqueio(int id, int statusBloqueio, int usuarioId, int contratanteId);

        List<FORNECEDORBASE> IncluirFornecedoresBase(FORNECEDORBASE_IMPORTACAO importacao, int contratanteId,
            List<FORNECEDORBASE> fornecedoresImportados);

        RetornoPesquisa<FORNECEDORBASE> PesquisarFornecedores(ImportacaoFornecedoresFiltrosDTO filtro, int pagina,
            int tamanhoPagina);

        FornecedorBaseTopoDTO PesquisarFornecedoresBaseTopo(int contratanteId);
        void CategorizarFornecedores(int[] fornecedores, int categoriaId);
        void AtivarExecucaoRobo(int[] fornecedores);
        void Atualizar(FORNECEDORBASE fornecedorBase);
        void Excluir(int fornecedorBaseId);
        TimelineDTO RetornarIndicesTimeLine(int contratanteId);
    }

    public class FornecedorBaseWebForLinkService : Service<FORNECEDORBASE>, IFornecedorBaseWebForLinkService
    {
        private readonly IFluxoWebForLinkRepository _fluxoRepository;
        private readonly IFornecedorBaseWebForLinkRepository _fornecedorBaseRepository;
        private readonly IFornecedorWebForLinkRepository _fornecedorRepository;
        private readonly IFornecedorBaseImportacaoWebForLinkRepository _fornecedorRepositoryBaseImportacao;
        private readonly IPapelWebForLinkService _papelBP;
        private readonly ISolicitacaoBloqueioWebForLinkRepository _solicitacaoBloqueioRepository;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;

        public FornecedorBaseWebForLinkService(IFornecedorWebForLinkRepository fornecedor,
            IFornecedorBaseWebForLinkRepository fornecedorBase,
            IFluxoWebForLinkRepository fluxo,
            ISolicitacaoWebForLinkRepository solicitacao,
            ISolicitacaoBloqueioWebForLinkRepository solicitacaoBloqueio,
            IFornecedorBaseImportacaoWebForLinkRepository fornecedorBaseImportacao,
            IPapelWebForLinkService papel) : base(fornecedorBase)
        {
            try
            {
                _fornecedorRepository = fornecedor;
                _fornecedorBaseRepository = fornecedorBase;
                _fluxoRepository = fluxo;
                _solicitacaoRepository = solicitacao;
                _solicitacaoBloqueioRepository = solicitacaoBloqueio;
                _fornecedorRepositoryBaseImportacao = fornecedorBaseImportacao;
                _papelBP = papel;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FORNECEDORBASE Buscar(Expression<Func<FORNECEDORBASE, bool>> filtro)
        {
            try
            {
                return _fornecedorBaseRepository.Get(filtro);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o PJPF Base por ID", ex);
            }
        }

        public FORNECEDORBASE BuscarPorID(int id)
        {
            try
            {
                return _fornecedorBaseRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o PJPF Base por ID", ex);
            }
        }

        public FORNECEDORBASE BuscarPorIdPreCadastro(int id)
        {
            try
            {
                return _fornecedorBaseRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contratanteId"></param>
        /// <returns></returns>
        public FORNECEDORBASE BuscarPorIDContratanteID(int id, int contratanteId)
        {
            try
            {
                return _fornecedorBaseRepository.Get(x => x.CONTRATANTE_ID == contratanteId && x.ID == id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o PJPF Base por Id e Contratante", ex);
            }
        }

        public List<FORNECEDORBASE> ListarPorDocumento(string documentoFornecedor)
        {
            try
            {
                return _fornecedorBaseRepository
                    .Find(x => x.CNPJ == documentoFornecedor || x.CPF == documentoFornecedor)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o PJPF Base por CNPJ ou CPF", ex);
            }
        }

        public FORNECEDORBASE BuscarPorId(int id)
        {
            try
            {
                return _fornecedorBaseRepository.Get(id);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de fornecedores base", e);
            }
        }

        public BloqueioDTO ModificarStatusBloqueio(int id, int statusBloqueio, int usuarioId, int contratanteId)
        {
            try
            {
                if (statusBloqueio == 0)
                    throw new ServiceWebForLinkException("statusBloqueio não pode ser 0.");

                var retorno = new BloqueioDTO();
                var selecao = _fornecedorBaseRepository.Get(id);
                var fluxoId = _fluxoRepository.Find(y => y.FLUXO_TP_ID == 110 &&
                                                         y.CONTRATANTE_ID == contratanteId).FirstOrDefault().ID;

                //Alterar valor de decisão bloqueio
                selecao.DECISAO_BLOQUEIO = statusBloqueio;

                _fornecedorBaseRepository.Update(selecao);

                var solicitacao = new SOLICITACAO();

                //No caso de Bloqueio Manual
                if (statusBloqueio == 3)
                {
                    //WFD_solicitacao
                    solicitacao.CONTRATANTE_ID = selecao.CONTRATANTE_ID;
                    solicitacao.DT_PRAZO = DateTime.Now.AddDays(15);
                    solicitacao.DT_PRORROGACAO_PRAZO = null;
                    solicitacao.FLUXO_ID = fluxoId;
                    solicitacao.MOTIVO = null;
                    solicitacao.MOTIVO_PRORROGACAO = null;
                    solicitacao.PJPF_BASE_ID = selecao.ID;
                    solicitacao.PJPF_ID = null;
                    solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
                    solicitacao.SOLICITACAO_STATUS_ID = 1;
                    solicitacao.TP_PJPF = null;
                    solicitacao.USUARIO_ID = usuarioId;
                    _solicitacaoRepository.Add(solicitacao);

                    var bloqueioSol = new SOLICITACAO_BLOQUEIO
                    {
                        WFD_SOLICITACAO = solicitacao,
                        BLQ_COMPRAS_TODAS_ORG_COMPRAS = null,
                        BLQ_LANCAMENTO_EMP = true,
                        BLQ_LANCAMENTO_TODAS_EMP = null,
                        BLQ_MOTIVO_DSC = "",
                        BLQ_QUALIDADE_FUNCAO_BQL_ID = 2
                    };
                    _solicitacaoBloqueioRepository.Add(bloqueioSol);

                    retorno.UsuarioId = usuarioId;
                    //PapelService papelBP = new PapelService();
                    retorno.PapelAtual = _papelBP.BuscarPorContratanteETipoPapel(contratanteId, 10).ID;
                    retorno.FluxoId = fluxoId;
                    retorno.ContratanteId = contratanteId;
                }

                //Salvar

                retorno.SolicitacaoId = solicitacao.ID;

                return retorno;
            }
                //catch (DbEntityValidationException e)
                //{
                //    foreach (var eve in e.EntityValidationErrors)
                //    {
                //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        foreach (var ve in eve.ValidationErrors)
                //        {
                //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //                ve.PropertyName, ve.ErrorMessage);
                //        }
                //    }
                //    throw;
                //}
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de fornecedores base", e);
            }
        }

        public List<FORNECEDORBASE> IncluirFornecedoresBase(FORNECEDORBASE_IMPORTACAO importacao, int contratanteId,
            List<FORNECEDORBASE> fornecedoresImportados)
        {
            var fornecedoresBaseCNPJouCPF = _fornecedorBaseRepository
                .Find(f => f.CONTRATANTE_ID == contratanteId)
                .Select(x => new[] {x.CNPJ, x.CPF})
                .SelectMany(x => x.Where(y => !string.IsNullOrEmpty(y)))
                .ToList();

            var fornecedoresJaCadastrados = new List<FORNECEDORBASE>();

            foreach (var fornecedor in fornecedoresImportados)
            {
                fornecedor.WFD_PJPF_BASE_IMPORTACAO = importacao;
                var cnpjOuCPF = string.Empty;
                var tipoFornecedor = 0;

                if (!string.IsNullOrEmpty(fornecedor.CNPJ))
                {
                    cnpjOuCPF = fornecedoresBaseCNPJouCPF.FirstOrDefault(x => x == fornecedor.CNPJ);
                    tipoFornecedor = (int) EnumTiposFornecedor.EmpresaNacional;
                }
                else if (!string.IsNullOrEmpty(fornecedor.CPF))
                {
                    cnpjOuCPF = fornecedoresBaseCNPJouCPF.FirstOrDefault(x => x == fornecedor.CPF);
                    tipoFornecedor = (int) EnumTiposFornecedor.PessoaFisica;
                }

                if (!string.IsNullOrEmpty(cnpjOuCPF))
                    fornecedoresJaCadastrados.Add(fornecedor);
                else
                {
                    fornecedor.PJPF_TIPO = tipoFornecedor;
                    fornecedor.DT_IMPORTACAO = DateTime.Now;
                    importacao.WFD_PJPF_BASE.Add(fornecedor);
                    //Db.Entry(fornecedor).State = EntityState.Added;
                    //fornecedorBaseBM.Add(fornecedor);
                }
            }
            _fornecedorRepositoryBaseImportacao.Add(importacao);

            //Processo.Finalizar();

            return fornecedoresJaCadastrados;
        }

        //public RetornoPesquisa<FORNECEDORBASE> PesquisarFornecedores(ImportacaoFornecedoresFiltrosDTO filtro, int pagina, int tamanhoPagina)
        //{
        //    var predicate = PredicateBuilder.New<FORNECEDORBASE>();
        //    predicate = predicate.And(x => x.CONTRATANTE_ID == filtro.ContratanteId && x.PRECADASTRO == false);

        //    if (filtro.Bloqueados.HasValue)
        //        predicate = (filtro.Bloqueados != 0)
        //                ? predicate.And(x => x.ROBO_ID != null &&
        //                    x.DECISAO_BLOQUEIO == filtro.Bloqueados &&
        //                    !(
        //                        x.ROBO.RF_SIT_CADASTRAL_CNPJ.Contains("ATIVA") &&
        //                        (x.ROBO.SINT_IE_SITU_CADASTRAL.Contains("HABILITADO ATIVO") ||
        //                            x.ROBO.SINT_IE_SITU_CADASTRAL.Contains("HABILITADO"))
        //                    )
        //                    )
        //                : predicate.And(x => x.ROBO_ID != null &&
        //                    x.DECISAO_BLOQUEIO == null &&
        //                    !(
        //                        x.ROBO.RF_SIT_CADASTRAL_CNPJ.Contains("ATIVA") &&
        //                        (x.ROBO.SINT_IE_SITU_CADASTRAL.Contains("HABILITADO ATIVO") ||
        //                            x.ROBO.SINT_IE_SITU_CADASTRAL.Contains("HABILITADO"))
        //                    )
        //                    );

        //    if (filtro.Prorrogados.HasValue)
        //        predicate = (!filtro.Prorrogados.Value)
        //            ? predicate.And(x => x.WFD_PJPF_BASE_CONVITE.Any(y => !y.WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO.Any()))
        //            : predicate.And(x => x.WFD_PJPF_BASE_CONVITE.Any(y => y.WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO.Any()));

        //    if (filtro.Aprovados.HasValue)
        //    {
        //        if (filtro.Aprovados != 1)
        //            predicate = predicate.And(x => x.WFD_SOLICITACAO.Any(y => y.WFD_SOLICITACAO_PRORROGACAO.Any()));
        //        //predicate = (filtro.Aprovados == 3)
        //        //    ? predicate.And(x => x.WFD_SOLICITACAO.Any(y => y.WFD_SOLICITACAO_PRORROGACAO.Any(w => w.APROVADO == true)))
        //        //    : predicate.And(x => x.WFD_SOLICITACAO.Any(y => y.WFD_SOLICITACAO_PRORROGACAO.Any(w => w.APROVADO == false)));
        //        else
        //            predicate = predicate.And(x => x.WFD_SOLICITACAO.Any(y => y.WFD_SOLICITACAO_PRORROGACAO.Any(w => w.APROVADO == null)));
        //    }

        //    if (filtro.Categorizados.HasValue)
        //        predicate = (!filtro.Categorizados.Value)
        //            ? predicate.And(x => x.CATEGORIA_ID == null)
        //            : predicate.And(x => x.CATEGORIA_ID != null);

        //    if (filtro.Validados.HasValue)
        //        predicate = (!filtro.Validados.Value)
        //            ? predicate.And(x => x.EXECUTA_ROBO == null)
        //            : predicate.And(x => x.EXECUTA_ROBO != null);

        //    if (filtro.Convidados.HasValue)
        //    {
        //        predicate = (!filtro.Convidados.Value)
        //            ? predicate.And(x => !x.WFD_PJPF_BASE_CONVITE.Any() && x.DECISAO_BLOQUEIO != 3)
        //            : predicate.And(x => x.WFD_PJPF_BASE_CONVITE.Any() && x.DECISAO_BLOQUEIO != 3);

        //        predicate = predicate.And(x => x.CATEGORIA_ID != null);
        //    }

        //    if (filtro.CategoriaId.HasValue)
        //        predicate = predicate.And(x => x.CATEGORIA_ID == filtro.CategoriaId);

        //    if (filtro.ArquivoImportado.HasValue)
        //        predicate = predicate.And(x => x.WFD_PJPF_BASE_IMPORTACAO.ID == filtro.ArquivoImportado);

        //    if (!string.IsNullOrEmpty(filtro.CNPJ))
        //        predicate = predicate.And(x => x.CNPJ == filtro.CNPJ);

        //    if (!string.IsNullOrEmpty(filtro.RazaoSocial))
        //        predicate = predicate.And(x => x.RAZAO_SOCIAL == filtro.RazaoSocial);

        //    if (!string.IsNullOrEmpty(filtro.CPF))
        //        predicate = predicate.And(x => x.CPF == filtro.CPF);

        //    if (!string.IsNullOrEmpty(filtro.Nome))
        //        predicate = predicate.And(x => x.NOME == filtro.Nome);

        //    try
        //    {
        //        return _fornecedorBaseRepository.Pesquisar(predicate, tamanhoPagina, pagina, x => x.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Fornecedores", ex);
        //    }
        //}

        public FornecedorBaseTopoDTO PesquisarFornecedoresBaseTopo(int contratanteId)
        {
            var fornecedoresBaseTopo = new FornecedorBaseTopoDTO();

            try
            {
                var listaBase = _fornecedorBaseRepository.All();
                fornecedoresBaseTopo.TotalSemCategoria =
                    listaBase.Count(
                        x => !x.CATEGORIA_ID.HasValue && x.CONTRATANTE_ID == contratanteId && x.PRECADASTRO == false);
                fornecedoresBaseTopo.TotalSemValidacao =
                    listaBase.Count(
                        x => !x.EXECUTA_ROBO.HasValue && x.CONTRATANTE_ID == contratanteId && x.PRECADASTRO == false);
                fornecedoresBaseTopo.TotalSemConvite =
                    listaBase.Count(
                        x =>
                            !x.WFD_PJPF_BASE_CONVITE.Any() && x.CATEGORIA_ID.HasValue &&
                            x.CONTRATANTE_ID == contratanteId && x.PRECADASTRO == false);

                #region Total sem prazo

                var indexador = 0;
                var listagem = _fornecedorBaseRepository
                    .Find(x => x.CONTRATANTE_ID == contratanteId && x.PRECADASTRO == false)
                    .ToList();
                foreach (var item in listagem)
                {
                    var dias = item.WFD_CONTRATANTE.WFD_CONTRATANTE_CONFIG.PRAZO_ENTREGA_FICHA;
                    var qtd =
                        item.WFD_PJPF_BASE_CONVITE.Count(
                            x => x.WFD_SOLICITACAO.SOLICITACAO_DT_CRIA.AddDays(dias) >= DateTime.Now);
                    indexador = +qtd;
                }

                #endregion

                fornecedoresBaseTopo.TotalSemPrazo = indexador;
                fornecedoresBaseTopo.TotalEmProrrogacao =
                    listagem.Count(
                        x => x.WFD_PJPF_BASE_CONVITE.Any(y => y.WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO.Any()));
                fornecedoresBaseTopo.TotalSemCarga = 0;
                fornecedoresBaseTopo.TotalSemDados = 0;
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar as informações do topo.", e);
            }

            return fornecedoresBaseTopo;
        }

        public void CategorizarFornecedores(int[] fornecedores, int categoriaId)
        {
            try
            {
                //PARA UPDATES EM MASSA UTILIZAR DIRETAMENTE O COMANDO SQL. O ENTITYFRAMWORK 6.0 AINDA NÃO SUPORTA ESTA TAREFA.
                //UTILIZAR O PADRAO DE UPDATE DO ENTITY PARA CARGA EM MASSA NÃO É PERFOMATICO.
                var fornecedoresIds = String.Join(",", fornecedores);
                var query = String.Format("UPDATE dbo.FORNECEDORBASE SET CATEGORIA_ID = {0} WHERE ID in ({1})",
                    categoriaId, fornecedoresIds);
                //TODO: ABAIXO
                //_fornecedorRepository.ExecutarQuery(query);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao categorizar o fornecedor.", e);
            }
        }

        public void AtivarExecucaoRobo(int[] fornecedores)
        {
            //PARA UPDATES EM MASSA UTILIZAR DIRETAMENTE O COMANDO SQL. O ENTITYFRAMWORK 6.0 AINDA NÃO SUPORTA ESTA TAREFA.
            //UTILIZAR O PADRAO DE UPDATE DO ENTITY PARA CARGA EM MASSA NÃO É PERFOMATICO.
            var fornecedoresIds = String.Join(",", fornecedores);
            var query =
                String.Format(
                    "UPDATE dbo.FORNECEDORBASE SET EXECUTA_ROBO = 1, DT_SOLICITACAO_ROBO = GETDATE() WHERE ID in ({0})",
                    fornecedoresIds);

            try
            {
                //TODO
                //_fornecedorRepository.ExecutarQuery(query);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao ativar a execução do Robô.", e);
            }
        }

        public void Atualizar(FORNECEDORBASE fornecedorBase)
        {
            try
            {
                if (fornecedorBase.ID == 0)
                    _fornecedorBaseRepository.Add(fornecedorBase);
                else
                {
                    var fornecedorBanco = _fornecedorBaseRepository.Get(fornecedorBase.ID);
                    //FORNECEDORBASE fornecedorMescla = _fornecedorBaseRepository.MesclarObjetos(fornecedorBanco, fornecedorBase);
                    //_fornecedorBaseRepository.Update(fornecedorMescla);
                }
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao atualizar o fornecedor.", e);
            }
        }

        public void Excluir(int fornecedorBaseId)
        {
            try
            {
                var fornecedorBase = _fornecedorBaseRepository.Get(fornecedorBaseId);

                if (fornecedorBase != null)
                {
                    _fornecedorBaseRepository.Delete(fornecedorBase);
                }
                else
                {
                    throw new ServiceWebForLinkException(string.Format("O fornecedor {0} não foi encontrado.",
                        fornecedorBaseId));
                }
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao excluir o fornecedor.", e);
            }
        }

        public TimelineDTO RetornarIndicesTimeLine(int contratanteId)
        {
            return new TimelineDTO
            {
                Importados =
                    _fornecedorBaseRepository.Find(x => x.CONTRATANTE_ID == contratanteId && x.PRECADASTRO == false)
                        .Count(),
                Validados =
                    _fornecedorBaseRepository.Find(
                        x => x.CONTRATANTE_ID == contratanteId && x.EXECUTA_ROBO != null && x.PRECADASTRO == false)
                        .Count(),
                Categorizados =
                    _fornecedorBaseRepository.Find(
                        x => x.CONTRATANTE_ID == contratanteId && x.CATEGORIA_ID != null && x.PRECADASTRO == false)
                        .Count(),
                Convidados =
                    _fornecedorBaseRepository.Find(
                        x =>
                            x.CONTRATANTE_ID == contratanteId && x.WFD_PJPF_BASE_CONVITE.Any() && x.PRECADASTRO == false)
                        .Count(),
                Bloqueados = 0, //TODO: -- A definir
                Respondido =
                    _fornecedorBaseRepository.Find(
                        x =>
                            x.CONTRATANTE_ID == contratanteId && x.PRECADASTRO == false &&
                            x.WFD_SOLICITACAO.Any(
                                y =>
                                    y.WFD_SOLICITACAO_TRAMITE.Any(
                                        z => z.Papel.PAPEL_TP_ID == 50 && z.SOLICITACAO_STATUS_ID == 3))).Count(),
                Reprovados =
                    _fornecedorBaseRepository.Find(
                        x =>
                            x.CONTRATANTE_ID == contratanteId && x.PRECADASTRO == false &&
                            x.WFD_SOLICITACAO.Any(y => y.SOLICITACAO_STATUS_ID == 3)).Count(),
                Criados =
                    _fornecedorBaseRepository.Find(
                        x =>
                            x.CONTRATANTE_ID == contratanteId && x.PRECADASTRO == false &&
                            x.WFD_SOLICITACAO.Any(y => y.SOLICITACAO_STATUS_ID == 4)).Count()
            };
        }

        public RetornoPesquisa<FORNECEDORBASE> PesquisarFornecedores(ImportacaoFornecedoresFiltrosDTO filtro, int pagina,
            int tamanhoPagina)
        {
            return _fornecedorBaseRepository.Pesquisar(x => x.ATIVO, pagina, tamanhoPagina, x => x.ID);
        }

        public void Dispose()
        {
        }
    }

    public class BloqueioDTO
    {
        public int ContratanteId { get; set; }
        public int SolicitacaoId { get; set; }
        public int FluxoId { get; set; }
        public int PapelAtual { get; set; }
        public int UsuarioId { get; set; }
    }
}