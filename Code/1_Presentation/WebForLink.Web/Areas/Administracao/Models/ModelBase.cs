
using System.Web.Mvc;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class ViewModelPadrao
    {
        public ViewModelPadrao()
        {
            Url = new UrlHelper();
        }
        public string UrlEditar { get; set; }
        public string UrlDetalhar { get; set; }
        public string UrlExcluir { get; set; }
        public int? Pagina { get; set; }
        public string MensagemSucesso { get; set; }
        public string MensagemErro { get; set; }
        public UrlHelper Url { get; set; }
        public string Key
        {
            get
            { return "r10X310y"; }
        }
    }
}