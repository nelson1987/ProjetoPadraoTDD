using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForDocs.Business.Process
{
    public class CargaBP
    {
        #region Acesso BM externa
        private readonly CargaBM cargaBM = new CargaBM();
        #endregion
        /// <summary>
        /// Listar Todas as solicitações aprovadas
        /// </summary>
        /// <returns></returns>
        public List<SOLICITACAO> ListarSolicitacoesAprovadas()
        {
            return new List<SOLICITACAO>();
        }
    }
}
