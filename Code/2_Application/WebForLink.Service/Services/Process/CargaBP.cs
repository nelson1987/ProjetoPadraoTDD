using System.Collections.Generic;
using WebForDocs.Business.Manager;
using WebForDocs.Data.ModeloDB;

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
        public List<WFD_SOLICITACAO> ListarSolicitacoesAprovadas()
        {
            return new List<WFD_SOLICITACAO>();
        }
    }
}
