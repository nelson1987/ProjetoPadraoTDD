using System.Collections.Generic;
using System.Web.Mvc;

namespace WebForLink.Web.ViewModels
{
    public class ListaDocumentosSolicitacaoVM
    {
        public ListaDocumentosSolicitacaoVM()
        {
            GrupoDocumentos = new List<SelectListItem>();
            Documentos = new List<SelectListItem>();
            Periodicidades = new List<SelectListItem>();
        }
        public ListaDocumentosSolicitacaoVM(int id, bool? obrigatorio, bool? exigeValidade, int? listaDocumento, int? periodicidade, int? documento)
        {
            Id = id;
            Obrigatorio = obrigatorio.HasValue ? obrigatorio.Value : false;
            ExigeValidade = exigeValidade.HasValue ? exigeValidade.Value : false;
            GrupoDocumento = listaDocumento.HasValue ? listaDocumento.Value : 0;
            Documento = documento.HasValue ? documento.Value : 0;
            if (ExigeValidade)
            {
                TipoAtualizacao = 2;
                if (periodicidade.HasValue)
                {
                    Periodicidade = periodicidade.Value;
                    TipoAtualizacao = 3;
                }
            }
            else
                TipoAtualizacao = 1;
            GrupoDocumentos = new List<SelectListItem>();
            Documentos = new List<SelectListItem>();
            Periodicidades = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public bool ExigeValidade { get; set; }
        public bool Obrigatorio { get; set; }
        public int TipoAtualizacao { get; set; }
        public List<SelectListItem> GrupoDocumentos { get; set; }
        public int GrupoDocumento { get; set; }
        public List<SelectListItem> Documentos { get; set; }
        public int Documento { get; set; }
        public List<SelectListItem> Periodicidades { get; set; }
        public int Periodicidade { get; set; }
        public int SolicitacaoId { get; set; }

    }
}