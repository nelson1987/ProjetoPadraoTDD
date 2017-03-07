using System.Collections.Generic;

namespace WebForLink.Web.ViewModels.FichaCadastral
{
    public class FichaPreCadastroVM
    {
        public FichaPreCadastroVM()
        {
        }

        public List<DadosBancariosVM> DadosBancarios { get; set; }
        public List<DadosContatoVM> DadosContatos { get; set; }
        public List<DadosEnderecosVM> DadosEnderecos { get; set; }
        public List<FornecedorUnspscVM> FornecedoresUnspsc { get; set; }
    }
}