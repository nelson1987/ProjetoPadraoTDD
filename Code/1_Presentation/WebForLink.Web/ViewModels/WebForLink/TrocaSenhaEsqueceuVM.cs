using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class TrocaSenhaEsqueceuVM
    {
        public int ID { get; set; }

        public int TipoCadastro { get; set; }

        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [MaxLength(10, ErrorMessage = "A Senha não pode ter mais que 10 digitos")]
        [Display(Name = "Nova Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme a Senha digitada!")]
        [Compare("Senha", ErrorMessage = "A Senha não é igual a confirmação!")]
        [Display(Name = "Confirmar Nova Senha")]
        [MaxLength(10, ErrorMessage = "A Senha não pode ter mais de 10 digitos")]
        public string ConfirmaSenha { get; set; }

        public string TextoTermoAceite { get; set; }
    }
}