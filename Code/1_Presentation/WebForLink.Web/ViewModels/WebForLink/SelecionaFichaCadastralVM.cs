using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebForLink.Web.ViewModels
{
    public class SelecionaFichaCadastralVM
    {
        public int ContratanteId { get; set; }
        [Required(ErrorMessage ="Contratante é um dado obrigatório")]
        public int FornecedorId { get; set; }
        [DisplayName("Selecione um Contratante")]
        public List<SelectListItem> ContratantesList { get; set; }
    }
}