using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class CadastrarUsuarioFornecedorVM : TrocaSenhaEsqueceuVM
    {
        [DisplayName("CPF/CNPJ")]
        public string DocumentoPjPf { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "E-mail alternativo é um campo obrigatório")]
        [DisplayName("E-mail alternativo")]
        public string EmailAlternativo { get; set; }

        public int SolicitacaoId { get; set; }
        /// <summary>
        /// True: usuário aceitou o termo
        /// </summary>
        public bool TermoAceite { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeFornecedor { get; set; }

        public string CnpjSemFormatacao
        {
            get
            {
                return DocumentoPjPf
                .Replace(".", "")
                .Replace("/", "")
                .Replace("-", "");
            }
        }
    }
}