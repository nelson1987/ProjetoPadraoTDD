namespace WebForLink.Web.ViewModels
{
    public class RetornoFornecedoresModel : RetornoModel
    {
        public string CodigoFornecedorSap { get; set; }
        public int GrupoContas { get; set; }
        public int OrganizacaoCompras { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
    }
}