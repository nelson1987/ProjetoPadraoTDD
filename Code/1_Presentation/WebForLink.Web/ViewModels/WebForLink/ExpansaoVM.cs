using System.Collections.Generic;
using System.Web.WebPages.Html;

namespace WebForLink.Web.ViewModels
{
    public class ExpansaoVM
    {
        public int Empresa { get; set; }
        public List<SelectListItem> Empresas { get; set; }
        public int Organizacao { get; set; }
        public List<SelectListItem> Organizacoes { get; set; }
    }
}