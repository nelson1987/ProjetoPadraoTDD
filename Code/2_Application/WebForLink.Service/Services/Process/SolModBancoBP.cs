using System;
using System.Collections.Generic;
using System.Linq;
using WebForDocs.Business.Infrastructure.Exceptions;
using WebForDocs.Dominio.Models;

namespace WebForDocs.Business.Process
{
    public class SolModBancoBP : BPBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<WFD_SOL_MOD_BANCO> BuscarPorSolicitacaoId(int id)
        {
            try
            {
                return Db.WFD_SOL_MOD_BANCO.Include("T_BANCO").Where(x => x.SOLICITACAO_ID == id).ToList();
            }
            catch (Exception ex)
            {
                throw new WFLBusinessException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }
    }
}
