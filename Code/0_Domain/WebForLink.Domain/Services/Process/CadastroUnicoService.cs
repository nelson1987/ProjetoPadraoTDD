using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class CadastroUnicoWebForLinkService : Service<QUESTIONARIO>, ICadastroUnicoWebForLinkService
    {
        private readonly IFornecedorWebForLinkRepository _fornecedorRepository;
        private readonly IInformacaoComplementarWebForLinkRepository _informacaoComplementarRepository;
        private readonly IQuestionarioWebForLinkRepository _questionarioRepository;

        public CadastroUnicoWebForLinkService(
            IFornecedorWebForLinkRepository fornecedor,
            IQuestionarioWebForLinkRepository questionario,
            IInformacaoComplementarWebForLinkRepository informacaoComplementar) : base(questionario)
        {
            try
            {
                _fornecedorRepository = fornecedor;
                _questionarioRepository = questionario;
                _informacaoComplementarRepository = informacaoComplementar;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public Fornecedor CarregarDadosPjpf(int idFornecedor)
        {
            try
            {
                return _fornecedorRepository.CarregarDadosPjpf(idFornecedor);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        ///     Buscar Questionário pelo Id do Contratante
        /// </summary>
        /// <param name="idContratante">Id do Contratante</param>
        /// <returns></returns>
        public List<QUESTIONARIO> BuscarPorIdContratante(int idContratante)
        {
            return _questionarioRepository.Find(x => x.CONTRATANTE_ID == idContratante).ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="idContratante"></param>
        /// <param name="idPapel"></param>
        /// <returns></returns>
        public List<QUESTIONARIO> BuscarPorIdContratanteParaIncluirSolicitacao(int idContratante, int idPapel)
        {
            var questionarioList = new List<QUESTIONARIO>();
            foreach (var questionario in _questionarioRepository.Find(x => x.CONTRATANTE_ID == idContratante))
            {
                var abasQuestionario = questionario
                    .QIC_QUEST_ABA
                    .OrderBy(x => x.ORDEM)
                    .ToList();
                var abaList = new List<QUESTIONARIO_ABA>();

                foreach (var aba in abasQuestionario)
                {
                    var perguntasAba = aba
                        .QIC_QUEST_ABA_PERG
                        .OrderBy(x => x.ORDEM)
                        .ToList();

                    #region Pergunta

                    var perguntaList = perguntasAba.Select(pergunta => new QUESTIONARIO_PERGUNTA
                    {
                        ID = pergunta.ID,
                        QUEST_ABA_ID = pergunta.QUEST_ABA_ID,
                        PERG_NM = pergunta.PERG_NM,
                        TP_DADO = pergunta.TP_DADO,
                        EXIBE_NM = pergunta.EXIBE_NM,
                        DOMINIO = pergunta.DOMINIO,
                        ORDEM = pergunta.ORDEM,
                        RESP_TAMANHO = pergunta.RESP_TAMANHO,
                        E_PAI = pergunta.E_PAI,
                        PERG_PAI = pergunta.PERG_PAI,
                        QIC_QUEST_ABA = pergunta.QIC_QUEST_ABA,
                        QIC_QUEST_ABA_PERG_PAPEL = pergunta
                            .QIC_QUEST_ABA_PERG_PAPEL
                            .Where(x => x.PAPEL_ID == idPapel)
                            .ToList(),
                        QIC_QUEST_ABA_PERG_RESP = pergunta.QIC_QUEST_ABA_PERG_RESP
                    }).ToList();

                    #endregion

                    #region Aba

                    abaList.Add(new QUESTIONARIO_ABA
                    {
                        ID = aba.ID,
                        ABA_NM = aba.ABA_NM,
                        QUESTIONARIO_ID = aba.QUESTIONARIO_ID,
                        ABA_DSC = aba.ABA_DSC,
                        ORDEM = aba.ORDEM,
                        QIC_QUEST_ABA_PERG = perguntaList,
                        QIC_QUESTIONARIO = aba.QIC_QUESTIONARIO
                    });

                    #endregion
                }

                #region Questionario

                questionarioList.Add(new QUESTIONARIO
                {
                    ID = questionario.ID,
                    QUEST_NM = questionario.QUEST_NM,
                    CONTRATANTE_ID = questionario.CONTRATANTE_ID,
                    QUEST_DSC = questionario.QUEST_DSC,
                    LE_D_BANCARIO = questionario.LE_D_BANCARIO,
                    LE_D_CONTATO = questionario.LE_D_CONTATO,
                    LE_D_GERAIS = questionario.LE_D_GERAIS,
                    LE_INFO_COMPL = questionario.LE_INFO_COMPL,
                    QIC_QUEST_ABA = abaList,
                    WFD_CONTRATANTE = questionario.WFD_CONTRATANTE
                });

                #endregion
            }

            return questionarioList;
        }

        /// <summary>
        ///     Buscar Questionário pelo Id do Contratante e Id do Papel
        /// </summary>
        /// <param name="idContratante">Id do Contratante</param>
        /// <param name="idPapel">Id do Papel do Usuário</param>
        /// <returns></returns>
        public List<QUESTIONARIO> BuscarPorIdContratante(int idContratante, int idPapel)
        {
            var questionarioList = new List<QUESTIONARIO>();
            foreach (var questionario in _questionarioRepository.Find(x => x.CONTRATANTE_ID == idContratante))
            {
                var abasQuestionario = questionario
                    .QIC_QUEST_ABA
                    .OrderBy(x => x.ORDEM)
                    .ToList();
                var abaList = new List<QUESTIONARIO_ABA>();

                foreach (var aba in abasQuestionario)
                {
                    var perguntasAba = aba
                        .QIC_QUEST_ABA_PERG
                        .OrderBy(x => x.ORDEM)
                        .ToList();

                    #region Pergunta

                    var perguntaList = perguntasAba.Select(pergunta => new QUESTIONARIO_PERGUNTA
                    {
                        ID = pergunta.ID,
                        QUEST_ABA_ID = pergunta.QUEST_ABA_ID,
                        PERG_NM = pergunta.PERG_NM,
                        TP_DADO = pergunta.TP_DADO,
                        EXIBE_NM = pergunta.EXIBE_NM,
                        DOMINIO = pergunta.DOMINIO,
                        ORDEM = pergunta.ORDEM,
                        RESP_TAMANHO = pergunta.RESP_TAMANHO,
                        E_PAI = pergunta.E_PAI,
                        PERG_PAI = pergunta.PERG_PAI,
                        QIC_QUEST_ABA = pergunta.QIC_QUEST_ABA,
                        QIC_QUEST_ABA_PERG_PAPEL = pergunta
                            .QIC_QUEST_ABA_PERG_PAPEL
                            .Where(x => x.PAPEL_ID == idPapel)
                            .ToList(),
                        WFD_INFORM_COMPL = pergunta
                            .WFD_INFORM_COMPL
                            .Where(x => x.PERG_ID == pergunta.ID)
                            .ToList(),
                        QIC_QUEST_ABA_PERG_RESP = pergunta.QIC_QUEST_ABA_PERG_RESP
                    }).ToList();

                    #endregion

                    #region Aba

                    abaList.Add(new QUESTIONARIO_ABA
                    {
                        ID = aba.ID,
                        ABA_NM = aba.ABA_NM,
                        QUESTIONARIO_ID = aba.QUESTIONARIO_ID,
                        ABA_DSC = aba.ABA_DSC,
                        ORDEM = aba.ORDEM,
                        QIC_QUEST_ABA_PERG = perguntaList,
                        QIC_QUESTIONARIO = aba.QIC_QUESTIONARIO
                    });

                    #endregion
                }

                #region Questionario

                questionarioList.Add(new QUESTIONARIO
                {
                    ID = questionario.ID,
                    QUEST_NM = questionario.QUEST_NM,
                    CONTRATANTE_ID = questionario.CONTRATANTE_ID,
                    QUEST_DSC = questionario.QUEST_DSC,
                    LE_D_BANCARIO = questionario.LE_D_BANCARIO,
                    LE_D_CONTATO = questionario.LE_D_CONTATO,
                    LE_D_GERAIS = questionario.LE_D_GERAIS,
                    LE_INFO_COMPL = questionario.LE_INFO_COMPL,
                    QIC_QUEST_ABA = abaList,
                    WFD_CONTRATANTE = questionario.WFD_CONTRATANTE
                });

                #endregion
            }

            return questionarioList;
        }

        /// <summary>
        ///     Buscar Questionário pelo Id do Contratante, Id do Papel e Id da Solicitação
        /// </summary>
        /// <param name="idContratante">Id do Contratante</param>
        /// <param name="idPapel">Id do Papel do Usuário</param>
        /// <param name="idSolicitacao">Id da Solicitação</param>
        /// <returns></returns>
        public List<QUESTIONARIO> BuscarPorIdContratante(int idContratante, int idPapel, int idSolicitacao)
        {
            var questionarioList = new List<QUESTIONARIO>();
            foreach (var questionario in _questionarioRepository.Find(x => x.CONTRATANTE_ID == idContratante))
            {
                var abasQuestionario = questionario
                    .QIC_QUEST_ABA
                    .OrderBy(x => x.ORDEM)
                    .ToList();
                var abaList = new List<QUESTIONARIO_ABA>();

                foreach (var aba in abasQuestionario)
                {
                    var perguntasAba = aba
                        .QIC_QUEST_ABA_PERG
                        .OrderBy(x => x.ORDEM)
                        .ToList();

                    #region Pergunta

                    var perguntaList = perguntasAba.Select(pergunta => new QUESTIONARIO_PERGUNTA
                    {
                        ID = pergunta.ID,
                        QUEST_ABA_ID = pergunta.QUEST_ABA_ID,
                        PERG_NM = pergunta.PERG_NM,
                        TP_DADO = pergunta.TP_DADO,
                        EXIBE_NM = pergunta.EXIBE_NM,
                        DOMINIO = pergunta.DOMINIO,
                        ORDEM = pergunta.ORDEM,
                        RESP_TAMANHO = pergunta.RESP_TAMANHO,
                        E_PAI = pergunta.E_PAI,
                        PERG_PAI = pergunta.PERG_PAI,
                        QIC_QUEST_ABA = pergunta.QIC_QUEST_ABA,
                        QIC_QUEST_ABA_PERG_PAPEL = pergunta
                            .QIC_QUEST_ABA_PERG_PAPEL
                            .Where(x => x.PAPEL_ID == idPapel)
                            .ToList(),
                        WFD_INFORM_COMPL = pergunta
                            .WFD_INFORM_COMPL
                            .Where(x => x.PERG_ID == pergunta.ID && x.SOLICITACAO_ID == idSolicitacao)
                            .ToList(),
                        QIC_QUEST_ABA_PERG_RESP = pergunta.QIC_QUEST_ABA_PERG_RESP
                    }).ToList();

                    #endregion

                    #region Aba

                    abaList.Add(new QUESTIONARIO_ABA
                    {
                        ID = aba.ID,
                        ABA_NM = aba.ABA_NM,
                        QUESTIONARIO_ID = aba.QUESTIONARIO_ID,
                        ABA_DSC = aba.ABA_DSC,
                        ORDEM = aba.ORDEM,
                        QIC_QUEST_ABA_PERG = perguntaList,
                        QIC_QUESTIONARIO = aba.QIC_QUESTIONARIO
                    });

                    #endregion
                }

                #region Questionario

                questionarioList.Add(new QUESTIONARIO
                {
                    ID = questionario.ID,
                    QUEST_NM = questionario.QUEST_NM,
                    CONTRATANTE_ID = questionario.CONTRATANTE_ID,
                    QUEST_DSC = questionario.QUEST_DSC,
                    LE_D_BANCARIO = questionario.LE_D_BANCARIO,
                    LE_D_CONTATO = questionario.LE_D_CONTATO,
                    LE_D_GERAIS = questionario.LE_D_GERAIS,
                    LE_INFO_COMPL = questionario.LE_INFO_COMPL,
                    QIC_QUEST_ABA = abaList,
                    WFD_CONTRATANTE = questionario.WFD_CONTRATANTE
                });

                #endregion
            }

            return questionarioList;
        }

        /// <summary>
        /// </summary>
        /// <param name="idContratante"></param>
        /// <param name="idPapel"></param>
        /// <param name="idContratanteFornecedor"></param>
        /// <returns></returns>
        public List<QUESTIONARIO> BuscarPorIdContratanteAlteracao(int idContratante, int idPapel,
            int idContratanteFornecedor)
        {
            var questionarioList = new List<QUESTIONARIO>();
            foreach (var questionario in _questionarioRepository.Find(x => x.CONTRATANTE_ID == idContratante))
            {
                var abasQuestionario = questionario
                    .QIC_QUEST_ABA
                    .OrderBy(x => x.ORDEM)
                    .ToList();
                var abaList = new List<QUESTIONARIO_ABA>();

                foreach (var aba in abasQuestionario)
                {
                    var perguntasAba = aba
                        .QIC_QUEST_ABA_PERG
                        .OrderBy(x => x.ORDEM)
                        .ToList();

                    #region Pergunta

                    var perguntaList = perguntasAba.Select(pergunta => new QUESTIONARIO_PERGUNTA
                    {
                        ID = pergunta.ID,
                        QUEST_ABA_ID = pergunta.QUEST_ABA_ID,
                        PERG_NM = pergunta.PERG_NM,
                        TP_DADO = pergunta.TP_DADO,
                        EXIBE_NM = pergunta.EXIBE_NM,
                        DOMINIO = pergunta.DOMINIO,
                        ORDEM = pergunta.ORDEM,
                        RESP_TAMANHO = pergunta.RESP_TAMANHO,
                        E_PAI = pergunta.E_PAI,
                        PERG_PAI = pergunta.PERG_PAI,
                        QIC_QUEST_ABA = pergunta.QIC_QUEST_ABA,
                        QIC_QUEST_ABA_PERG_PAPEL = pergunta
                            .QIC_QUEST_ABA_PERG_PAPEL
                            .Where(x => x.PAPEL_ID == idPapel)
                            .ToList(),
                        WFD_PJPF_INFORM_COMPL = pergunta
                            .WFD_PJPF_INFORM_COMPL
                            .Where(x => x.PERG_ID == pergunta.ID && x.CONTRATANTE_PJPF_ID == idContratanteFornecedor)
                            .ToList(),
                        QIC_QUEST_ABA_PERG_RESP = pergunta.QIC_QUEST_ABA_PERG_RESP
                    }).ToList();

                    #endregion

                    #region Aba

                    abaList.Add(new QUESTIONARIO_ABA
                    {
                        ID = aba.ID,
                        ABA_NM = aba.ABA_NM,
                        QUESTIONARIO_ID = aba.QUESTIONARIO_ID,
                        ABA_DSC = aba.ABA_DSC,
                        ORDEM = aba.ORDEM,
                        QIC_QUEST_ABA_PERG = perguntaList,
                        QIC_QUESTIONARIO = aba.QIC_QUESTIONARIO
                    });

                    #endregion
                }

                #region Questionario

                questionarioList.Add(new QUESTIONARIO
                {
                    ID = questionario.ID,
                    QUEST_NM = questionario.QUEST_NM,
                    CONTRATANTE_ID = questionario.CONTRATANTE_ID,
                    QUEST_DSC = questionario.QUEST_DSC,
                    LE_D_BANCARIO = questionario.LE_D_BANCARIO,
                    LE_D_CONTATO = questionario.LE_D_CONTATO,
                    LE_D_GERAIS = questionario.LE_D_GERAIS,
                    LE_INFO_COMPL = questionario.LE_INFO_COMPL,
                    QIC_QUEST_ABA = abaList,
                    WFD_CONTRATANTE = questionario.WFD_CONTRATANTE
                });

                #endregion
            }

            return questionarioList;
        }

        /// <summary>
        /// </summary>
        /// <param name="idFornecedor"></param>
        /// <param name="idPapel"></param>
        /// <param name="idSolicitacao"></param>
        /// <returns></returns>
        public Fornecedor CarregarDadosPjpf(int idFornecedor, int idPapel, int? idSolicitacao)
        {
            try
            {
                //Questionário por Contratante
                var questionariosContratante = _questionarioRepository.Find(x => x.CONTRATANTE_ID == 1).ToList();
                // contratanteQuestionario.QIC_QUESTIONARIO.ToList();
                foreach (var questionario in questionariosContratante)
                {
                    //Aba dos Questionários
                    var abasQuestionario = questionario.QIC_QUEST_ABA.ToList();
                    foreach (var aba in abasQuestionario)
                    {
                        var perguntas = aba.QIC_QUEST_ABA_PERG.ToList();
                        foreach (var pergunta in perguntas)
                        {
                            if (pergunta.E_PAI)
                            {
                                var perguntaFilho =
                                    aba.QIC_QUEST_ABA_PERG.FirstOrDefault(x => x.PERG_PAI == pergunta.ID);
                            }
                            var papelPergunta = pergunta.QIC_QUEST_ABA_PERG_PAPEL
                                .FirstOrDefault(x => x.PAPEL_ID == idPapel);
                            if (papelPergunta != null)
                            {
                                if (papelPergunta.LEITURA)
                                {
                                    var nomeImpressao = pergunta.EXIBE_NM;
                                    if (pergunta.TP_DADO == "DOMINIO" && pergunta.DOMINIO == true)
                                    {
                                        var respostaspossíveis = pergunta.QIC_QUEST_ABA_PERG_RESP.OrderBy(x => x.ORDEM);
                                    }
                                    var respostaUsuario = pergunta.WFD_INFORM_COMPL;
                                }
                            }
                        }
                    }
                }
                //Perguntas das Abas
                //Perguntas exibidas nas abas por papeis
                //respostas das perguntas com tp_dado ="Dominio"
                //Relação de Resposta
                //Resposta do Usuário
                //ProcurarRelacionamento


                return CarregarDadosPjpf(idFornecedor).WFD_CONTRATANTE_PJPF.Where(x => x.PJPF_ID == idFornecedor)
                    .Select(contratante => new WFD_CONTRATANTE_PJPF
                    {
                        BancoDoFornecedor = contratante.BancoDoFornecedor,
                        WFD_PJPF_CONTATOS = contratante.WFD_PJPF_CONTATOS,
                        WFD_CONTRATANTE = new Contratante
                        {
                            ID = contratante.WFD_CONTRATANTE.ID,
                            TIPO_CADASTRO_ID = contratante.WFD_CONTRATANTE.TIPO_CADASTRO_ID,
                            CNPJ = contratante.WFD_CONTRATANTE.CNPJ,
                            RAZAO_SOCIAL = contratante.WFD_CONTRATANTE.RAZAO_SOCIAL,
                            NOME_FANTASIA = contratante.WFD_CONTRATANTE.NOME_FANTASIA,
                            DATA_CADASTRO = contratante.WFD_CONTRATANTE.DATA_CADASTRO,
                            LOGO_FOTO = contratante.WFD_CONTRATANTE.LOGO_FOTO,
                            EXTENSAO_IMAGEM = contratante.WFD_CONTRATANTE.EXTENSAO_IMAGEM,
                            ESTILO = contratante.WFD_CONTRATANTE.ESTILO,
                            //CONTRANTE_COD_ERP = contratante.Contratante.CONTRANTE_COD_ERP,
                            QIC_QUESTIONARIO =
                                contratante.WFD_CONTRATANTE.QIC_QUESTIONARIO.Select(item => new QUESTIONARIO
                                {
                                    ID = item.ID,
                                    QUEST_NM = item.QUEST_NM,
                                    CONTRATANTE_ID = item.CONTRATANTE_ID,
                                    QUEST_DSC = item.QUEST_DSC,
                                    LE_D_BANCARIO = item.LE_D_BANCARIO,
                                    LE_D_CONTATO = item.LE_D_CONTATO,
                                    LE_D_GERAIS = item.LE_D_GERAIS,
                                    LE_INFO_COMPL = item.LE_INFO_COMPL,
                                    QIC_QUEST_ABA = item.QIC_QUEST_ABA.Select(aba => new QUESTIONARIO_ABA
                                    {
                                        ID = aba.ID,
                                        ABA_NM = aba.ABA_NM,
                                        QUESTIONARIO_ID = aba.QUESTIONARIO_ID,
                                        ABA_DSC = aba.ABA_DSC,
                                        ORDEM = aba.ORDEM,
                                        QIC_QUEST_ABA_PERG =
                                            aba.QIC_QUEST_ABA_PERG.Select(pergunta => new QUESTIONARIO_PERGUNTA
                                            {
                                                ID = pergunta.ID,
                                                QUEST_ABA_ID = pergunta.QUEST_ABA_ID,
                                                PERG_NM = pergunta.PERG_NM,
                                                TP_DADO = pergunta.TP_DADO,
                                                EXIBE_NM = pergunta.EXIBE_NM,
                                                DOMINIO = pergunta.DOMINIO,
                                                ORDEM = pergunta.ORDEM,
                                                RESP_TAMANHO = pergunta.RESP_TAMANHO,
                                                QIC_QUEST_ABA_PERG_PAPEL = pergunta.QIC_QUEST_ABA_PERG_PAPEL
                                                    .Where(
                                                        y => y.PERG_ID == pergunta.ID && y.PAPEL_ID == idPapel)
                                                    .Select(papelPergunta => new QUESTIONARIO_PAPEL
                                                    {
                                                        ID = papelPergunta.ID,
                                                        ESCRITA = papelPergunta.ESCRITA,
                                                        LEITURA = papelPergunta.LEITURA,
                                                        OBRIG = papelPergunta.OBRIG,
                                                        PAPEL_ID = papelPergunta.PAPEL_ID,
                                                        PERG_ID = papelPergunta.PERG_ID
                                                    }).ToList(),
                                                QIC_QUEST_ABA_PERG_RESP = pergunta.QIC_QUEST_ABA_PERG_RESP
                                                    .Where(y => y.PERG_ID == pergunta.ID)
                                                    .Select(respostaPergunta => new QUESTIONARIO_RESPOSTA
                                                    {
                                                        ID = respostaPergunta.ID,
                                                        ORDEM = respostaPergunta.ORDEM,
                                                        PERG_ID = respostaPergunta.PERG_ID,
                                                        RESP_COD = respostaPergunta.RESP_COD,
                                                        RESP_DSC = respostaPergunta.RESP_DSC
                                                    }).ToList(),
                                                WFD_INFORM_COMPL = idSolicitacao != null
                                                    ? pergunta.WFD_INFORM_COMPL
                                                        .Where(
                                                            y =>
                                                                y.PERG_ID == pergunta.ID &&
                                                                y.SOLICITACAO_ID == idSolicitacao)
                                                        .Select(respostaPergunta => new WFD_INFORM_COMPL
                                                        {
                                                            ID = respostaPergunta.ID,
                                                            PERG_ID = respostaPergunta.PERG_ID,
                                                            RESPOSTA = respostaPergunta.RESPOSTA,
                                                            SOLICITACAO_ID = respostaPergunta.SOLICITACAO_ID
                                                        }).ToList()
                                                    : null
                                            }).ToList()
                                    }).ToList()
                                }).ToList()
                        }
                    })
                    .FirstOrDefault().WFD_PJPF;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<QuestionarioDinamico> BuscarQuestionarioDinamico(QuestionarioDinamicoFiltrosDTO filtros)
        {
            var questionarioList = new List<QuestionarioDinamico>();
            foreach (var questionario in _questionarioRepository.Find(x => x.CONTRATANTE_ID == filtros.ContratanteId))
            {
                if (questionario.QIC_QUESTIONARIO_CATEGORIA.Any())
                {
                    if (!questionario.QIC_QUESTIONARIO_CATEGORIA.Any(x => x.CATEGORIA_ID == filtros.CategoriaId))
                    {
                        continue;
                    }
                }

                var abaList = new List<AbaQuestionarioDinamico>();
                foreach (var aba in questionario.QIC_QUEST_ABA.OrderBy(x => x.ORDEM))
                {
                    var perguntaList = new List<PerguntaAbaDinamico>();
                    foreach (var pergunta in aba.QIC_QUEST_ABA_PERG.OrderBy(x => x.ORDEM))
                    {
                        #region Validar qual é o papel do usuário naquele momento

                        var perguntaPapelFornecedor = pergunta.QIC_QUEST_ABA_PERG_PAPEL
                            .FirstOrDefault(x => x.PAPEL_ID == filtros.PapelId);

                        #endregion

                        if (perguntaPapelFornecedor != null)
                        {
                            if (!perguntaPapelFornecedor.LEITURA) //Se o usuário não poderá visualizar aquela pergunta
                                continue; //Próxima pergunta

                            var perguntaNova = new PerguntaAbaDinamico
                            {
                                PerguntaId = pergunta.ID,
                                Visivel = true,
                                Bloqueado = !perguntaPapelFornecedor.ESCRITA,
                                Obrigatorio = perguntaPapelFornecedor.OBRIG,
                                Titulo = pergunta.PERG_NM,
                                Tamanho = pergunta.RESP_TAMANHO,
                                EPai = pergunta.E_PAI,
                                AbaId = pergunta.QUEST_ABA_ID,
                                PerguntaPai = pergunta.PERG_PAI,
                                Dominio = pergunta.DOMINIO,
                                ExibeNome = pergunta.EXIBE_NM,
                                SolicitacaoId = filtros.SolicitacaoId
                            };
                            //if (pergunta.ID == 37)
                            //    System.Diagnostics.Debugger.Break();

                            #region Validar se está em solicitação ou em alteração

                            if (filtros.Alteracao)
                                //Se estiver em qualquer parte dos fluxos olhar INFORM_COMPL atrelado a sua solicitacao
                            {
                                #region Validar se é filho e se deve bloquea-lo

                                if (pergunta.PERG_PAI != null)
                                {
                                    //if (perguntaNova.Bloqueado)
                                    //    //SEGUE 
                                    //else


                                    if (filtros.SolicitacaoId > 0)
                                    {
                                        var respostaPaiGravada = _informacaoComplementarRepository
                                            .BuscarPorPerguntaIdSolicitacaoId(
                                                (int) pergunta.PERG_PAI, filtros.SolicitacaoId);
                                        if (respostaPaiGravada == null)
                                            perguntaNova.Visivel = false;
                                    }
                                    else
                                    {
                                        var respostaPaiGravada = _informacaoComplementarRepository
                                            .BuscarPorPerguntaIdFornecedorId(
                                                (int) pergunta.PERG_PAI, filtros.ContratantePJPFId);
                                        if (respostaPaiGravada == null)
                                            perguntaNova.Visivel = false;
                                    }
                                    //perguntaNova.Visivel = false;
                                }

                                #endregion

                                #region validar se há resposta no banco para aquele fornecedor

                                var respostaFornecedor = pergunta.WFD_PJPF_INFORM_COMPL
                                    .FirstOrDefault(
                                        x =>
                                            x.PERG_ID == pergunta.ID &&
                                            x.CONTRATANTE_PJPF_ID == filtros.ContratantePJPFId);
                                if (respostaFornecedor != null)
                                {
                                    perguntaNova.RespostaFornecedorId = respostaFornecedor.ID;
                                    perguntaNova.RespostaFornecedor = respostaFornecedor.RESPOSTA;
                                }

                                #endregion

                                #region validar se há resposta no banco para aquela solicitação

                                var respostaSolicitacao = pergunta.WFD_INFORM_COMPL
                                    .FirstOrDefault(
                                        x => x.PERG_ID == pergunta.ID && x.SOLICITACAO_ID == filtros.SolicitacaoId);
                                if (respostaSolicitacao != null)
                                {
                                    perguntaNova.RespostaId = respostaSolicitacao.ID;
                                    perguntaNova.Resposta = respostaSolicitacao.RESPOSTA;

                                    if (pergunta.DOMINIO == true)
                                        perguntaNova.Resposta =
                                            respostaSolicitacao.QIC_QUEST_ABA_PERG.QIC_QUEST_ABA_PERG_RESP
                                                .FirstOrDefault(
                                                    x => x.ID.ToString().Contains(respostaSolicitacao.RESPOSTA))
                                                .RESP_DSC;
                                }

                                #endregion

                                #region Validar qual o tipo de dado ele é

                                if (pergunta.DOMINIO == true) // ele tem um lista de respostas associadas
                                {
                                    if (pergunta.TP_DADO == "RADIO")
                                        perguntaNova.TpDadoDominio = EnumTipoDadoDominio.RadioButton;
                                    if (pergunta.TP_DADO == "CHECKBOX")
                                        perguntaNova.TpDadoDominio = EnumTipoDadoDominio.Checkbox;
                                    if (pergunta.TP_DADO == "DROPDOWNLIST")
                                        perguntaNova.TpDadoDominio = EnumTipoDadoDominio.DropdownList;
                                    perguntaNova.ListaSelecionavel = pergunta.QIC_QUEST_ABA_PERG_RESP
                                        .OrderBy(y => y.ORDEM)
                                        .Select(x => new RespostasPossiveis
                                        {
                                            Id = x.ID,
                                            Texto = x.RESP_DSC,
                                            Valor = x.RESP_COD
                                        }).ToList();
                                }

                                #endregion
                            }
                            else //Se estiver em Alteração ler de PJPF_INFORM_COMPL
                            {
                                if (filtros.FornecedorId != 0)
                                {
                                    #region validar se há resposta no banco para aquela solicitação

                                    var respostaGravada = pergunta.WFD_PJPF_INFORM_COMPL
                                        .FirstOrDefault(x => x.PERG_ID == pergunta.ID
                                                             && x.CONTRATANTE_PJPF_ID == filtros.FornecedorId);
                                    if (respostaGravada != null)
                                        perguntaNova.Resposta = respostaGravada.RESPOSTA;

                                    #endregion

                                    #region Validar qual o tipo de dado ele é

                                    if (pergunta.DOMINIO == true) // ele tem um lista de respostas associadas
                                    {
                                        if (pergunta.TP_DADO == "RADIO")
                                            perguntaNova.TpDadoDominio = EnumTipoDadoDominio.RadioButton;
                                        if (pergunta.TP_DADO == "CHECKBOX")
                                            perguntaNova.TpDadoDominio = EnumTipoDadoDominio.Checkbox;
                                        if (pergunta.TP_DADO == "DROPDOWNLIST")
                                            perguntaNova.TpDadoDominio = EnumTipoDadoDominio.DropdownList;

                                        if (respostaGravada == null)
                                            perguntaNova.ListaSelecionavel = pergunta.QIC_QUEST_ABA_PERG_RESP
                                                .OrderBy(y => y.ORDEM)
                                                .Select(x => new RespostasPossiveis
                                                {
                                                    Id = x.ID,
                                                    Texto = x.RESP_DSC,
                                                    Valor = x.RESP_COD
                                                }).ToList();
                                        else
                                            perguntaNova.ListaSelecionavel = pergunta.QIC_QUEST_ABA_PERG_RESP
                                                .Where(z => z.RESP_PAI_ID == respostaGravada.PERG_ID)
                                                .OrderBy(y => y.ORDEM)
                                                .Select(x => new RespostasPossiveis
                                                {
                                                    Id = x.ID,
                                                    Texto = x.RESP_DSC,
                                                    Valor = x.RESP_COD
                                                }).ToList();
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region Validar se é filho e se deve bloquea-lo

                                    if (pergunta.PERG_PAI != null)
                                    {
                                        var respostaPaiGravada = _informacaoComplementarRepository
                                            .BuscarPorPerguntaIdSolicitacaoId
                                            ((int) pergunta.PERG_PAI, filtros.SolicitacaoId);
                                        if (respostaPaiGravada != null)
                                            perguntaNova.Visivel = false;
                                    }

                                    #endregion

                                    #region validar se há resposta no banco para aquela solicitação

                                    var respostaGravada = pergunta.WFD_INFORM_COMPL
                                        .FirstOrDefault(x => x.PERG_ID == pergunta.ID
                                                             && x.SOLICITACAO_ID == filtros.SolicitacaoId);
                                    if (respostaGravada != null)
                                    {
                                        perguntaNova.RespostaId = respostaGravada.ID;
                                        perguntaNova.Resposta = respostaGravada.RESPOSTA;

                                        if (pergunta.DOMINIO == true)
                                            perguntaNova.Resposta =
                                                respostaGravada.QIC_QUEST_ABA_PERG.QIC_QUEST_ABA_PERG_RESP
                                                    .FirstOrDefault(
                                                        x => x.ID.ToString().Contains(respostaGravada.RESPOSTA))
                                                    .RESP_DSC;
                                    }

                                    #endregion

                                    #region Validar qual o tipo de dado ele é

                                    if (pergunta.DOMINIO == true) // ele tem um lista de respostas associadas
                                    {
                                        if (pergunta.TP_DADO == "RADIO")
                                            perguntaNova.TpDadoDominio = EnumTipoDadoDominio.RadioButton;
                                        if (pergunta.TP_DADO == "CHECKBOX")
                                            perguntaNova.TpDadoDominio = EnumTipoDadoDominio.Checkbox;
                                        if (pergunta.TP_DADO == "DROPDOWNLIST")
                                            perguntaNova.TpDadoDominio = EnumTipoDadoDominio.DropdownList;

                                        if (respostaGravada == null)
                                            perguntaNova.ListaSelecionavel = pergunta.QIC_QUEST_ABA_PERG_RESP
                                                .OrderBy(y => y.ORDEM)
                                                .Select(x => new RespostasPossiveis
                                                {
                                                    Id = x.ID,
                                                    Texto = x.RESP_DSC,
                                                    Valor = x.RESP_COD
                                                }).ToList();
                                        else
                                            perguntaNova.ListaSelecionavel = pergunta.QIC_QUEST_ABA_PERG_RESP
                                                .Where(z => z.RESP_PAI_ID == respostaGravada.PERG_ID)
                                                .OrderBy(y => y.ORDEM)
                                                .Select(x => new RespostasPossiveis
                                                {
                                                    Id = x.ID,
                                                    Texto = x.RESP_DSC,
                                                    Valor = x.RESP_COD
                                                }).ToList();
                                    }

                                    #endregion
                                }

                                //validar se é filho
                                //se o pai estiver respondido - Desbloqueia
                                //se o pai não estiver respondido - Bloqueia
                            }

                            #endregion

                            //Saber se ela é pai
                            // se ela é um dominio
                            // se é uma lista
                            //integrar radiobutton = resposta unica
                            //integrar checkbox = resposta multipla separado por ^

                            //validar se já é a 3ª pergunta para adicionar o clearfix
                            if ((perguntaList.Count() + 1)%3 == 0)
                                perguntaNova.PulaLinha = true;

                            perguntaList.Add(perguntaNova);
                        }
                    }

                    #region AbaList

                    abaList.Add(new AbaQuestionarioDinamico
                    {
                        Titulo = aba.ABA_NM,
                        Descricao = aba.ABA_DSC,
                        Perguntas = perguntaList,
                        QuestionarioId = aba.QUESTIONARIO_ID
                    });

                    #endregion
                }

                #region QuestionarioList

                questionarioList.Add(new QuestionarioDinamico
                {
                    QuestionarioID = questionario.ID,
                    Titulo = questionario.QUEST_NM,
                    Descricao = questionario.QUEST_DSC,
                    Abas = abaList,
                    ContratanteId = questionario.CONTRATANTE_ID,
                    ExibeDadosBancarios = questionario.LE_D_BANCARIO,
                    ExibeDadosContato = questionario.LE_D_CONTATO,
                    ExibeDadosGerais = questionario.LE_D_GERAIS,
                    ExibeInformacaoComplementar = questionario.LE_INFO_COMPL
                });

                #endregion
            }
            return questionarioList;
        }

        public void Dispose()
        {
        }
    }
}