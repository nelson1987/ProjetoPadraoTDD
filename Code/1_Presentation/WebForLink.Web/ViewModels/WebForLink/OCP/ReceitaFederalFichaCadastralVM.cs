using System.ComponentModel;

namespace WebForLink.Web.ViewModels.OCP
{
    public class ReceitaFederalFichaCadastralVM
    {
        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }
        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }
        [DisplayName("Situação Cadastral")]
        public string SituacaoCadastral { get; set; }
        [DisplayName("Motivo Situação Cadastral")]
        public string MotivoSituacaoCadastral { get; set; }
        [DisplayName("Data Sit. Cad.")]
        public string DataSituacaoCadastral { get; set; }
        [DisplayName("Data Emissão")]
        public string DataEmissao { get; set; }
        [DisplayName("Hora Emissão")]
        public string HoraEmissao { get; set; }
        [DisplayName("Data Abertura")]
        public string DataAbertura { get; set; }
        [DisplayName("Ente Federativo")]
        public string EnteFederativo { get; set; }
        [DisplayName("Endereço Eletrônico")]
        public string EnderecoEletronico { get; set; }
        [DisplayName("Telefone")]
        public string Telefone { get; set; }
        [DisplayName("Observação IBGE")]
        public string ObservacaoIbge { get; set; }
        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }
        [DisplayName("Número")]
        public string Numero { get; set; }
        [DisplayName("Complemento")]
        public string Complemento { get; set; }
        [DisplayName("Bairro")]
        public string Bairro { get; set; }
        [DisplayName("Município")]
        public string Municipio { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }
        [DisplayName("CEP")]
        public string Cep { get; set; }
        [DisplayName("Matriz ou Filial")]
        public string MatrizOuFilial { get; set; }
        [DisplayName("Atividade Principal")]
        public string AtividadePrincipal { get; set; }
        [DisplayName("Natureza Jurídica")]
        public string NaturezaJuridica { get; set; }
        [DisplayName("Situação Especial")]
        public string SituacaoEspecial { get; set; }
        [DisplayName("Data Situação Especial")]
        public string DatasSituacaoEspecial { get; set; }

    }
}