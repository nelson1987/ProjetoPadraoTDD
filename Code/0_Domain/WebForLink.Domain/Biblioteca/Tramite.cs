using System;
using System.Collections.Generic;
using System.Linq;
using WebForDocs.Business.Fornecedores;
using WebForDocs.Business.Process;
using WebForDocs.Dominio.Models;

namespace WebForDocs.Biblioteca
{
    public static class Tramite
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region BPs de Chamada
        private static readonly SolicitacaoTramiteBP solicitacaoTramiteBP = new SolicitacaoTramiteBP();
        #endregion

        /// <summary>
        /// Ver SolicitacaoTramiteBP.AtualizarTramite
        /// </summary>
        /// <param name="db"></param>
        /// <param name="contratanteId"></param>
        /// <param name="solicitacaoId"></param>
        /// <param name="fluxoId"></param>
        /// <param name="papelAtualId"></param>
        /// <param name="statusId"></param>
        /// <param name="Usuario"></param>
        public static void AtualizaTramite(int contratanteId, int solicitacaoId, int fluxoId, int papelAtualId, int statusId, int? usuarioId)
        {
            int? grupoDestino = 0;

            WFD_SOLICITACAO_TRAMITE tramite;

            List<WFD_SOLICITACAO_TRAMITE> tramiteAtual = new List<WFD_SOLICITACAO_TRAMITE>();

            using (var bPTramite = new TramiteBP())
            {
                tramiteAtual = bPTramite.RetornarSolicitacaoTramiteAtual(solicitacaoId);

                grupoDestino = tramiteAtual.Count > 0
                    ? tramiteAtual.Single(t => t.PAPEL_ID == papelAtualId).GRUPO_DESTINO
                    : bPTramite.ListarProximoPapeisFluxo(contratanteId, fluxoId, papelAtualId, 1).FirstOrDefault().GRUPO_DESTINO;

                try
                {
                    switch (statusId)
                    {
                        case 1:
                            tramite = bPTramite.InserirTramiteInicial(solicitacaoId, papelAtualId, statusId, (int)usuarioId, (int)grupoDestino);
                            break;
                        case 2:
                            List<WFL_FLUXO_SEQUENCIA> proximoPapeis = new List<WFL_FLUXO_SEQUENCIA>();
                            using(var tramiteBp = new TramiteBP())
                                tramiteBp.InserirTramiteSequencia(contratanteId, fluxoId, solicitacaoId, papelAtualId, statusId, (int)usuarioId, (int)grupoDestino, tramiteAtual, proximoPapeis);
                            EmailSolicitacao emailSolicitacao = new EmailSolicitacao();
                            emailSolicitacao.EnviarEmailSolicitacao(contratanteId, solicitacaoId, fluxoId, proximoPapeis);
                            break;
                        case 3:
                        case 6:
                            tramite = tramiteAtual.Single(t => t.PAPEL_ID == papelAtualId);
                            tramite.SOLICITACAO_STATUS_ID = statusId; // Reprovado
                            tramite.USUARIO_ID = usuarioId;
                            tramite.TRMITE_DT_FIM = DateTime.Now;

                            bPTramite.AlterarSolicitacaoParaFinalizado(solicitacaoId, statusId); // Reprovado

                            EmailSolicitacao emailReprovacao = new EmailSolicitacao();
                            emailReprovacao.EnviarEmailReprovacao(solicitacaoId);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Error ao percorrer a lista 'lstFornecedorRobo' exception: {0}", ex));
                }
            }
        }
    }
}