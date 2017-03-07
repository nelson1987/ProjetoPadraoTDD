using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels.Adesao
{
    public class PreCadastroAdesaoVM
    {
        public int PlanoEscolhido { get; set; }

        [DisplayName("Nome completo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ResponsavelNome { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ResponsavelCPF { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ResponsavelEmail { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ResponsavelTelefone { get; set; }

        [DisplayName("CNPJ")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaDocumento { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaCEP { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaEndereco { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaNumero { get; set; }

        [DisplayName("Complemento")]
        public string EmpresaComplemento { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaCidade { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaUF { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaPais { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaTelefone { get; set; }
        
        [DisplayName("Razão Social")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EmpresaNome { get; set; }

        public object Referencia { get; internal set; }
    }
    public class PreCadastroAdesaoResponsavelVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CPF { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Telefone { get; set; }
    }
    public class PreCadastroAdesaoEmpressaVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        
        //[Remote("ValidateUserName", "Adesao")]/*,AdditionalFields ="Endereco,Numero,Cidade,UF", ErrorMessage ="ERRO!",HttpMethod ="POST")]*/
        //[Remote("ValidateUserNameRoute", "Adesao", HttpMethod = "Post", AdditionalFields = "Nome", ErrorMessage = "Username is not available.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CEP { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Endereco { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        public string Numero { get; set; }

        public string Complemento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cidade { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UF { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Telefone { get; set; }
    }
}