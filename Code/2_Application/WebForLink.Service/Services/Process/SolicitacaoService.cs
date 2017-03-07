using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoWebForLinkAppService : AppService<WebForLinkContexto>, ISolicitacaoWebForLinkAppService
    {
        private readonly IContratanteConfiguracaoWebForLinkService _contratanteConfig;
        private readonly IFluxoWebForLinkService _fluxoBP;
        private readonly IFornecedorBaseConviteWebForLinkService _fornecedorBaseServiceConvite;
        private readonly IFornecedorListaDocumentosWebForLinkService _listaDoc;
        private readonly IFornecedorBaseWebForLinkService _pjpfBaseBP;
        private readonly IRoboWebForLinkService _roboFornecedorService;
        private readonly ISolicitacaoBloqueioWebForLinkService _solicitacaoBloqueioService;
        private readonly ISolicitacaoCadastroFornecedorWebForLinkService _solicitacaoCadastroFornecedorService;
        private readonly ISolicitacaoModificacaoContatoWebForLinkService _solicitacaoContatoService;
        private readonly ISolicitacaoDocumentoWebForLinkService _solicitacaoDocumentoService;
        private readonly ISolicitacaoModificacaoEnderecoWebForLinkService _solicitacaoEnderecoService;
        private readonly ISolicitacaoMensagemWebForLinkService _solicitacaoMensagemService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;
        private readonly ISolicitacaoServicoMaterialWebForLinkService _solicitacaoUnspscService;

        public SolicitacaoWebForLinkAppService(
            ISolicitacaoWebForLinkService solicitacao,
            IRoboWebForLinkService roboFornecedor,
            IContratanteConfiguracaoWebForLinkService contratanteConfig,
            ISolicitacaoCadastroFornecedorWebForLinkService solicitacaoCadastroFornecedor,
            ISolicitacaoMensagemWebForLinkService solicitacaoMensagem,
            ISolicitacaoModificacaoContatoWebForLinkService solicitacaoContato,
            ISolicitacaoDocumentoWebForLinkService solicitacaoDocumento,
            IFornecedorBaseConviteWebForLinkService fornecedorBaseConvite,
            ISolicitacaoModificacaoEnderecoWebForLinkService solicitacaoEndereco,
            ISolicitacaoBloqueioWebForLinkService solicitacaoBloqueio,
            ISolicitacaoServicoMaterialWebForLinkService solicitacaoUnspsc,
            IFornecedorBaseWebForLinkService pjpfBase,
            IFluxoWebForLinkService fluxo,
            IFornecedorListaDocumentosWebForLinkService listaDoc)
        {
            try
            {
                _listaDoc = listaDoc;
                _solicitacaoService = solicitacao;
                _roboFornecedorService = roboFornecedor;
                _contratanteConfig = contratanteConfig;
                _solicitacaoCadastroFornecedorService = solicitacaoCadastroFornecedor;
                _solicitacaoMensagemService = solicitacaoMensagem;
                _solicitacaoContatoService = solicitacaoContato;
                _solicitacaoDocumentoService = solicitacaoDocumento;
                _fornecedorBaseServiceConvite = fornecedorBaseConvite;
                _solicitacaoEnderecoService = solicitacaoEndereco;
                _solicitacaoBloqueioService = solicitacaoBloqueio;
                _solicitacaoUnspscService = solicitacaoUnspsc;
                _pjpfBaseBP = pjpfBase;
                _fluxoBP = fluxo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO_EMAIL> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO_EMAIL> Find(
            Expression<Func<CONTRATANTE_CONFIGURACAO_EMAIL, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(CONTRATANTE_CONFIGURACAO_EMAIL entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(CONTRATANTE_CONFIGURACAO_EMAIL entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(CONTRATANTE_CONFIGURACAO_EMAIL entity)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL Get(int id)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL Get(Expression<Func<CONTRATANTE_CONFIGURACAO_EMAIL, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO_EMAIL> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO_EMAIL> Find(
            Expression<Func<CONTRATANTE_CONFIGURACAO_EMAIL, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public int[] BuscarSolicitacaoAguardandoCarga()
        {
            return _solicitacaoService.BuscarSolicitacaoAguardandoCarga();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public int[] BuscarSolicitacaoAguardandoRetornoCarga()
        {
            return _solicitacaoService.BuscarSolicitacaoAguardandoRetornoCarga();
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        //public SOLICITACAO Alterar(SOLICITACAO solicitacao)
        //{
        //    try
        //    {
        //        _solicitacaoService.Update(solicitacao);
        //        return solicitacao;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar a Solicitação para Ficha Cadastral", ex);
        //    }
        //}

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public int BuscarTipoFluxoId(int solicitacaoId)
        {
            try
            {
                return _solicitacaoService.BuscarTipoFluxoId(solicitacaoId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar tipo de fluxo da solicitacao", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <param name="contratanteId"></param>
        /// <param name="fluxoId"></param>
        /// <param name="motivoReprovacao"></param>
        /// <param name="usuarioId"></param>
        public void AlterarAprovacao(int solicitacaoId, int contratanteId, int fluxoId, string motivoReprovacao,
            int usuarioId)
        {
            var solicitacao = _solicitacaoService.Get(solicitacaoId);
            solicitacao.CONTRATANTE_ID = contratanteId;
            solicitacao.FLUXO_ID = fluxoId;
            solicitacao.MOTIVO = motivoReprovacao;
            solicitacao.USUARIO_ID = usuarioId;
            _solicitacaoService.Update(solicitacao);
        }



        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdIncluindoFluxo(int solicitacaoId)
        {
            try
            {
                return _solicitacaoService.BuscarPorIdIncluindoFluxo(solicitacaoId);
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
                return _solicitacaoService.BuscarAprovacaoPorId(id);
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
        public SOLICITACAO BuscarPorSolicitacaoId(int solicitacaoId)
        {
            try
            {
                return _solicitacaoService.BuscarPorSolicitacaoId(solicitacaoId);
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
        public SOLICITACAO BuscarPorIdComSolicitacaoCadastroFornecedor(int solicitacaoId)
        {
            try
            {
                return _solicitacaoService.BuscarPorIdComSolicitacaoCadastroPjpf(solicitacaoId);
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
                return _solicitacaoService.BuscarPorIdControleSolicitacoes(id);
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
                return _solicitacaoService.BuscarPorIdComFornecedoresDireto(id);
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
                return _solicitacaoService.BuscarPorIdDocumentosSolicitados(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoCriacaoId"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarPorIdComDocumentos(int solicitacaoCriacaoId)
        {
            try
            {
                return _solicitacaoService.BuscarPorIdComDocumentos(solicitacaoCriacaoId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        public SOLICITACAO BuscarPorId(int id)
        {
            try
            {
                return _solicitacaoService.Get(id);
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
                return _solicitacaoService.Get(filtro);
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
                _solicitacaoService.Add(solicitacao);
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
        public SOLICITACAO InserirSolicitacaoDocumentos(SOLICITACAO solicitacao, List<SolicitacaoDeDocumentos> documentos)
        {
            try
            {
                solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
                solicitacao.PJPF_ID = solicitacao.PJPF_ID == 0 ? null : solicitacao.PJPF_ID;
                solicitacao.MOTIVO = null;
                solicitacao.TP_PJPF = null;
                solicitacao.DT_PRAZO =
                    DateTime.Now.AddDays(_contratanteConfig.Get(solicitacao.CONTRATANTE_ID).PRAZO_ENTREGA_FICHA);
                _solicitacaoService.Add(solicitacao);
                documentos.ForEach(x =>
                {
                    x.WFD_SOLICITACAO = solicitacao;
                    _solicitacaoDocumentoService.Add(documentos);
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
                _solicitacaoService.Add(solicitacao);
                if (robo != null)
                    _roboFornecedorService.Add(robo);
                _solicitacaoCadastroFornecedorService.Add(cadastro);
                if (contato != null)
                    _solicitacaoContatoService.Add(contato);
                _solicitacaoDocumentoService.Add(documentos);

                return solicitacao;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Log.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //foreach (var ve in eve.ValidationErrors)
                    //{
                    //    Log.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    //}
                }
                throw;
            }
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
                _solicitacaoService.Add(solicitacao);
                if (robo != null)
                    _roboFornecedorService.Add(robo);
                _solicitacaoCadastroFornecedorService.Add(cadastro);
                if (contato != null)
                    _solicitacaoContatoService.Add(contato);
                _solicitacaoDocumentoService.Add(documentos);
                _fornecedorBaseServiceConvite.Add(convite);
                _solicitacaoMensagemService.Add(mensagem);
                return solicitacao;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Log.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //foreach (var ve in eve.ValidationErrors)
                    //{
                    //    Log.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    //}
                }
                throw;
            }
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

                if (cadastro.TipoCadastro == (int)EnumTiposCadastro.Cliente)
                {
                    switch ((EnumTiposFornecedor)cadastro.TipoFornecedor)
                    {
                        case EnumTiposFornecedor.EmpresaNacional:
                            fluxoId =
                                _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorNacional,
                                    cadastro.ContratanteId).ID;
                            cnpj = cadastro.CNPJ;
                            break;
                        case EnumTiposFornecedor.PessoaFisica:
                            fluxoId =
                                _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorPF,
                                    cadastro.ContratanteId).ID;
                            cpf = cadastro.CPF;
                            dtNasc = cadastro.DataNascimento;
                            break;
                    }
                }
                else
                {
                    switch ((EnumTiposFornecedor)cadastro.TipoFornecedor)
                    {
                        case EnumTiposFornecedor.EmpresaNacional:
                            fluxoId =
                                _fluxoBP.BuscarPorTipoEContratante(
                                    (int)EnumTiposFluxo.CadastroFornecedorNacionalDireto, cadastro.ContratanteId).ID;
                            cnpj = cadastro.CNPJ;
                            break;
                        case EnumTiposFornecedor.EmpresaEstrangeira:
                            fluxoId =
                                _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorEstrangeiro,
                                    cadastro.ContratanteId).ID;
                            razaoSocial = cadastro.RazaoSocial;
                            break;
                        case EnumTiposFornecedor.PessoaFisica:
                            fluxoId =
                                _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorPFDireto,
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
                    SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao,
                    DT_PRAZO =
                        DateTime.Now.AddDays(_contratanteConfig.Get(solicitacao.CONTRATANTE_ID).PRAZO_ENTREGA_FICHA)
                };
                _solicitacaoService.Add(solicitacao);

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
                _roboFornecedorService.Add(robo);

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
                _solicitacaoCadastroFornecedorService.Add(solicitacaoCadastroFornecedor);

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
                    _solicitacaoContatoService.Add(solicitacaoModificacaoContato);
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
                    _solicitacaoEnderecoService.Add(solicitacaoModificacaoEndereco);
                }

                var solicitacaoModificacaoServicoMaterial = new List<SOLICITACAO_UNSPSC>();
                if (pjpfBase.WFD_PJPF_BASE_UNSPSC != null)
                {
                    solicitacaoModificacaoServicoMaterial = pjpfBase.WFD_PJPF_BASE_UNSPSC.Select(
                        x => new SOLICITACAO_UNSPSC
                        {
                            WFD_SOLICITACAO = solicitacao,
                            UNSPSC_ID = (int)x.UNSPSC_ID
                        }).ToList();
                    _solicitacaoUnspscService.Add(solicitacaoModificacaoServicoMaterial);
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
                _solicitacaoDocumentoService.Add(solicitacaoDocumentos);
                return solicitacao;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new ServiceWebForLinkException("Erro ao tentar inserir nova solicitação", ex);
            }

            //return solicitacao;
        }

        public SOLICITACAO Buscar(Func<SOLICITACAO, bool> func)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO Buscar(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="cadastro"></param>
        /// <returns></returns>
        public SOLICITACAO CadastrarSolicitacaoNovoFornecedor(CadastrarSolicitacaoDTO cadastro)
        {
            BeginTransaction();
            var fluxoId = 0;
            string cnpj = null;
            string cpf = null;
            DateTime? dtNasc = null;
            string razaoSocial = null;

            if (cadastro.TipoCadastro == (int)EnumTiposCadastro.Cliente)
            {
                switch ((EnumTiposFornecedor)cadastro.TipoFornecedor)
                {
                    case EnumTiposFornecedor.EmpresaNacional:
                        fluxoId =
                            _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorNacional,
                                cadastro.ContratanteId).ID;
                        cnpj = cadastro.CNPJ;
                        break;
                    case EnumTiposFornecedor.PessoaFisica:
                        fluxoId =
                            _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorPF,
                                cadastro.ContratanteId).ID;
                        cpf = cadastro.CPF;
                        dtNasc = cadastro.DataNascimento;
                        break;
                }
            }
            else
            {
                switch ((EnumTiposFornecedor)cadastro.TipoFornecedor)
                {
                    case EnumTiposFornecedor.EmpresaNacional:
                        fluxoId =
                            _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorNacionalDireto,
                                cadastro.ContratanteId).ID;
                        cnpj = cadastro.CNPJ;
                        break;
                    case EnumTiposFornecedor.EmpresaEstrangeira:
                        fluxoId =
                            _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorEstrangeiro,
                                cadastro.ContratanteId).ID;
                        razaoSocial = cadastro.RazaoSocial;
                        break;
                    case EnumTiposFornecedor.PessoaFisica:
                        fluxoId =
                            _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.CadastroFornecedorPFDireto,
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
                SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao,
                DT_PRAZO = DateTime.Now.AddDays(
                    _contratanteConfig
                        .Get(x => x.CONTRATANTE_ID == cadastro.ContratanteId)
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

            _solicitacaoService.Add(solicitacao);
            Commit();

            return solicitacao;
            //return IncluirNovaSolicitacaoFornecedor(solicitacao, solForn, solContato, solrobo, docs);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public SOLICITACAO BuscarSolicitacaoFinalizaCriacaoFornecedor(int solicitacaoId)
        {
            try
            {
                return _solicitacaoService.BuscarSolicitacaoFinalizaCriacaoFornecedor(solicitacaoId);
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
            return _solicitacaoService.ListarSolicitacaoCarga(idContratante);
        }

        /// <summary>
        /// </summary>
        /// <param name="idSolicitacao"></param>
        /// <returns></returns>
        public List<SOLICITACAO> ListarTodasSolicitacoesAprovadas(int idSolicitacao)
        {
            return _solicitacaoService.ListarTodasSolicitacoesAprovadas(idSolicitacao);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<SOLICITACAO> ListarSolicitacaoAprovadaPorId()
        {
            return _solicitacaoService.ListarSolicitacaoAprovadaPorId((int)EnumStatusTramite.Aguardando);
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
                return _solicitacaoService.ListarPorId(contratanteId, fornecedorId, tipoFluxoId, statusId);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar a solicitação.", e);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cadastro"></param>
        /// <returns></returns>
        public List<int> BuscarPorContratanteFornecedorTipoFluxoEStatus(int contratanteId, int? fornecedorId,
            int tipoFluxoId, int statusId)
        {
            return ListarPorId(contratanteId, fornecedorId, tipoFluxoId, statusId);
        }

        //public RetornoPesquisa<SOLICITACAO> BuscarPesquisa(Expression<Func<SOLICITACAO, bool>> filtro, int tamanhoPagina, int pagina
        //    , Func<SOLICITACAO, IComparable> ordenacao)
        //{
        //    return _solicitacaoService.Pesquisar(filtro, tamanhoPagina, pagina, ordenacao);
        //}

        //public RetornoPesquisa<SOLICITACAO> BuscarPesquisaAcompanhamento(Expression<Func<SOLICITACAO, bool>> filtro, int tamanhoPagina, int pagina
        //    , Func<SOLICITACAO, IComparable> ordenacao)
        //{
        //    try
        //    {
        //        //return _solicitacaoService.BuscarPesquisaAcompanhamento(filtro, tamanhoPagina, pagina, ordenacao);
        //        return _solicitacaoService.Pesquisar(filtro, tamanhoPagina, pagina, ordenacao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
        //    }
        //}

        public void CriarSolicitacaoBloqueio(SOLICITACAO solicitacao, SOLICITACAO_BLOQUEIO bloqueio)
        {
            solicitacao.FLUXO_ID = 8; // Bloqueio
            solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
            solicitacao.SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao; // EM APROVACAO
            _solicitacaoService.Add(solicitacao);
            _solicitacaoBloqueioService.Add(bloqueio);
        }




        public object BuscarPesquisa(Func<SOLICITACAO, bool> p1, int tamanhoPagina, int pagina, Func<SOLICITACAO, object> p2)
        {
            throw new NotImplementedException();
        }

        SOLICITACAO IAppService<SOLICITACAO>.Get(int id, bool @readonly)
        {
            throw new NotImplementedException();
        }

        SOLICITACAO IAppService<SOLICITACAO>.Get(string id, bool @readonly)
        {
            throw new NotImplementedException();
        }

        SOLICITACAO IAppService<SOLICITACAO>.GetAllReferences(int id, bool @readonly)
        {
            throw new NotImplementedException();
        }

        IEnumerable<SOLICITACAO> IAppService<SOLICITACAO>.All(bool @readonly)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO> Find(Expression<Func<SOLICITACAO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SOLICITACAO entity)
        {
            BeginTransaction();
            _solicitacaoService.Add(entity);
            Commit();
            return ValidationResult;
        }

        public ValidationResult Update(SOLICITACAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SOLICITACAO entity)
        {
            throw new NotImplementedException();
        }

        SOLICITACAO IReadOnlyAppService<SOLICITACAO>.Get(int id)
        {
            return _solicitacaoService.Get(x=>x.ID == id);
        }

        public SOLICITACAO Get(Expression<Func<SOLICITACAO, bool>> predicate)
        {
            return _solicitacaoService.Get(predicate);
        }

        IEnumerable<SOLICITACAO> IReadOnlyAppService<SOLICITACAO>.All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO> Find(Expression<Func<SOLICITACAO, bool>> predicate)
        {
            return _solicitacaoService.Find(predicate);
        }

        List<int> ISolicitacaoWebForLinkAppService.BuscarSolicitacaoAguardandoCarga()
        {
            throw new NotImplementedException();
        }

        List<int> ISolicitacaoWebForLinkAppService.BuscarSolicitacaoAguardandoRetornoCarga()
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<SOLICITACAO> BuscarPesquisa(Expression<Func<SOLICITACAO, bool>> expression, int tamanhoPagina, int pagina, Func<SOLICITACAO, IComparable> p)
        {
            return _solicitacaoService.BuscarPesquisa(expression, tamanhoPagina, pagina, p);
        }

        public void AlterarSolicitacaoMensagem(SOLICITACAO solicitacao)
        {
            if (solicitacao.ID == 0)
                Create(solicitacao);
            else
            {
                var entity = _solicitacaoService.Get(solicitacao.ID);
                entity.CONTRATANTE_ID = solicitacao.CONTRATANTE_ID;
                entity.FLUXO_ID = solicitacao.FLUXO_ID;
                entity.SOLICITACAO_DT_CRIA = solicitacao.SOLICITACAO_DT_CRIA;
                entity.USUARIO_ID = solicitacao.USUARIO_ID;
                entity.SOLICITACAO_STATUS_ID = solicitacao.SOLICITACAO_STATUS_ID;
                entity.MOTIVO = solicitacao.MOTIVO;
                entity.PJPF_ID = solicitacao.PJPF_ID;
                entity.TP_PJPF = solicitacao.TP_PJPF;
                entity.DT_PRAZO = solicitacao.DT_PRAZO;
                entity.DT_PRORROGACAO_PRAZO = solicitacao.DT_PRORROGACAO_PRAZO;
                entity.MOTIVO_PRORROGACAO = solicitacao.MOTIVO_PRORROGACAO;
                entity.PJPF_BASE_ID = solicitacao.PJPF_BASE_ID;
                entity.ROBO_EXECUTADO = solicitacao.ROBO_EXECUTADO;
                entity.ROBO_TENTATIVAS_EXCEDIDAS = solicitacao.ROBO_TENTATIVAS_EXCEDIDAS;
                entity.SolicitacaoDeDocumentos = solicitacao.SolicitacaoDeDocumentos;
                Alterar(entity);
            }
        }

        public void Alterar(SOLICITACAO solicitacao)
        {
            try
            {
                BeginTransaction();
                _solicitacaoService.Update(solicitacao);
                Commit();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a Solicitação para Ficha Cadastral", ex);
            }
        }

        public void AlterarSolicitacaoParaFinalizado(int solicitacaoId, int statusId)
        {
            SOLICITACAO solicitacao = _solicitacaoService.Get(solicitacaoId);
            solicitacao.SOLICITACAO_STATUS_ID = statusId;
            Alterar(solicitacao);
        }
    }
}