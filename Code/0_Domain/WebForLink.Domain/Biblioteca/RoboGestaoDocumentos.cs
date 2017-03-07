using LinqKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebForDocs.Data.ModeloDB;
using WebForDocs.Dominio.Models;
using WebForDocs.Exceptions;
using WebForDocs.Interfaces;

namespace WebForDocs.Biblioteca
{
    /// <summary>
    /// buscar todos os documentos por Contratante e fornecedores já cadastrados que encontram-se com a data de validade maior que o dia de hoje
    /// </summary>
    public class RoboGestaoDocumentos
    {
        public static int? IdContratante { get; set; }
        public static int? IdFornecedor { get; set; }
        private static readonly IGeral _metodosGerais = new Geral();
        private static WFLModel db = new WFLModel();


        #region Construtores
        public static void InicializarRobo()
        {
            IdContratante = null;
            IdFornecedor = null;
            BloquearFornecedorDocumentoVencido(db);
            EnviarEmailDocumentosAVencer(db, 5);
            EnviarEmailDocumentosAVencer(db, 10);
        }
        public static void InicializarRobo(int contratante)
        {
            IdContratante = contratante;
            IdFornecedor = null;
            BloquearFornecedorDocumentoVencido(db);
            EnviarEmailDocumentosAVencer(db, 5);
            EnviarEmailDocumentosAVencer(db, 10);
        }
        public static void InicializarRobo(int contratante, int fornecedor)
        {
            IdContratante = contratante;
            IdFornecedor = fornecedor;
            BloquearFornecedorDocumentoVencido(db);
            EnviarEmailDocumentosAVencer(db, 5);
            EnviarEmailDocumentosAVencer(db, 10);
        }
        #endregion

        #region Bloquear Fornecedores com Documentos Vencidos
        /// <summary>
        /// R.9.1.4 ENVIA EMAIL DE BLOQUEIO DOCUMENTO VENCIDO - Caso a data de vencimento esteja vencida e o documento seja obrigatório, 
        /// o sistema envia um email informando que o Fornecedor/Cliente foi bloqueado até que atualize o documento desejado.
        /// </summary>
        /// <param name="db">Contexto</param>
        private static void BloquearFornecedorDocumentoVencido(WFLModel db)
        {
            //Filtros
            var predicate = PredicateBuilder.True<WFD_PJPF_DOCUMENTOS>();
            predicate = predicate.And(a => a.DATA_VENCIMENTO < DateTime.Now);
            predicate = predicativos(predicate);

            //Inserir IDs em Lista de Documentos Atrasados
            var lista = db.WFD_PJPF_DOCUMENTOS
                .Include("WFD_CONTRATANTE_PJPF")
                .AsExpandable()
                .Where(predicate)
                .Select(x => new
                {
                    fornecedorId = x.PJPF_ID,
                    contratanteId = (int)x.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID
                })
                .ToList();

            lista.Distinct().ForEach(x =>
            {
                InserirSolicitacaoBloqueioAtrasoDocumento(db, x.contratanteId, x.fornecedorId);
            });
        }

        /// <summary>
        /// Bloqueio automatico dos usuarios que ja estao com documentos com prazos atrasados
        /// </summary>
        /// <param name="db">Contexto</param>
        /// <param name="fornecedor">Id do Fornecedor</param>
        private static void InserirSolicitacaoBloqueioAtrasoDocumento(WFLModel db, int contratante, int fornecedor)
        {
            try
            {
                var FluxoId = db.WFL_FLUXO.FirstOrDefault(x => x.CONTRATANTE_ID == contratante && x.FLUXO_TP_ID == 110).ID;
                WFD_SOLICITACAO solicitacao = new WFD_SOLICITACAO()
                {
                    CONTRATANTE_ID = contratante,
                    FLUXO_ID = FluxoId,
                    SOLICITACAO_DT_CRIA = DateTime.Now,
                    SOLICITACAO_STATUS_ID = 5,
                    PJPF_ID = fornecedor,
                };
                WFD_SOL_BLOQ bloqueio = new WFD_SOL_BLOQ()
                {
                    BLQ_COMPRAS_TODAS_ORG_COMPRAS = false,
                    BLQ_LANCAMENTO_EMP = true,
                    BLQ_QUALIDADE_FUNCAO_BQL_ID = 2,
                    WFD_SOLICITACAO = solicitacao,
                    BLQ_MOTIVO_DSC = "Bloqueio gerado automaticamente."
                };
                db.Entry(solicitacao).State = EntityState.Added;
                db.Entry(bloqueio).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new WebForLinkException("Erro ao tentar inserir a solicitação de bloqueio. ", ex);
            }
        }
        #endregion

        #region Enviar Email para Fornecedor com Documentos a vencer em X Dias
        /// <summary>
        /// R.9.1.1/R.9.1.2 ENVIA  E-MAIL 5/10 DIAS ANTES DO VENCIMENTO - Caso a data de vencimento esteja a 5/10 dias do vencimento, 
        /// o sistema envia um email de alerta para que o Fornecedor/Cliente efetue o upload de uma versão com validade atualizada
        /// </summary>
        private static void EnviarEmailDocumentosAVencer(WFLModel db, int dias)
        {
            //Filtros
            var predicate = PredicateBuilder.True<WFD_PJPF_DOCUMENTOS>();
            //predicate = predicate.And(a => a.DATA_VENCIMENTO.Value.Subtract(DateTime.Now).Days == dias);
            predicate = predicativos(predicate);

            //Inserir IDs em Lista de Documentos Atrasados
            db.WFD_PJPF_DOCUMENTOS
                .AsExpandable()
                .Where(predicate)
                .ForEach(x =>
                {
                    var tempo = x.DATA_VENCIMENTO.GetValueOrDefault();
                    if ((tempo.Subtract(DateTime.Now).Days + dias) == 0)
                    {
                        var emailFornecedor = db.WFD_PJPF.FirstOrDefault(y => y.ID == x.PJPF_ID).EMAIL;
                        var assuntoMail = string.Format("Faltam {0} Dias para seu documento expirar", dias);
                        var corpoMail = string.Format("Seus documentos expiraram em: {0}. Efetue o upload de uma versão com validade atualizada", x.DATA_VENCIMENTO.GetValueOrDefault().ToShortDateString());
                        //--Enviar E-mail de fato
                        _metodosGerais.EnviarEmail(ConfigurationManager.AppSettings.Get("EmailRobo"), assuntoMail, corpoMail);
                    }
                });
        }
        #endregion

        #region Predicativos
        private static Expression<Func<WFD_PJPF_DOCUMENTOS, bool>> predicativos(Expression<Func<WFD_PJPF_DOCUMENTOS, bool>> predicate)
        {
            predicate = predicate.And(x => x.DATA_VENCIMENTO != null);
            if (IdFornecedor != null)
                predicate = predicate.And(d => d.PJPF_ID == IdFornecedor);

            if (IdContratante != null)
                predicate = predicate.And(x => x.CONTRATANTE_PJPF_ID == IdContratante);
            return predicate;
        }
        #endregion
    }
}