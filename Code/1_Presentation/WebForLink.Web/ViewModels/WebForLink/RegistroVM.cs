using System;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class RegistroVM
    {
        public int ID { get; set; }

        public int TipoCadastro { get; set; }

        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Informe o CPF!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe a Razão Social!")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage="Informe o Nome!")]
        [Display(Name="Nome")]
        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Informe o Email!")]
        [EmailAddress(ErrorMessage="Email Inválido!")]
        [Display(Name = "E-mail")]
        public string Email{ get; set; }

        [Display(Name = "Cargo")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [MaxLength(10, ErrorMessage = "A Senha não pode ter mais que 10 digitos")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme a Senha digitada!")]
        [Compare("Senha", ErrorMessage = "A Senha não é igual a confirmação!")]
        [Display(Name = "Confirmar a Senha")]
        [MaxLength(10, ErrorMessage = "A Senha não pode ter mais de 10 digitos")]
        public string ConfirmaSenha { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? Nascimento { get; set; }

        public bool UsuarioPrincipal { get; set; }

        public string login { get; set; }
    }
}