using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class QuestionarioPerguntaRepository : EntityFrameworkRepository<QUESTIONARIO_PERGUNTA, WebForLinkContexto>,
        IQuestionarioPerguntaWebForLinkRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="idPai"></param>
        /// <returns></returns>
        public List<QUESTIONARIO_PERGUNTA> BuscarPorPerguntasFilho(int idPai)
        {
            try
            {
                return DbSet
                    .Include("QIC_QUEST_ABA_PERG_RESP")
                    .Where(x => x.PERG_PAI == idPai)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}