using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class QuestionarioAbaPerguntaWebForLinkAppService : AppService<WebForLinkContexto>,
        IQuestionarioAbaPerguntaWebForLinkAppService
    {
        private readonly IQuestionarioPerguntaWebForLinkService _perguntaQuestionarioService;

        public QuestionarioAbaPerguntaWebForLinkAppService(
            IQuestionarioPerguntaWebForLinkService perguntaQuestionarioService)
        {
            _perguntaQuestionarioService = perguntaQuestionarioService;
            try
            {
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public QUESTIONARIO_PERGUNTA Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PERGUNTA> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PERGUNTA> Find(Expression<Func<QUESTIONARIO_PERGUNTA, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(QUESTIONARIO_PERGUNTA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(QUESTIONARIO_PERGUNTA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(QUESTIONARIO_PERGUNTA entity)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA Get(int id)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA Get(Expression<Func<QUESTIONARIO_PERGUNTA, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PERGUNTA> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PERGUNTA> Find(Expression<Func<QUESTIONARIO_PERGUNTA, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="idPai"></param>
        /// <returns></returns>
        public List<QUESTIONARIO_PERGUNTA> BuscarPorPerguntasFilho(int idPai)
        {
            try
            {
                return _perguntaQuestionarioService.Find(x => x.PERG_PAI == idPai).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idPapel"></param>
        /// <param name="idPergunta"></param>
        /// <param name="idResposta"></param>
        /// <returns></returns>
        public List<PerguntaDTO> ListarTodasPerguntasQuestionario(int idPapel, int idPergunta, int idResposta)
        {
            var filhos = BuscarPorPerguntasFilho(idPergunta);
            var partials = new List<PerguntaDTO>();
            foreach (var pergunta in filhos)
            {
                var papelPergunta = pergunta.QIC_QUEST_ABA_PERG_PAPEL.FirstOrDefault(x => x.PAPEL_ID == idPapel);
                if (papelPergunta != null)
                {
                    if (papelPergunta.LEITURA)
                    {
                        var respostaBanco = string.Empty;
                        var perguntaModelo = new PerguntaDTO
                        {
                            Id = pergunta.ID,
                            AbaId = pergunta.QUEST_ABA_ID,
                            Dominio = pergunta.DOMINIO ?? false,
                            Titulo = pergunta.PERG_NM,
                            EPai = pergunta.E_PAI,
                            PerguntaPai = pergunta.PERG_PAI,
                            Leitura = papelPergunta.LEITURA,
                            Obrigatorio = papelPergunta.OBRIG
                        };

                        var resposta = pergunta.WFD_INFORM_COMPL
                            .FirstOrDefault(x => x.PERG_ID == pergunta.ID);
                        if (resposta != null)
                        {
                            perguntaModelo.Resposta = resposta.RESPOSTA;
                            perguntaModelo.RespostaId = resposta.ID;
                        }
                        if (pergunta.TP_DADO == "DOMINIO" && pergunta.DOMINIO == true)
                        {
                            perguntaModelo.DominioList =
                                pergunta.QIC_QUEST_ABA_PERG_RESP.Where(x => x.RESP_PAI_ID == idResposta)
                                    .OrderBy(x => x.ORDEM)
                                    .ToList();
                        }
                        perguntaModelo.Escrita = papelPergunta.ESCRITA;
                        partials.Add(perguntaModelo);
                    }
                }
            }
            return partials;
        }
    }
}