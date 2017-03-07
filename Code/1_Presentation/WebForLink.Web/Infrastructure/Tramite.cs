using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.Infrastructure
{
    public interface ITramiteWebForLinkAppService
    {
        void AtualizarTramite(int contratanteId, int solicitacaoId, int fluxoId, int papelAtualId, int statusId, int? usuarioId);
        void AlterarSolicitacaoParaFinalizado(int solicitacaoId, int statusId);
        List<SOLICITACAO_TRAMITE> RetornarSolicitacaoTramiteAtual(int solicitacaoId);
        List<SOLICITACAO_TRAMITE> ListarProximoPapeisFluxo(int contratanteId, int fluxoId, int papelAtualId, int v);
        SOLICITACAO_TRAMITE InserirTramiteInicial(int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino);
        void InserirTramiteSequencia(int contratanteId, int fluxoId, int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino, List<SOLICITACAO_TRAMITE> tramiteAtual, List<FLUXO_SEQUENCIA> proximoPapeis);
    }

    public class TramiteWebForLinkAppService : ITramiteWebForLinkAppService
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region BPs de Chamada
        private readonly ISolicitacaoTramiteWebForLinkAppService _solicitacaoTramiteBP;
        private readonly ITramiteWebForLinkAppService _tramiteBp;
        public TramiteWebForLinkAppService(ISolicitacaoTramiteWebForLinkAppService solicitacaoTramite, ITramiteWebForLinkAppService tramite)
        {
            _solicitacaoTramiteBP = solicitacaoTramite;
            _tramiteBp = tramite;
        }

        public void AlterarSolicitacaoParaFinalizado(int solicitacaoId, int statusId)
        {
            throw new NotImplementedException();
        }
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
        public void AtualizarTramite(int contratanteId, int solicitacaoId, int fluxoId, int papelAtualId, int statusId, int? usuarioId)
        {
            int? grupoDestino = 0;

            SOLICITACAO_TRAMITE tramite;

            List<SOLICITACAO_TRAMITE> tramiteAtual = new List<SOLICITACAO_TRAMITE>();
            
                tramiteAtual = _tramiteBp.RetornarSolicitacaoTramiteAtual(solicitacaoId);

                grupoDestino = tramiteAtual.Count > 0
                    ? tramiteAtual.Single(t => t.PAPEL_ID == papelAtualId).GRUPO_DESTINO
                    : _tramiteBp.ListarProximoPapeisFluxo(contratanteId, fluxoId, papelAtualId, 1).FirstOrDefault().GRUPO_DESTINO;

                try
                {
                    switch (statusId)
                    {
                        case 1:
                            tramite = _tramiteBp.InserirTramiteInicial(solicitacaoId, papelAtualId, statusId, (int)usuarioId, (int)grupoDestino);
                            break;
                        case 2:
                            List<FLUXO_SEQUENCIA> proximoPapeis = new List<FLUXO_SEQUENCIA>();
                                _tramiteBp.InserirTramiteSequencia(contratanteId, fluxoId, solicitacaoId, papelAtualId, statusId, (int)usuarioId, (int)grupoDestino, tramiteAtual, proximoPapeis);
                            EmailSolicitacao emailSolicitacao = new EmailSolicitacao();
                            emailSolicitacao.EnviarEmailSolicitacao(contratanteId, solicitacaoId, fluxoId, proximoPapeis);
                            break;
                        case 3:
                        case 6:
                            tramite = tramiteAtual.Single(t => t.PAPEL_ID == papelAtualId);
                            tramite.SOLICITACAO_STATUS_ID = statusId; // Reprovado
                            tramite.USUARIO_ID = usuarioId;
                            tramite.TRMITE_DT_FIM = DateTime.Now;

                        _tramiteBp.AlterarSolicitacaoParaFinalizado(solicitacaoId, statusId); // Reprovado

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

        public SOLICITACAO_TRAMITE InserirTramiteInicial(int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino)
        {
            throw new NotImplementedException();
        }

        public void InserirTramiteSequencia(int contratanteId, int fluxoId, int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino, List<SOLICITACAO_TRAMITE> tramiteAtual, List<FLUXO_SEQUENCIA> proximoPapeis)
        {
            throw new NotImplementedException();
        }

        public List<SOLICITACAO_TRAMITE> ListarProximoPapeisFluxo(int contratanteId, int fluxoId, int papelAtualId, int v)
        {
            throw new NotImplementedException();
        }

        public List<SOLICITACAO_TRAMITE> RetornarSolicitacaoTramiteAtual(int solicitacaoId)
        {
            throw new NotImplementedException();
        }
    }
    }