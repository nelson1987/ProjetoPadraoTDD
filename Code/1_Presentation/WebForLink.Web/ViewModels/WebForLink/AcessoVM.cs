using System.ComponentModel.DataAnnotations;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels
{
    public class AcessoVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Informe o Login!")]
        [Display(Name = "Login")]
        [MaxLength(255, ErrorMessage = "Login não pode ter mais que 255 Caracteres")]
        public string Login { get; set; }

        [Display(Name = "E-mail")]
        [MaxLength(255, ErrorMessage = "E-mail não pode ter mais que 255 Caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        //[MaxLength(10, ErrorMessage = "A Senha não pode ter mais que 10 digitos")]
        public string Senha { get; set; }
        public int ContratanteId { get; set; }
        public string NomeEmpresa { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string ReturnUrl { get; set; }
        public int? SolicitacaoId { get; set; }
        public int? FornecedorId { get; set; }
        public int TravaLogin { get; set; }
        public string chaveUrl { get; set; }
        public EnumTipoCadastroNovoUsuario TipoCadastroNovoUsuario { get; set; }
    }
}