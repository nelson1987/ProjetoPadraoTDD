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
    public class QuestionarioWebForLinkRepository : EntityFrameworkRepository<QUESTIONARIO, WebForLinkContexto>,
        IQuestionarioWebForLinkRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="idContratante"></param>
        /// <returns></returns>
        public List<QUESTIONARIO> ListarTodosPorIdContratante(int idContratante)
        {
            try
            {
                return DbSet.Include("QIC_QUEST_ABA")
                    .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG")
                    .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.QIC_QUEST_ABA_PERG_PAPEL")
                    .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.QIC_QUEST_ABA_PERG_RESP")
                    .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.WFD_INFORM_COMPL")
                    .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.WFD_PJPF_INFORM_COMPL")
                    .Include("QIC_QUESTIONARIO_CATEGORIA")
                    .Where(x => x.CONTRATANTE_ID == idContratante)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Questionários por idContratante",
                    ex);
            }
        }

        public QUESTIONARIO BuscarPorIdEIdContratante(int id, int idContratante)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.ID == id && x.CONTRATANTE_ID == idContratante);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Questionários por idContratante",
                    ex);
            }
        }
    }
}