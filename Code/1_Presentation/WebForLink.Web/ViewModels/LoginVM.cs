using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Login é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}