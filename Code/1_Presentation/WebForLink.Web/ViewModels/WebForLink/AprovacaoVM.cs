using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class AprovacaoVM
    {
        public AprovacaoVM() 
        {
        }
        public AprovacaoVM(int idSolicitacao, int fluxoId, string fluxoNome)
        {
            Solicitacao = new SOLICITACAO();
            Fornecedor = new SolicitacaoCadastroFornecedor();
            Solicitacao_Tramite = new SOLICITACAO_TRAMITE();
            FornecedorFinalizado = new Fornecedor();
            IdSolicitacao = idSolicitacao;
            FluxoId = fluxoId;
            NomeSolicitacao = fluxoNome;
        }
        public int ID { get; set; }

        [Display(Name = "Data de Solicitação")]
        [DataType(DataType.Date)]
        public DateTime Solicitacao_Dt_Cria { get; set; }

        public int Contratante_ID { get; set; }

        [Display(Name = "Empresa")]
        public string NomeContratante { get; set; }

        [Display(Name = "Fornecedor")]
        public string NomeFornecedor { get; set; }

        [Display(Name = "Usuário")]
        public string Login { get; set; }

        public int PJPF_ID { get; set; }

        [Display(Name = "Tipo de Solicitação")]
        public string NomeFluxo { get; set; }

        public int FluxoId { get; set; }
        public int FluxoTPId { get; set; }

        [Display(Name = "Número da Solicitação")]
        public int IdSolicitacao { get; set; }

        [Display(Name = "Solicitação")]
        public string NomeSolicitacao { get; set; }

        [Display(Name = "Status Solicitação")]
        public string StatusSolicitacao { get; set; }

        [Display(Name = "Etapa")]
        public string SolicitacaoTramite { get; set; }

        public string UrlAprovacao { get; set; }

        public string CNPJ_CPF { get; set; }

        public string Etapa { get; set; }

        public string GrupoContas { get; set; }

        public SOLICITACAO Solicitacao { get; set; }
        public Usuario Usuario { get; set; }
        public SOLICITACAO_STATUS Solicitacao_Status { get; set; }
        public Fluxo Fluxo { get; set; }
        public SOLICITACAO_TRAMITE Solicitacao_Tramite { get; set; }
        public List<SOLICITACAO_TRAMITE> Solicitacao_Tramites { get; set; }
        public SolicitacaoCadastroFornecedor Fornecedor { get; set; }
        public FORNECEDOR_UNSPSC FornecedorUnspsc { get; set; }
        public Contratante Contratante { get; set; }
        public Fornecedor FornecedorFinalizado { get; set; }
    }
}