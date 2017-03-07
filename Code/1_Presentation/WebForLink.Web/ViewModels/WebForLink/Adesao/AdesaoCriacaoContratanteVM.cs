using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels.Adesao
{
    public class AdesaoCriacaoContratanteVM
    {
        [Required(ErrorMessage = "CNPJ é obrigatório")]
        public string Cnpj { get; set; }

        [DisplayName("Razão Social")]
        [Required(ErrorMessage = "Razão Social é obrigatória")]
        public string RazaoSocial { get; set; }
    }
}