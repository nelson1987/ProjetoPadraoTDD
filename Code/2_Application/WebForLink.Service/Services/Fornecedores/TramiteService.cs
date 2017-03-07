using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Data.Contextos;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.UnitOfWork;

using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Domain.Services.Fornecedores
{
    public interface ITramiteService
    {

        void InserirTramiteSequencia(int contratanteId, int fluxoId, int solicitacaoId, int papelAtualId, int statusId, int usuarioId, int grupoDestino, List<SOLICITACAO_TRAMITE> tramiteAtual, List<FLUXO_SEQUENCIA> proximoPapeis);
        void AtualizaTramite(WebForLinkContexto Db, int contratanteId, int solicitacaoId, int fluxoId, int papelAtualId, int statusId, int? usuarioId);
        void AlterarSolicitacaoParaFinalizado(int solicitacaoId, int solicitacaotatusId);
        SOLICITACAO_TRAMITE InserirTramiteInicial(int solicitacaoId, int PapelAtual, int Status, int? Usuario, int? grupoDestino);
        List<FLUXO_SEQUENCIA> ListarProximoPapeisFluxo(int contratanteId, int fluxoId, int papelAtualId, int? grupoOrigem);
        List<SOLICITACAO_TRAMITE> RetornarSolicitacaoTramiteAtual(int solicitacaoId);
    }

    public class TramiteService : AppService<WebForLinkContexto>, ITramiteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        private readonly ISolicitacaoTramiteRepository _solicitacaoTramiteRepository;
        private readonly IFluxoSequenciaRepository _fluxoSequenciaRepository;
        public TramiteService(IUnitOfWork processo, ISolicitacaoRepository solicitacao, ISolicitacaoTramiteRepository solicitacaoTramite, IFluxoSequenciaRepository fluxoSequencia)
        {
            try
            {
                _unitOfWork = processo;
                _solicitacaoRepository = solicitacao;
                _solicitacaoTramiteRepository = solicitacaoTramite;
                _fluxoSequenciaRepository = fluxoSequencia;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
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
                            AlterarSolicitacaoParaFinalizado(solicitacaoId, 4);// Concluido
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar inserir trâmite de sequência.", ex);
            }
        }

        public void AtualizaTramite(WebForLinkContexto Db, int contratanteId, int solicitacaoId, int fluxoId, int papelAtualId, int statusId, int? usuarioId)
        {
            int? grupoDestino = 0;

            SOLICITACAO_TRAMITE tramite;
            List<SOLICITACAO_TRAMITE> tramiteAtual = RetornarSolicitacaoTramiteAtual(solicitacaoId);

            grupoDestino = tramiteAtual.Count > 0
                ? tramiteAtual.Single(t => t.PAPEL_ID == papelAtualId).GRUPO_DESTINO
                : ListarProximoPapeisFluxo(contratanteId, fluxoId, papelAtualId, 1).FirstOrDefault().GRUPO_DESTINO;

            switch (statusId)
            {
                case 1:
                    tramite = InserirTramiteInicial(solicitacaoId, papelAtualId, statusId, usuarioId, grupoDestino);
                    break;
                case 2:
                    tramite = InserirTramiteSequencia(solicitacaoId, papelAtualId, statusId, usuarioId, grupoDestino, tramiteAtual);

                    List<FLUXO_SEQUENCIA> proximoPapeis = ListarProximoPapeisFluxo(contratanteId, fluxoId, papelAtualId, grupoDestino);

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
                                AlterarSolicitacaoParaFinalizado(solicitacaoId, 4);// Concluido
                        }
                    }

                    //EmailSolicitacao emailSolicitacao = new EmailSolicitacao();
                    //emailSolicitacao.EnviarEmailSolicitacao(Contratante, solicitacaoId, Fluxo, proximoPapeis);
                    break;
                case 3:
                case 6:
                    tramite = tramiteAtual.Single(t => t.PAPEL_ID == papelAtualId);
                    tramite.SOLICITACAO_STATUS_ID = statusId; // Reprovado
                    tramite.USUARIO_ID = usuarioId;
                    tramite.TRMITE_DT_FIM = DateTime.Now;

                    AlterarSolicitacaoParaFinalizado(solicitacaoId, statusId); // Reprovado

                    //EmailSolicitacao emailReprovacao = new EmailSolicitacao();
                    //emailReprovacao.EnviarEmailReprovacao(solicitacao.ID);
                    break;
            }
        }

        public void AlterarSolicitacaoParaFinalizado(int solicitacaoId, int solicitacaotatusId)
        {
            SOLICITACAO solicitacao = _solicitacaoRepository.BuscarPorId(solicitacaoId);
            solicitacao.SOLICITACAO_STATUS_ID = solicitacaotatusId;
        }

        private SOLICITACAO_TRAMITE InserirTramiteConclusao(int solicitacaoId, FLUXO_SEQUENCIA item)
        {
            SOLICITACAO_TRAMITE tramite = new SOLICITACAO_TRAMITE();
            tramite.SOLICITACAO_ID = solicitacaoId;
            tramite.PAPEL_ID = (int)item.PAPEL_ID_FIM;
            tramite.SOLICITACAO_STATUS_ID = 1;
            tramite.TRAMITE_DT_INI = DateTime.Now;
            tramite.GRUPO_DESTINO = item.GRUPO_DESTINO;
            _solicitacaoTramiteRepository.Inserir(tramite);
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
                _solicitacaoTramiteRepository.Inserir(tramite);
            }

            return tramite;
        }

        public SOLICITACAO_TRAMITE InserirTramiteInicial(int solicitacaoId, int PapelAtual, int Status, int? Usuario, int? grupoDestino)
        {
            SOLICITACAO_TRAMITE tramite = new SOLICITACAO_TRAMITE
            {
                SOLICITACAO_ID = solicitacaoId,
                PAPEL_ID = PapelAtual,
                SOLICITACAO_STATUS_ID = Status,
                USUARIO_ID = Usuario,
                TRAMITE_DT_INI = DateTime.Now,
                GRUPO_DESTINO = grupoDestino
            };
            _solicitacaoTramiteRepository.Inserir(tramite);
            return tramite;
        }

        public List<FLUXO_SEQUENCIA> ListarProximoPapeisFluxo(int contratanteId, int fluxoId, int papelAtualId, int? grupoOrigem)
        {
            var predicate = PredicateBuilder.True<FLUXO_SEQUENCIA>();
            predicate = predicate.And(f => f.CONTRATANTE_ID == contratanteId
                && f.FLUXO_ID == fluxoId
                && f.PAPEL_ID_INI == papelAtualId
                && f.GRUPO_ORIGEM == grupoOrigem);
            return _fluxoSequenciaRepository.Listar(predicate).ToList();
        }

        public List<SOLICITACAO_TRAMITE> RetornarSolicitacaoTramiteAtual(int solicitacaoId)
        {
            var predicate = PredicateBuilder.True<SOLICITACAO_TRAMITE>();
            predicate = predicate.And(x => x.SOLICITACAO_ID == solicitacaoId && x.SOLICITACAO_STATUS_ID == 1);
            return _solicitacaoTramiteRepository.Listar(predicate).ToList();
        }

        public void Dispose()
        {
            _unitOfWork.Finalizar();
        }
    }
}
