using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
	public class UsuarioOrigemFornecedorVM : UsuarioVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CNPJ { get; set; }
    }
}