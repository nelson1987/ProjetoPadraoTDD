using System.Collections.Generic;
using System.Web.Mvc;

namespace WebForLink.Web.ViewModels
{
    public class PerguntaModeloList
    {
        public int perguntaId { get; set; }
        public List<SelectListItem> respostas { get; set; }
    }
}