using System.Collections.Generic;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class DocumentosCompartilhadosVM
    {
        public DocumentosCompartilhadosVM()
        {
            DocumentosCompartilhados = new List<DocsCompartilhadosVM>();
        }

        public FichaCadastralWebForLinkVM FichaCadastral { get; set; }

        public List<DocsCompartilhadosVM> DocumentosCompartilhados { get; set; }
    }
    public class DocsCompartilhadosVM
    {
        public int ID { get; set; }

        public string TipoDocumento { get; set; }

        public string DescricaoDocumento { get; set; }

        public string NomeArquivo { get; set; }

        public string UrlArquivo { get; set; }
    }
}