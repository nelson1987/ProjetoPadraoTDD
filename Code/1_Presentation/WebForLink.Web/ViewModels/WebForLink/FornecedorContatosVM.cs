using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{

    public class FornecedorContatosVM
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o Email")]
        public string Email { get; set; }
        
    }
}