using System;
using System.Collections.Generic;
using System.Linq;

namespace WebForDocs.Business.Process
{
    public class SolModContatoBP : PadraoBP<UnitOfWork>, IDisposable
    {
        private static UnitOfWork Processo { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public SolModContatoBP()
            : base(Processo)
        {
            try
            {
                if (Processo == null)
                    Processo = new UnitOfWork(new WFLModel());
            }
            catch (Exception ex)
            {
                throw new WFLBusinessException(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<WFD_SOL_MOD_CONTATO> ListarPorSolicitacaoId(int id)
        {
            try
            {
                return Db.WFD_SOL_MOD_CONTATO.Where(x => x.SOLICITACAO_ID == id).ToList();
            }
            catch (Exception ex)
            {
                throw new WFLBusinessException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }
    }
}
