using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class CadastroDiretoVM
    {
        public CadastroDiretoVM()
        {
            FichaCadastral = new FichaCadastralWebForLinkVM();
        }

        public int Id { get; set; }
        public int TipoFornecedorId { get; set; }
        public string TipoFornecedor { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public ContratanteVM Empresa { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public FornecedorCategoriaVM Categoria { get; set; }
        public int OrganizacaoComprasId { get; set; }
        public string OrganizacaoComprasNome { get; set; }
        public OrganizacaoComprasVM OrganizacaoCompras { get; set; }
        public string CNPJCPF { get; set; }
        public string RazaoSocial { get; set; }
        public string DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string NomeContato { get; set; }
        public string EmailContato { get; set; }
        public FichaCadastralWebForLinkVM FichaCadastral { get; set; }
    }

}