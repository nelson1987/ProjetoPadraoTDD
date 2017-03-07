using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class MensagemImportacaoVM
    {
        public MensagemImportacaoVM()
        {
        }
        public MensagemImportacaoVM(string assunto, string corpo)
        {
            Documentos = new List<SolicitacaoDocumentosVM>()
                                {
                                    new SolicitacaoDocumentosVM()
                                };
            Fornecedores = new List<SolicitacaoFornecedoresVM>()
                                {
                                    new SolicitacaoFornecedoresVM()
                                };
            Fornecedor = new SolicitacaoFornecedoresVM();
            Assunto = assunto;
            Mensagem = corpo;
        }
        [Display(Name = "Assunto")]
        [Required(ErrorMessage = "Informe o Assunto")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Informe Mensagem que estará no corpo do Email")]
        public string Mensagem { get; set; }

        public SolicitacaoFornecedoresVM Fornecedor { get; set; }

        public List<SolicitacaoFornecedoresVM> Fornecedores { get; set; }

        public List<SolicitacaoDocumentosVM> Documentos { get; set; }
    }
}