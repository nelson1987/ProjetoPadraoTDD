using System.Web;

namespace WebForLink.Web.ViewModels
{
    public class PesquisaFornecedoresVM
    {
        public int? Pagina { get; set; }
        public string hiddenFornecedorId { get; set; }
        public string acao { get; set; }
        public string ids { get; set; }
        public int? CategoriaSelecionada { get; set; }
        public string CategoriaSelecionadaNome { get; set; }
        public string Fornecedor { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string MensagemSucesso { get; set; }
        public string MensagemError { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}