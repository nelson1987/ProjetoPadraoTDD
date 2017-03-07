using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels.OCP
{
    public class SintegraFichaCadastralVM
    {
        [DisplayName("Código")]
        public string Codigo { get; set; }

        public string CertificadoHtml { get; set; }

        [DisplayName("Data de Consulta")]
        public string DataConsulta { get; set; }

        [DisplayName("Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }

        [DisplayName("Múltiplas Inscrição Estadual")]
        public string MultiplasIE { get; set; }

        [DisplayName("Data de situação cadastral de inscrição estadual")]
        public string SituacaoCadastralInscricaoEstadual { get; set; }

        [DisplayName("Data de situação cadastral de inscrição estadual")]
        public string DataSituacaoCadastralInscricaoEstadual { get; set; }

        [DisplayName("Data Inclusão")]
        public string DataInclusao { get; set; }

        [DisplayName("Atividade Econômica Principal")]
        public string Telefone { get; set; }

        [DisplayName("Atividade Econômica Principal")]
        public string AtividadeEconomicaPrincipal { get; set; }
        
        public string Bairro { get; set; }

        [DisplayName("CEP")]
        public string CEP { get; set; }

        [DisplayName("CNPJ")]
        public string CNPJ { get; set; }

        [DisplayName("Complemento")]
        public string Complemento { get; set; }

        //[Display(Name = "EnquadramentoFiscal", ResourceType = typeof(Translate.Resources))]
        public string EnquadramentoFiscal { get; set; }

        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }

        [DisplayName("Município")]
        public string Municipio { get; set; }
        
        [DisplayName("Número")]
        public string Numero { get; set; }

        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }

        [DisplayName("Estado")]
        public string UF { get; set; }

        [DisplayName("Data Situação Cadastral")]
        public string DataSituacaoCadastral { get; internal set; }

        [DisplayName("Situação Cadastral")]
        public string SituacaoCadastral { get; internal set; }

        public string HTML { get; internal set; }
    }
}