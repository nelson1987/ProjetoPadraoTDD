using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class DadosFornecedorImportacaoModel
    {
        public string CPF { get; set; }                
        public string CNPJ { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public string NomeContato { get; set; }
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }

        public List<ErroFornecedorImportacaoModel> ErrosImportacao { get; set; }
    }
}