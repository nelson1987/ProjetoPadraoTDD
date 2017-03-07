using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class QuestionarioAbaPerguntaService : Service<QUESTIONARIO_PERGUNTA>
    {
        private readonly IQuestionarioPerguntaWebForLinkRepository _perguntaQuestionarioRepository;

        public QuestionarioAbaPerguntaService(IQuestionarioPerguntaWebForLinkRepository perguntaQuestionarioRepository)
            : base(perguntaQuestionarioRepository)
        {
            try
            {
                _perguntaQuestionarioRepository = perguntaQuestionarioRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="idPai"></param>
        /// <returns></returns>
        public List<QUESTIONARIO_PERGUNTA> BuscarPorPerguntasFilho(int idPai)
        {
            try
            {
                return _perguntaQuestionarioRepository.Find(x => x.PERG_PAI == idPai).ToList();
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