using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class FornecedoresSolicitacaoDocumentosVM
    {
        public FornecedoresSolicitacaoDocumentosVM() 
        { 
        }

        public FornecedoresSolicitacaoDocumentosVM(int periodicidade, string cnpj, string assunto, string corpo,List<Fornecedor> wfdPjpfList, int diasPrazo, string nomeContratante)
        {
            Periodicidade = periodicidade;
            CNPJContratante = cnpj;
            Assunto = assunto;
            corpo = corpo.Replace("^NomeEmpresa^", nomeContratante);
            Mensagem = corpo;


            ExpiracaoDocumento = DateTime.Now.AddDays(diasPrazo);
            listPJPF = new List<Fornecedor>();
            if (wfdPjpfList != null)
            {
                wfdPjpfList.ForEach(x => x.CNPJ = Mascara.MascararCNPJouCPF(x.CNPJ));
                listPJPF = wfdPjpfList;
            }
            Solicitacoes = new List<SOLICITACAO>();
        }

        public string CNPJContratante { get; set; }

        public List<Fornecedor> listPJPF { get; set; }

        public bool Obrigatorio { get; set; }

        public int TipoAtualizacao { get; set; }

        [Display(Name = "PeriodicidadeDoDocumento")]
        public int? Periodicidade { get; set; }

        public List<Documentos> ListDocumentos { get; set; }

        public string Assunto { get; set; }

        public string Mensagem { get; set; }

        public string Arquivo { get; set; }

        public string TipoArquivoSubido { get; set; }

        public string ArquivoSubido { get; set; }

        public string ArquivoSubidoOriginal { get; set; }

        public string ChaveUrl { get; set; }

        public FichaCadastralWebForLinkVM FichaCadastral { get; set; }

        public ICollection<SOLICITACAO> Solicitacoes { get; set; }

        [Display(Name = "Data de Expiração Documento")]
        [Required]
        //[Remote()]
        //[Remote("ValidarNomeLogin", "Validador", ErrorMessage = "Este login já existe")]
        public DateTime ExpiracaoDocumento { get; set; }
    }

    public class Documentos
    {
        public int GrupoDocumentoId { get; set; }
        public int DocumentoId { get; set; }
        public int Obrigatorio { get; set; }
        public int Periodicidade { get; set; }
    }

}