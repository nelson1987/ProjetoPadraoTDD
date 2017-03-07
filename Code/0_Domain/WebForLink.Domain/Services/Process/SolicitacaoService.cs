using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class SolicitacaoWebForLinkService : Service<SOLICITACAO>, ISolicitacaoWebForLinkService
    {
        private readonly IContratanteConfiguracaoWebForLinkRepository _contratanteConfig;
        private readonly IFluxoWebForLinkService _fluxoBp;
        private readonly IFornecedorBaseConviteWebForLinkRepository _fornecedorBaseRepositoryConvite;
        private readonly IFornecedorListaDocumentosWebForLinkService _listaDoc;
        private readonly IFornecedorBaseWebForLinkService _pjpfBaseBP;
        private readonly IRoboWebForLinkRepository _roboFornecedorRepository;
        private readonly ISolicitacaoBloqueioWebForLinkRepository _solicitacaoBloqueioRepository;
        private readonly ISolicitacaoCadasatroFornecedorWebForLinkRepository _solicitacaoCadastroFornecedorRepository;
        private readonly ISolicitacaoModificacaoContatoWebForLinkRepository _solicitacaoContatoRepository;
        private readonly ISolicitacaoDocumentoWebForLinkRepository _solicitacaoDocumentoRepository;
        private readonly ISolicitacaoEnderecoWebForLinkRepository _solicitacaoEnderecoRepository;
        private readonly ISolicitacaoMensagemWebForLinkRepository _solicitacaoMensagemRepository;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;
        private readonly ISolicitacaoServicoMaterialWebForLinkRepository _solicitacaoUnspscRepository;

        public SolicitacaoWebForLinkService(
            ISolicitacaoWebForLinkRepository solicitacao,
            IRoboWebForLinkRepository roboFornecedor,
            IContratanteConfiguracaoWebForLinkRepository contratanteConfig,
            ISolicitacaoCadasatroFornecedorWebForLinkRepository solicitacaoCadastroFornecedor,
            ISolicitacaoMensagemWebForLinkRepository solicitacaoMensagem,
            ISolicitacaoModificacaoContatoWebForLinkRepository solicitacaoContato,
            ISolicitacaoDocumentoWebForLinkRepository solicitacaoDocumento,
            IFornecedorBaseConviteWebForLinkRepository fornecedorBaseConvite,
            ISolicitacaoEnderecoWebForLinkRepository solicitacaoEndereco,
            ISolicitacaoBloqueioWebForLinkRepository solicitacaoBloqueio,
            ISolicitacaoServicoMaterialWebForLinkRepository solicitacaoUnspsc,
            IFornecedorBaseWebForLinkService pjpfBase,
            IFluxoWebForLinkService fluxo,
            IFornecedorListaDocumentosWebForLinkService listaDoc) : base(solicitacao)
        {
            try
            {
                _listaDoc = listaDoc;
                _solicitacaoRepository = solicitacao;
                _roboFornecedorRepository = roboFornecedor;
                _contratanteConfig = contratanteConfig;
                _solicitacaoCadastroFornecedorRepository = solicitacaoCadastroFornecedor;
                _solicitacaoMensagemRepository = solicitacaoMensagem;
                _solicitacaoContatoRepository = solicitacaoContato;
                _solicitacaoDocumentoRepository = solicitacaoDocumento;
                _fornecedorBaseRepositoryConvite = fornecedorBaseConvite;
                _solicitacaoEnderecoRepository = solicitacaoEndereco;
                _solicitacaoBloqueioRepository = solicitacaoBloqueio;
                _solicitacaoUnspscRepository = solicitacaoUnspsc;
                _pjpfBaseBP = pjpfBase;
                _fluxoBp = fluxo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public int[] BuscarSolicitacaoAguardandoCarga()
        {
            return _solicitacaoRepository.BuscarSolicitacaoAguardandoCarga();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public int[] BuscarSolicitacaoAguardandoRetornoCarga()
        {
            return _solicitacaoRepository.BuscarSolicitacaoAguardandoRetornoCarga();
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public int BuscarTipoFluxoId(int solicitacaoId)
        {
            try
            {
                return _solicitacaoRepository.BuscarTipoFluxoId(solicitacaoId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar tipo de fluxo da solicitacao", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdIncluindoFluxo(int id)
        {
            try
            {
                return _solicitacaoRepository.BuscarPorIdIncluindoFluxo(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarAprovacaoPorId(int id)
        {
            try
            {
                return _solicitacaoRepository.BuscarAprovacaoPorId(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarSolicitacaoFinalizaCriacaoFornecedor(int solicitacaoId)
        {
            try
            {
                return _solicitacaoRepository.BuscarSolicitacaoFinalizaCriacaoFornecedor(solicitacaoId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="contratanteId"></param>
        /// <param name="fornecedorId"></param>
        /// <param name="tipoFluxoId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public List<int> ListarPorId(int contratanteId, int? fornecedorId, int tipoFluxoId, int statusId)
        {
            try
            {
                return _solicitacaoRepository.ListarPorId(contratanteId, fornecedorId, tipoFluxoId, statusId);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar a solicitação.", e);
            }
        }

        public List<SOLICITACAO> ListarSolicitacaoAprovadaPorId(int aguardando)
        {
            throw new NotImplementedException();
        }

        public void ConvidarFornecedorComSolicitacao(SOLICITACAO solicitacao,
            SolicitacaoCadastroFornecedor solicitacaoCadastroPjPf,
            SolicitacaoModificacaoDadosContato contato, object o, List<SolicitacaoDeDocumentos> docs,
            FORNECEDORBASE_CONVITE convite,
            SOLICITACAO_MENSAGEM mensagem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorSolicitacaoId(int id)
        {
            try
            {
                return _solicitacaoRepository.BuscarPorSolicitacaoId(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdComSolicitacaoCadastroPjpf(int id)
        {
            try
            {
                return _solicitacaoRepository.BuscarPorIdComSolicitacaoCadastroPJPF(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("ocorreu um erro ao buscar a solicitação.", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdControleSolicitacoes(int id)
        {
            try
            {
                return _solicitacaoRepository.BuscarPorIdControleSolicitacoes(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdComFornecedoresDireto(int id)
        {
            try
            {
                return _solicitacaoRepository.BuscarPorIdComFornecedoresDireto(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdDocumentosSolicitados(int id)
        {
            try
            {
                return _solicitacaoRepository.BuscarPorIdDocumentosSolicitados(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdComDocumentos(int id)
        {
            try
            {
                return _solicitacaoRepository.BuscarPorIdComDocumentos(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<SOLICITACAO> ListarSolicitacaoCarga(int idContratante)
        {
            return _solicitacaoRepository.ListarSolicitacaoCarga(idContratante);
        }

        /// <summary>
        /// </summary>
        /// <param name="idSolicitacao"></param>
        /// <returns></returns>
        public List<SOLICITACAO> ListarTodasSolicitacoesAprovadas(int idSolicitacao)
        {
            return _solicitacaoRepository.ListarTodasSolicitacoesAprovadas(idSolicitacao);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public SOLICITACAO Alterar(SOLICITACAO solicitacao)
        {
            try
            {
                _solicitacaoRepository.Update(solicitacao);
                return solicitacao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a Solicitação para Ficha Cadastral", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="SolicitacaoID"></param>
        /// <param name="ContratanteID"></param>
        /// <param name="FluxoID"></param>
        /// <param name="motivoReprovacao"></param>
        /// <param name="usuarioId"></param>
        public void AlterarAprovacao(int SolicitacaoID, int ContratanteID, int FluxoID, string motivoReprovacao,
            int usuarioId)
        {
            var solicitacao = _solicitacaoRepository.Get(SolicitacaoID);
            solicitacao.CONTRATANTE_ID = ContratanteID;
            solicitacao.FLUXO_ID = FluxoID;
            solicitacao.MOTIVO = motivoReprovacao;
            solicitacao.USUARIO_ID = usuarioId;
            _solicitacaoRepository.Update(solicitacao);
        }

        public SOLICITACAO AlterarSolicitacaoMensagem(SOLICITACAO solicitacao)
        {
            if (solicitacao.ID == 0)
                _solicitacaoRepository.Add(solicitacao);
            else
            {
                var banco = _solicitacaoRepository.Get(solicitacao.ID);
                banco.CONTRATANTE_ID = solicitacao.CONTRATANTE_ID;
                banco.FLUXO_ID = solicitacao.FLUXO_ID;
                banco.SOLICITACAO_DT_CRIA = solicitacao.SOLICITACAO_DT_CRIA;
                banco.USUARIO_ID = solicitacao.USUARIO_ID;
                banco.SOLICITACAO_STATUS_ID = solicitacao.SOLICITACAO_STATUS_ID;
                banco.MOTIVO = solicitacao.MOTIVO;
                banco.PJPF_ID = solicitacao.PJPF_ID;
                banco.TP_PJPF = solicitacao.TP_PJPF;
                banco.DT_PRAZO = solicitacao.DT_PRAZO;
                banco.DT_PRORROGACAO_PRAZO = solicitacao.DT_PRORROGACAO_PRAZO;
                banco.MOTIVO_PRORROGACAO = solicitacao.MOTIVO_PRORROGACAO;
                banco.PJPF_BASE_ID = solicitacao.PJPF_BASE_ID;
                banco.ROBO_EXECUTADO = solicitacao.ROBO_EXECUTADO;
                banco.ROBO_TENTATIVAS_EXCEDIDAS = solicitacao.ROBO_TENTATIVAS_EXCEDIDAS;
                banco.SolicitacaoDeDocumentos = solicitacao.SolicitacaoDeDocumentos;
                _solicitacaoRepository.Update(banco);
            }
            return solicitacao;
        }

        public SOLICITACAO BuscarPorId(int id)
        {
            try
            {
                return _solicitacaoRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        public SOLICITACAO Buscar(Expression<Func<SOLICITACAO, bool>> filtro)
        {
            try
            {
                return _solicitacaoRepository.Find(filtro).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <returns></returns>
        public SOLICITACAO InserirSolicitacao(SOLICITACAO solicitacao)
        {
            try
            {
                solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
                solicitacao.PJPF_ID = solicitacao.PJPF_ID == 0 ? null : solicitacao.PJPF_ID;
                solicitacao.MOTIVO = null;
                solicitacao.TP_PJPF = null;
                solicitacao.DT_PRAZO =
                    DateTime.Now.AddDays(_contratanteConfig.Get(solicitacao.CONTRATANTE_ID).PRAZO_ENTREGA_FICHA);
                _solicitacaoRepository.Add(solicitacao);
                return solicitacao;
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao incluir a solicitação.", e);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <param name="documentos"></param>
        /// <returns></returns>
        public SOLICITACAO InserirSolicitacaoDocumentos(SOLICITACAO solicitacao,
            List<SolicitacaoDeDocumentos> documentos)
        {
            try
            {
                solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
                solicitacao.PJPF_ID = solicitacao.PJPF_ID == 0 ? null : solicitacao.PJPF_ID;
                solicitacao.MOTIVO = null;
                solicitacao.TP_PJPF = null;
                solicitacao.DT_PRAZO =
                    DateTime.Now.AddDays(_contratanteConfig.Get(solicitacao.CONTRATANTE_ID).PRAZO_ENTREGA_FICHA);
                _solicitacaoRepository.Add(solicitacao);
                documentos.ForEach(x =>
                {
                    x.WFD_SOLICITACAO = solicitacao;
                    _solicitacaoDocumentoRepository.Add(documentos);
                });
                return solicitacao;
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao incluir a solicitação.", e);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <param name="cadastro"></param>
        /// <param name="contato"></param>
        /// <param name="robo"></param>
        /// <param name="documentos"></param>
        /// <returns></returns>
        public SOLICITACAO IncluirNovaSolicitacaoFornecedor(SOLICITACAO solicitacao,
            SolicitacaoCadastroFornecedor cadastro, SolicitacaoModificacaoDadosContato contato,
            ROBO robo, List<SolicitacaoDeDocumentos> documentos)
        {
            try
            {
                _solicitacaoRepository.Add(solicitacao);
                if (robo != null)
                    _roboFornecedorRepository.Add(robo);
                _solicitacaoCadastroFornecedorRepository.Add(cadastro);
                if (contato != null)
                    _solicitacaoContatoRepository.Add(contato);
                _solicitacaoDocumentoRepository.Add(documentos);

                return solicitacao;
            }
                //catch (DbEntityValidationException e)
                //{
                //    foreach (var eve in e.EntityValidationErrors)
                //    {
                //        //Log.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        //foreach (var ve in eve.ValidationErrors)
                //        //{
                //        //    Log.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                //        //}
                //    }
                //    throw;
                //}
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <param name="cadastro"></param>
        /// <param name="contato"></param>
        /// <param name="robo"></param>
        /// <param name="documentos"></param>
        /// <param name="convite"></param>
        /// <returns></returns>
        public SOLICITACAO ConvidarFornecedorComSolicitacao(SOLICITACAO solicitacao,
            SolicitacaoCadastroFornecedor cadastro, SolicitacaoModificacaoDadosContato contato,
            ROBO robo, List<SolicitacaoDeDocumentos> documentos, FORNECEDORBASE_CONVITE convite,
            SOLICITACAO_MENSAGEM mensagem)
        {
            try
            {
                _solicitacaoRepository.Add(solicitacao);
                if (robo != null)
                    _roboFornecedorRepository.Add(robo);
                _solicitacaoCadastroFornecedorRepository.Add(cadastro);
                if (contato != null)
                    _solicitacaoContatoRepository.Add(contato);
                _solicitacaoDocumentoRepository.Add(documentos);
                _fornecedorBaseRepositoryConvite.Add(convite);
                _solicitacaoMensagemRepository.Add(mensagem);
                return solicitacao;
            }
                //catch (DbEntityValidationException e)
                //{
                //    foreach (var eve in e.EntityValidationErrors)
                //    {
                //        //Log.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        //foreach (var ve in eve.ValidationErrors)
                //        //{
                //        //    Log.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                //        //}
                //    }
                //    throw;
                //}
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new ServiceWebForLinkException("Erro ao tentar incluir uma solicitação.", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cadastro"></param>
        /// <returns></returns>
        public SOLICITACAO CadastrarSolicitacaoPreCadastro(int idPjPfBase, CadastrarSolicitacaoDTO cadastro)
        {
            var solicitacao = new SOLICITACAO();
            try
            {
                var pjpfBase = _pjpfBaseBP.BuscarPorIdPreCadastro(idPjPfBase);
                var fluxoId = 0;
                string cnpj = null;
                string cpf = null;
                DateTime? dtNasc = null;
                string razaoSocial = null;

                if (cadastro.TipoCadastro == (int) EnumTiposCadastro.Cliente)
                {
                    switch ((EnumTiposFornecedor) cadastro.TipoFornecedor)
                    {
                        case EnumTiposFornecedor.EmpresaNacional:
                            fluxoId =
                                _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorNacional,
                                    cadastro.ContratanteId).ID;
                            cnpj = cadastro.CNPJ;
                            break;
                        case EnumTiposFornecedor.PessoaFisica:
                            fluxoId =
                                _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorPF,
                                    cadastro.ContratanteId).ID;
                            cpf = cadastro.CPF;
                            dtNasc = cadastro.DataNascimento;
                            break;
                    }
                }
                else
                {
                    switch ((EnumTiposFornecedor) cadastro.TipoFornecedor)
                    {
                        case EnumTiposFornecedor.EmpresaNacional:
                            fluxoId =
                                _fluxoBp.BuscarPorTipoEContratante(
                                    (int) EnumTiposFluxo.CadastroFornecedorNacionalDireto, cadastro.ContratanteId).ID;
                            cnpj = cadastro.CNPJ;
                            break;
                        case EnumTiposFornecedor.EmpresaEstrangeira:
                            fluxoId =
                                _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorEstrangeiro,
                                    cadastro.ContratanteId).ID;
                            razaoSocial = cadastro.RazaoSocial;
                            break;
                        case EnumTiposFornecedor.PessoaFisica:
                            fluxoId =
                                _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorPFDireto,
                                    cadastro.ContratanteId).ID;
                            cpf = cadastro.CPF;
                            dtNasc = cadastro.DataNascimento;
                            break;
                    }
                }

                //SOLICITACAO
                solicitacao = new SOLICITACAO
                {
                    FLUXO_ID = fluxoId,
                    CONTRATANTE_ID = cadastro.ContratanteId,
                    SOLICITACAO_DT_CRIA = DateTime.Now,
                    USUARIO_ID = cadastro.UsuarioId,
                    SOLICITACAO_STATUS_ID = (int) EnumStatusTramite.EmAprovacao,
                    DT_PRAZO =
                        DateTime.Now.AddDays(_contratanteConfig.Get(solicitacao.CONTRATANTE_ID).PRAZO_ENTREGA_FICHA)
                };
                _solicitacaoRepository.Add(solicitacao);

                //ROBO
                var robo = new ROBO
                {
                    ROBO_DT_EXEC = DateTime.Now,
                    WFD_SOLICITACAO = solicitacao,
                    CORR_CONTADOR_TENTATIVA = 0,
                    SINT_CONTADOR_TENTATIVA = 0,
                    RF_CONTADOR_TENTATIVA = 0,
                    SN_CONTADOR_TENTATIVA = 0,
                    SUFRAMA_CONTADOR_TENTATIVA = 0
                };
                _roboFornecedorRepository.Add(robo);

                //CADFORN
                var solicitacaoCadastroFornecedor = new SolicitacaoCadastroFornecedor
                {
                    WFD_SOLICITACAO = solicitacao,
                    CATEGORIA_ID = cadastro.CategoriaId,
                    ORG_COMPRAS_ID = cadastro.ComprasId,
                    EhExpansao = false,
                    PJPF_TIPO = cadastro.TipoFornecedor,
                    CNPJ = cnpj,
                    CPF = cpf,
                    RAZAO_SOCIAL = pjpfBase.RAZAO_SOCIAL,
                    DT_NASCIMENTO = dtNasc,
                    WFD_PJPF_ROBO = robo
                };
                _solicitacaoCadastroFornecedorRepository.Add(solicitacaoCadastroFornecedor);

                List<SolicitacaoModificacaoDadosContato> solicitacaoModificacaoContato;
                if (pjpfBase.WFD_PJPF_BASE_CONTATOS != null)
                {
                    solicitacaoModificacaoContato = pjpfBase.WFD_PJPF_BASE_CONTATOS.Select(
                        x => new SolicitacaoModificacaoDadosContato
                        {
                            WFD_SOLICITACAO = solicitacao,
                            NOME = x.NOME,
                            EMAIL = x.EMAIL,
                            TELEFONE = x.TELEFONE,
                            CELULAR = x.CELULAR
                        }).ToList();
                    _solicitacaoContatoRepository.Add(solicitacaoModificacaoContato);
                }

                List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacaoModificacaoEndereco;
                if (pjpfBase.WFD_PJPF_BASE_ENDERECO != null)
                {
                    solicitacaoModificacaoEndereco = pjpfBase.WFD_PJPF_BASE_ENDERECO.Select(
                        x => new SOLICITACAO_MODIFICACAO_ENDERECO
                        {
                            WFD_SOLICITACAO = solicitacao,
                            BAIRRO = x.BAIRRO,
                            CEP = x.CEP,
                            CIDADE = x.CIDADE,
                            COMPLEMENTO = x.COMPLEMENTO,
                            ENDERECO = x.ENDERECO,
                            NUMERO = x.NUMERO,
                            PAIS = x.PAIS,
                            UF = x.UF,
                            TP_ENDERECO_ID = x.TP_ENDERECO_ID
                        }).ToList();
                    _solicitacaoEnderecoRepository.Add(solicitacaoModificacaoEndereco);
                }

                var solicitacaoModificacaoServicoMaterial = new List<SOLICITACAO_UNSPSC>();
                if (pjpfBase.WFD_PJPF_BASE_UNSPSC != null)
                {
                    solicitacaoModificacaoServicoMaterial = pjpfBase.WFD_PJPF_BASE_UNSPSC.Select(
                        x => new SOLICITACAO_UNSPSC
                        {
                            WFD_SOLICITACAO = solicitacao,
                            UNSPSC_ID = (int) x.UNSPSC_ID
                        }).ToList();
                    _solicitacaoUnspscRepository.Add(solicitacaoModificacaoServicoMaterial);
                }


                var solicitacaoDocumentos = _listaDoc.BuscarPorCategoriaId(cadastro.CategoriaId).Select(x =>
                    new SolicitacaoDeDocumentos
                    {
                        WFD_SOLICITACAO = solicitacao,
                        DESCRICAO_DOCUMENTO_ID = x.DESCRICAO_DOCUMENTO_ID,
                        LISTA_DOCUMENTO_ID = x.ID,
                        OBRIGATORIO = x.OBRIGATORIO,
                        EXIGE_VALIDADE = x.EXIGE_VALIDADE,
                        PERIODICIDADE_ID = x.PERIODICIDADE_ID
                    }).ToList();
                foreach (var item in solicitacaoDocumentos)
                {
                    _solicitacaoDocumentoRepository.Add(item);
                }
                return solicitacao;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new ServiceWebForLinkException("Erro ao tentar inserir nova solicitação", ex);
            }

            //return solicitacao;
        }

        /// <summary>
        /// </summary>
        /// <param name="cadastro"></param>
        /// <returns></returns>
        public SOLICITACAO CadastrarSolicitacaoNovoFornecedor(CadastrarSolicitacaoDTO cadastro)
        {
            var fluxoId = 0;
            string cnpj = null;
            string cpf = null;
            DateTime? dtNasc = null;
            string razaoSocial = null;

            if (cadastro.TipoCadastro == (int) EnumTiposCadastro.Cliente)
            {
                switch ((EnumTiposFornecedor) cadastro.TipoFornecedor)
                {
                    case EnumTiposFornecedor.EmpresaNacional:
                        fluxoId =
                            _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorNacional,
                                cadastro.ContratanteId).ID;
                        cnpj = cadastro.CNPJ;
                        break;
                    case EnumTiposFornecedor.PessoaFisica:
                        fluxoId =
                            _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorPF,
                                cadastro.ContratanteId).ID;
                        cpf = cadastro.CPF;
                        dtNasc = cadastro.DataNascimento;
                        break;
                }
            }
            else
            {
                switch ((EnumTiposFornecedor) cadastro.TipoFornecedor)
                {
                    case EnumTiposFornecedor.EmpresaNacional:
                        fluxoId =
                            _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorNacionalDireto,
                                cadastro.ContratanteId).ID;
                        cnpj = cadastro.CNPJ;
                        break;
                    case EnumTiposFornecedor.EmpresaEstrangeira:
                        fluxoId =
                            _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorEstrangeiro,
                                cadastro.ContratanteId).ID;
                        razaoSocial = cadastro.RazaoSocial;
                        break;
                    case EnumTiposFornecedor.PessoaFisica:
                        fluxoId =
                            _fluxoBp.BuscarPorTipoEContratante((int) EnumTiposFluxo.CadastroFornecedorPFDireto,
                                cadastro.ContratanteId).ID;
                        cpf = cadastro.CPF;
                        dtNasc = cadastro.DataNascimento;
                        break;
                }
            }

            //SOLICITACAO
            var solicitacao = new SOLICITACAO
            {
                FLUXO_ID = fluxoId,
                CONTRATANTE_ID = cadastro.ContratanteId,
                SOLICITACAO_DT_CRIA = DateTime.Now,
                USUARIO_ID = cadastro.UsuarioId,
                SOLICITACAO_STATUS_ID = (int) EnumStatusTramite.EmAprovacao,
                DT_PRAZO = DateTime.Now.AddDays(
                    _contratanteConfig
                        .Find(x => x.CONTRATANTE_ID == cadastro.ContratanteId).FirstOrDefault()
                        .PRAZO_ENTREGA_FICHA)
            };

            //ROBO
            var solrobo = new ROBO
            {
                ROBO_DT_EXEC = DateTime.Now,
                CORR_CONTADOR_TENTATIVA = 0,
                SINT_CONTADOR_TENTATIVA = 0,
                RF_CONTADOR_TENTATIVA = 0,
                SN_CONTADOR_TENTATIVA = 0,
                SUFRAMA_CONTADOR_TENTATIVA = 0
            };
            solicitacao.AdicionarRobo(solrobo);

            //CADFORN
            var solForn = new SolicitacaoCadastroFornecedor
            {
                CATEGORIA_ID = cadastro.CategoriaId,
                ORG_COMPRAS_ID = cadastro.ComprasId,
                EhExpansao = false,
                PJPF_TIPO = cadastro.TipoFornecedor,
                CNPJ = cnpj,
                CPF = cpf,
                RAZAO_SOCIAL = razaoSocial,
                DT_NASCIMENTO = dtNasc,
                WFD_PJPF_ROBO = solrobo
            };
            solicitacao.AdicionarSolicitacaoCadastroFornecedor(solForn);

            if (!string.IsNullOrEmpty(cadastro.ContatoEmail))
            {
                //Contato
                var solContato = new SolicitacaoModificacaoDadosContato
                {
                    NOME = cadastro.ContatoNome,
                    EMAIL = cadastro.ContatoEmail,
                    TELEFONE = cadastro.Telefone,
                    CELULAR = string.Empty
                };
                solicitacao.AdicionarSolicitacaoModificacaoContato(solContato);
            }

            var docs = _listaDoc.BuscarPorCategoriaId(cadastro.CategoriaId).Select(x =>
                new SolicitacaoDeDocumentos
                {
                    DESCRICAO_DOCUMENTO_ID = x.DESCRICAO_DOCUMENTO_ID,
                    LISTA_DOCUMENTO_ID = x.ID,
                    OBRIGATORIO = x.OBRIGATORIO,
                    EXIGE_VALIDADE = x.EXIGE_VALIDADE,
                    PERIODICIDADE_ID = x.PERIODICIDADE_ID
                }).ToList();
            solicitacao.SolicitacaoDeDocumentos = docs;

            _solicitacaoRepository.Add(solicitacao);


            return solicitacao;
            //return IncluirNovaSolicitacaoFornecedor(solicitacao, solForn, solContato, solrobo, docs);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<SOLICITACAO> ListarSolicitacaoAprovadaPorId()
        {
            return _solicitacaoRepository.ListarSolicitacaoAprovadaPorId((int) EnumStatusTramite.Aguardando);
        }

        public List<int> BuscarPorContratanteFornecedorTipoFluxoEStatus(int contratanteId, int? fornecedorId,
            int tipoFluxoId, int statusId)
        {
            return ListarPorId(contratanteId, fornecedorId, tipoFluxoId, statusId);
        }

        //public RetornoPesquisa<SOLICITACAO> BuscarPesquisa(Expression<Func<SOLICITACAO, bool>> filtro, int tamanhoPagina,
        //    int pagina
        //    , Func<SOLICITACAO, IComparable> ordenacao)
        //{
        //    return _solicitacaoRepository.Pesquisar(filtro, tamanhoPagina, pagina, ordenacao);
        //}

        public RetornoPesquisa<SOLICITACAO> BuscarPesquisaAcompanhamento(Expression<Func<SOLICITACAO, bool>> filtro,
            int tamanhoPagina, int pagina
            , Func<SOLICITACAO, IComparable> ordenacao)
        {
            try
            {
                return _solicitacaoRepository.Pesquisar(filtro, tamanhoPagina, pagina, ordenacao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }

        public void CriarSolicitacaoBloqueio(SOLICITACAO solicitacao, SOLICITACAO_BLOQUEIO bloqueio)
        {
            solicitacao.FLUXO_ID = 8; // Bloqueio
            solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
            solicitacao.SOLICITACAO_STATUS_ID = (int) EnumStatusTramite.EmAprovacao; // EM APROVACAO
            _solicitacaoRepository.Add(solicitacao);
            _solicitacaoBloqueioRepository.Add(bloqueio);
        }

        public void Dispose()
        {
        }
    }
}