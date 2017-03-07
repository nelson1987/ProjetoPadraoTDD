using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class QuestionarioWebForLinkAppService : AppService<WebForLinkContexto>, IQuestionarioWebForLinkAppService
    {
        private readonly IQuestionarioWebForLinkService _questionarioService;
        private readonly IInformacaoComplementarWebForLinkService _resposta;

        public QuestionarioWebForLinkAppService(IQuestionarioWebForLinkService questionario,
            IInformacaoComplementarWebForLinkService resposta)
        {
            try
            {
                _questionarioService = questionario;
                _resposta = resposta;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public QUESTIONARIO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO> Find(Expression<Func<QUESTIONARIO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(QUESTIONARIO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(QUESTIONARIO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(QUESTIONARIO entity)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO Get(int id)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO Get(Expression<Func<QUESTIONARIO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO> Find(Expression<Func<QUESTIONARIO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Buscar Questionário por Id
        /// </summary>
        /// <param name="id">Id de Questionário</param>
        /// <returns>Questionário</returns>
        public QUESTIONARIO BuscarPorId(int id)
        {
            try
            {
                return _questionarioService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        public QUESTIONARIO BuscarPorIdEIdContratante(int id, int idContratante)
        {
            try
            {
                return _questionarioService.BuscarPorIdEIdContratante(id, idContratante);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Questionários por idContratante", ex);
            }
        }

        /// <summary>
        ///     Lista todos os Questionários
        /// </summary>
        /// <returns>Lista de Questionários</returns>
        public List<QUESTIONARIO> ListarTodos()
        {
            try
            {
                return _questionarioService.All().ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idContratante"></param>
        /// <returns></returns>
        public List<QUESTIONARIO> ListarTodosPorIdContratante(int idContratante)
        {
            try
            {
                return _questionarioService.Find(x => x.CONTRATANTE_ID == idContratante).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Questionários por idContratante", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idContratante"></param>
        /// <param name="idPapel"></param>
        /// <param name="idSolicitacao"></param>
        /// <returns></returns>
        public List<QUESTIONARIO> BuscarQuestionarioPorIdContratante(int idContratante, int idPapel, int? idSolicitacao)
        {
            var questionarioLst = ListarTodosPorIdContratante(idContratante);

            #region qicQuestionario

            if (questionarioLst != null)
            {
                questionarioLst.Select(questionariosLst => new QUESTIONARIO
                {
                    ID = questionariosLst.ID,
                    LE_D_BANCARIO = questionariosLst.LE_D_BANCARIO,
                    LE_D_CONTATO = questionariosLst.LE_D_CONTATO,
                    LE_D_GERAIS = questionariosLst.LE_D_GERAIS,
                    LE_INFO_COMPL = questionariosLst.LE_INFO_COMPL,
                    QUEST_DSC = questionariosLst.QUEST_DSC,
                    QUEST_NM = questionariosLst.QUEST_NM,
                    CONTRATANTE_ID = questionariosLst.CONTRATANTE_ID,
                    QIC_QUEST_ABA = questionariosLst.QIC_QUEST_ABA.Where(
                        x => x.QIC_QUEST_ABA_PERG.Any(y =>
                            y.QIC_QUEST_ABA_PERG_PAPEL.Any(z =>
                                z.PAPEL_ID == idPapel)))
                        .Select(aba =>
                            new QUESTIONARIO_ABA
                            {
                                ID = aba.ID,
                                ABA_DSC = aba.ABA_DSC,
                                ABA_NM = aba.ABA_NM,
                                ORDEM = aba.ORDEM,
                                QUESTIONARIO_ID = questionariosLst.ID,
                                //Trazer perguntas e resposta por Papel da Pergunta
                                QIC_QUEST_ABA_PERG = aba.QIC_QUEST_ABA_PERG.Where(x =>
                                    x.QIC_QUEST_ABA_PERG_PAPEL.Any(z => z.LEITURA)
                                    && x.QIC_QUEST_ABA_PERG_PAPEL.Any(z => z.PAPEL_ID == idPapel))
                                    .OrderBy(x => x.ORDEM)
                                    .Select(pergunta => new QUESTIONARIO_PERGUNTA
                                    {
                                        ID = pergunta.ID,
                                        ORDEM = pergunta.ORDEM,
                                        PERG_NM = pergunta.PERG_NM,
                                        QUEST_ABA_ID = pergunta.QUEST_ABA_ID,
                                        DOMINIO = pergunta.DOMINIO,
                                        EXIBE_NM = pergunta.EXIBE_NM,
                                        TP_DADO = pergunta.TP_DADO,
                                        //Trazer status da Pergunta pelo Papel atual da solicitação e do Usuário
                                        QIC_QUEST_ABA_PERG_PAPEL = pergunta.QIC_QUEST_ABA_PERG_PAPEL
                                            .Where(x => x.PAPEL_ID == idPapel
                                                        && x.PERG_ID == pergunta.ID).ToList(),
                                        //Se houver IdSolicitacao verificar se trouxe resposta
                                        WFD_INFORM_COMPL = idSolicitacao != null
                                            ? pergunta.WFD_INFORM_COMPL.Where(x => x.PERG_ID == pergunta.ID
                                                                                   && x.SOLICITACAO_ID == idSolicitacao)
                                                .ToList()
                                            : pergunta.WFD_INFORM_COMPL.Where(x => x.PERG_ID == pergunta.ID).ToList(),
                                        //Se TP_DADO == Dominio então buscar lista de dominio de QIC_QUEST_ABA_PERG_RESP
                                        QIC_QUEST_ABA_PERG_RESP = pergunta.TP_DADO.Contains("Dominio")
                                            ? pergunta.QIC_QUEST_ABA_PERG_RESP.Where(x => x.PERG_ID == pergunta.ID)
                                                .ToList()
                                            : null
                                    }).ToList()
                            }).ToList()
                });
                return questionarioLst;
            }

            #endregion

            var qicQuestionario = new List<QUESTIONARIO>();
            return qicQuestionario;
        }

        /// <summary>
        ///     Buscar questionário por Filtros.
        ///     Pela necessidade de mudança no questionário durante sua execução dependendo da resposta que for dada em algumas
        ///     perguntas.
        /// </summary>
        /// <param name="idQuestionario">Id do Questionário</param>
        /// <param name="idAba">Id da Aba</param>
        /// <param name="idPergunta">Id da Pergunta</param>
        /// <param name="resposta">Resposta da Pergunta </param>
        /// <returns>Questionário</returns>
        public QUESTIONARIO BuscarPorIdAbaPerguntaResposta(int idQuestionario, int idAba, int idPergunta,
            string resposta)
        {
            return BuscarPorId(int.Parse(resposta));
        }

        /// <summary>
        /// </summary>
        /// <param name="questionarioDinamicoFiltros"></param>
        /// <returns></returns>
        public List<PerguntaDTO> BuscarQuestionarioFornecedor(QuestionarioDinamicoFiltrosDTO questionarioDinamicoFiltros)
        {
            var perguntaDtos = new List<PerguntaDTO>();
            var questionarios = ListarTodosPorIdContratante(questionarioDinamicoFiltros.ContratanteId);
            foreach (var questionarioDinamico in questionarios)
            {
                var abas = questionarioDinamico.QIC_QUEST_ABA
                    .OrderBy(x => x.ORDEM)
                    .ToList();
                foreach (var aba in abas)
                {
                    var perguntas = aba.QIC_QUEST_ABA_PERG
                        .OrderBy(x => x.ORDEM)
                        .ToList();
                    foreach (var pergunta in perguntas)
                    {
                        #region Validar qual é o papel do usuário naquele momento

                        var perguntaPapelFornecedor = pergunta.QIC_QUEST_ABA_PERG_PAPEL
                            .FirstOrDefault(x => x.PAPEL_ID == questionarioDinamicoFiltros.PapelId);

                        #endregion

                        if (perguntaPapelFornecedor != null)
                        {
                            if (!perguntaPapelFornecedor.LEITURA) //Se o usuário não poderá visualizar aquela pergunta
                                continue; //Próxima pergunta

                            var perguntaNova =
                                new PerguntaDTO(pergunta.ID, true, !perguntaPapelFornecedor.ESCRITA,
                                    perguntaPapelFornecedor.OBRIG);

                            #region Validar se está em solicitação ou em alteração

                            if (questionarioDinamicoFiltros.Alteracao)
                                //Se estiver em qualquer parte dos fluxos olhar INFORM_COMPL atrelado a sua solicitacao
                            {
                                #region validar se há resposta no banco para aquele fornecedor

                                var respostaFornecedor = pergunta.WFD_PJPF_INFORM_COMPL
                                    .FirstOrDefault(x => x.PERG_ID == pergunta.ID);
                                if (respostaFornecedor != null)
                                    perguntaNova.Resposta = respostaFornecedor.RESPOSTA;

                                #endregion
                            }
                            else //Se estiver em Alteração ler de PJPF_INFORM_COMPL
                            {
                                #region Validar se é filho e se deve bloquea-lo

                                if (pergunta.PERG_PAI != null)
                                {
                                    var respostaPaiGravada = _resposta.BuscarPorPerguntaIdSolicitacaoId(
                                        (int) pergunta.PERG_PAI, questionarioDinamicoFiltros.SolicitacaoId);
                                    if (respostaPaiGravada != null)
                                        perguntaNova.Visivel = false;
                                }

                                #endregion

                                #region validar se há resposta no banco para aquela solicitação

                                var respostaGravada = pergunta.WFD_INFORM_COMPL
                                    .FirstOrDefault(x => x.SOLICITACAO_ID == questionarioDinamicoFiltros.SolicitacaoId);
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
                                            .Select(x => new
                                            {
                                                Id = x.ID,
                                                Texto = x.RESP_DSC,
                                                Valor = x.RESP_COD
                                            }).ToList();
                                    else
                                        perguntaNova.ListaSelecionavel = pergunta.QIC_QUEST_ABA_PERG_RESP
                                            .Where(z => z.RESP_PAI_ID == respostaGravada.PERG_ID)
                                            .OrderBy(y => y.ORDEM)
                                            .Select(x => new
                                            {
                                                Id = x.ID,
                                                Texto = x.RESP_DSC,
                                                Valor = x.RESP_COD
                                            }).ToList();
                                }

                                #endregion

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
                            perguntaDtos.Add(perguntaNova);
                        }
                    }
                }
            }
            return perguntaDtos;
        }

        #region Acesso BM externa

        #endregion
    }
}