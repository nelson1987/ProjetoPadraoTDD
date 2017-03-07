using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.Infrastructure
{
    public interface ITramiteWebForLinkAppService
    {
        void AtualizarTramite(int contratanteId, int solicitacaoId, int fluxoId, int papelAtualId, int statusId, int? usuarioId);
        //void AlterarSolicitacaoParaFinalizado(int solicitacaoId, int statusId);
        List<SOLICITACAO_TRAMITE> RetornarSolicitacaoTramiteAtual(int solicitacaoId);
        List<FLUXO_SEQUENCIA> ListarProximoPapeisFluxo(int contratanteId, int fluxoId, int papelAtualId, int v);
        SOLICITACAO_TRAMITE InserirTramiteInicial(int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino);
        void InserirTramiteSequencia(int contratanteId, int fluxoId, int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino, List<SOLICITACAO_TRAMITE> tramiteAtual, List<FLUXO_SEQUENCIA> proximoPapeis);
    }

    public class TramiteWebForLinkAppService : ITramiteWebForLinkAppService
    {
        #region BPs de Chamada
        private readonly ISolicitacaoTramiteWebForLinkAppService _solicitacaoTramiteBP;
        private readonly IFluxoSequenciaWebForLinkAppService _fluxoSequenciaBP;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoBP;

        public TramiteWebForLinkAppService(ISolicitacaoTramiteWebForLinkAppService solicitacaoTramite, 
            IFluxoSequenciaWebForLinkAppService fluxoSequencia,
            ISolicitacaoWebForLinkAppService solicitacao
            )
        {
            _solicitacaoTramiteBP = solicitacaoTramite;
            _fluxoSequenciaBP = fluxoSequencia;
            _solicitacaoBP = solicitacao;
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

            tramiteAtual = RetornarSolicitacaoTramiteAtual(solicitacaoId);

            grupoDestino = tramiteAtual.Count > 0
                ? tramiteAtual.Single(t => t.PAPEL_ID == papelAtualId).GRUPO_DESTINO
                : ListarProximoPapeisFluxo(contratanteId, fluxoId, papelAtualId, 1).FirstOrDefault().GRUPO_DESTINO;

            try
            {
                switch (statusId)
                {
                    case 1:
                        tramite = InserirTramiteInicial(solicitacaoId, papelAtualId, statusId, (int)usuarioId, (int)grupoDestino);
                        break;
                    case 2:
                        List<FLUXO_SEQUENCIA> proximoPapeis = new List<FLUXO_SEQUENCIA>();
                        InserirTramiteSequencia(contratanteId, fluxoId, solicitacaoId, papelAtualId, statusId, (int)usuarioId, (int)grupoDestino, tramiteAtual, proximoPapeis);
                        //TODO: Descomentar
                        //EmailSolicitacao emailSolicitacao = new EmailSolicitacao();
                        //emailSolicitacao.EnviarEmailSolicitacao(contratanteId, solicitacaoId, fluxoId, proximoPapeis);
                        break;
                    case 3:
                    case 6:
                        tramite = tramiteAtual.Single(t => t.PAPEL_ID == papelAtualId);
                        tramite.SOLICITACAO_STATUS_ID = statusId; // Reprovado
                        tramite.USUARIO_ID = usuarioId;
                        tramite.TRMITE_DT_FIM = DateTime.Now;

                        _solicitacaoBP.AlterarSolicitacaoParaFinalizado(solicitacaoId, statusId); // Reprovado
                                                                                   //TODO: Descomentar
                                                                                   //EmailSolicitacao emailReprovacao = new EmailSolicitacao();
                                                                                   //    emailReprovacao.EnviarEmailReprovacao(solicitacaoId);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error ao percorrer a lista 'lstFornecedorRobo' exception: {0}", ex));
            }
        }

        public SOLICITACAO_TRAMITE InserirTramiteInicial(int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino)
        {
            SOLICITACAO_TRAMITE tramite = new SOLICITACAO_TRAMITE
            {
                SOLICITACAO_ID = solicitacaoId,
                PAPEL_ID = papelAtualId,
                SOLICITACAO_STATUS_ID = statusId,
                USUARIO_ID = usuarioId,
                TRAMITE_DT_INI = DateTime.Now,
                GRUPO_DESTINO = grupoDestino
            };
            _solicitacaoTramiteBP.Create(tramite);
            return tramite;
        }

        public void InserirTramiteSequencia(int contratanteId, int fluxoId, int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino, List<SOLICITACAO_TRAMITE> tramiteAtual, List<FLUXO_SEQUENCIA> proximoPapeis)
        {
            try
            {
                SOLICITACAO_TRAMITE tramite = InserirTramiteSequencia(solicitacaoId, papelAtualId, statusId, usuarioId, grupoDestino, tramiteAtual);

                proximoPapeis = ListarProximoPapeisFluxo(contratanteId, fluxoId, papelAtualId, grupoDestino);

                // verifica se o tramite atual está todo aprovado, lembrando que pode ter mais de um tramite simultaneamente
                // senão a solicitação não pode ir para o proximo passo.
                if (!tramiteAtual.Any(t => t.SOLICITACAO_STATUS_ID == 1))
                {
                    foreach (FLUXO_SEQUENCIA item in proximoPapeis)
                    {
                        // Se não houver proximo passo o sistem finaliza a solicitacao
                        if (item.PAPEL_ID_FIM != null)
                            tramite = InserirTramiteConclusao(solicitacaoId, item);
                        else
                            _solicitacaoBP.AlterarSolicitacaoParaFinalizado(solicitacaoId, 4);// Concluido
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar inserir trâmite de sequência.", ex);
            }
        }

        public List<FLUXO_SEQUENCIA> ListarProximoPapeisFluxo(int contratanteId, int fluxoId, int papelAtualId, int grupoOrigem)
        {
            return _fluxoSequenciaBP.Find(f => f.CONTRATANTE_ID == contratanteId
                && f.FLUXO_ID == fluxoId
                && f.PAPEL_ID_INI == papelAtualId
                && f.GRUPO_ORIGEM == grupoOrigem).ToList();
        }

        public List<SOLICITACAO_TRAMITE> RetornarSolicitacaoTramiteAtual(int solicitacaoId)
        {
            return _solicitacaoTramiteBP.Find(x=>x.SOLICITACAO_ID == solicitacaoId).ToList();
        }
        private SOLICITACAO_TRAMITE InserirTramiteConclusao(int solicitacaoId, FLUXO_SEQUENCIA item)
        {
            SOLICITACAO_TRAMITE tramite = new SOLICITACAO_TRAMITE();
            tramite.SOLICITACAO_ID = solicitacaoId;
            tramite.PAPEL_ID = (int)item.PAPEL_ID_FIM;
            tramite.SOLICITACAO_STATUS_ID = 1;
            tramite.TRAMITE_DT_INI = DateTime.Now;
            tramite.GRUPO_DESTINO = item.GRUPO_DESTINO;
            _solicitacaoTramiteBP.Create(tramite);
            return tramite;
        }
        private SOLICITACAO_TRAMITE InserirTramiteSequencia(int solicitacaoId, int papelAtual, int status, int? usuario, int? grupoDestino, List<SOLICITACAO_TRAMITE> tramiteAtual)
        {
            SOLICITACAO_TRAMITE tramite;
            if (tramiteAtual.Count > 0)
            {
                tramite = tramiteAtual.Single(t => t.PAPEL_ID == papelAtual);
                tramite.SOLICITACAO_STATUS_ID = status; // Aprova
                tramite.TRMITE_DT_FIM = DateTime.Now;
                tramite.USUARIO_ID = usuario;
            }
            else
            {
                tramite = new SOLICITACAO_TRAMITE
                {
                    SOLICITACAO_ID = solicitacaoId,
                    PAPEL_ID = papelAtual,
                    SOLICITACAO_STATUS_ID = status,
                    USUARIO_ID = usuario,
                    TRAMITE_DT_INI = DateTime.Now,
                    TRMITE_DT_FIM = DateTime.Now,
                    GRUPO_DESTINO = grupoDestino
                };
                _solicitacaoTramiteBP.Create(tramite);
            }

            return tramite;
        }
    }
}