using System.Collections.Generic;
using System.Web.WebPages.Html;

namespace WebForLink.Web.ViewModels
{
    public class DinamicQuestionsVM
    {
        public int IdPergunta { get; set; }
        public int IdResposta { get; set; }
        public List<SelectListItem> Respostas { get; set; }
    }
}