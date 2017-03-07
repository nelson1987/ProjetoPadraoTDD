using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class SolicitacaoFornecedorVM
    {
        public SolicitacaoFornecedorVM()
        {
            Fornecedores = new List<SolicitacaoFornecedoresVM>();
            Documentos = new List<SolicitacaoDocumentosVM>();
        }

        public int ID { get; set; }

        public int TipoSolicitacao { get; set; }

        public int Categoria { get; set; }

        public string NomeCategoria { get; set; }

        public SolicitacaoFornecedoresVM Fornecedor { get; set; }
        
        public List<SolicitacaoFornecedoresVM> Fornecedores { get; set; }

        public List<SolicitacaoDocumentosVM> Documentos { get; set; }

        [Display(Name = "Assunto")]
        [Required(ErrorMessage = "Informe o Assunto")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Informe Mensagem que estará no corpo do Email")]
        public string Mensagem { get; set; }

        public int PassoAtual { get; set; }

        public bool? Solicitacao { get; set; }

        public int? SolicitacaoCriacaoID { get; set; }

        public bool Aprovado { get; set; }

        public int ContratanteSelecionado { get; set; }
    }
}